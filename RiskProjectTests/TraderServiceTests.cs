using Microsoft.Extensions.DependencyInjection;
using Moq;
using RiskProject.Domain.Enum;
using RiskProject.Domain.Interfaces;
using RiskProject.Services;
using System.Reflection;
using Xunit;
using static System.Formats.Asn1.AsnWriter;

namespace RiskProjectTests
{
    public class TraderServiceTestsFixture
    {
        public TraderService _traderService;
        public TraderServiceTestsFixture()
        {
            var services = new ServiceCollection();
            var assembly = Assembly.GetAssembly(typeof(ICategory));

            var categoryTypes = assembly
                .GetTypes()
                .Where(type => typeof(ICategory).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

            foreach (var type in categoryTypes)
            {
                services.AddScoped(typeof(ICategory), type);
            }
            
            services.AddScoped<TraderService>();
            
            var serviceProvider = services.BuildServiceProvider();

            _traderService = serviceProvider.GetRequiredService<TraderService>();
        }
    }

    public class TraderServiceTests : IClassFixture<TraderServiceTestsFixture>
    {
        private readonly TraderServiceTestsFixture _fixture;
        public TraderServiceTests(TraderServiceTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ClassifyCategory_ShouldReturnExpiredCategory()
        {
            var tradeMock = new Mock<ITrade>();
            var dayToExpire = 31 * -1;
            tradeMock.Setup(t => t.NextPaymentDate).Returns(DateTime.Now.Date.AddDays(dayToExpire));
            var trade = tradeMock.Object;
                      
            var referenceDate = DateTime.Today;
                        
            var result = _fixture._traderService.ClassifyCategory(trade, referenceDate);

            // Assert
            Assert.Equal(CategoryEnum.EXPIRED, result);
        }

        [Fact]
        public void ClassifyCategory_ShouldReturnHighRiskCategory()
        {
            var tradeMock = new Mock<ITrade>();
            tradeMock.Setup(t => t.NextPaymentDate).Returns(DateTime.Now.Date);
            tradeMock.Setup(t => t.ClientSector).Returns("Private");
            tradeMock.Setup(t => t.ClientSectorEnum).Returns(ClientSectorEnum.PRIVATE);
            tradeMock.Setup(t => t.Value).Returns(1_000_001);

            var trade = tradeMock.Object;

            var referenceDate = DateTime.Today;

            var result = _fixture._traderService.ClassifyCategory(trade, referenceDate);

            // Assert
            Assert.Equal(CategoryEnum.HIGHRISK, result);
        }

        [Fact]
        public void ClassifyCategory_ShouldReturnMediumRiskCategory()
        {
            var tradeMock = new Mock<ITrade>();
            tradeMock.Setup(t => t.NextPaymentDate).Returns(DateTime.Now.Date);
            tradeMock.Setup(t => t.ClientSectorEnum).Returns(ClientSectorEnum.PUBLIC);
            tradeMock.Setup(t => t.Value).Returns(1_000_001);

            var trade = tradeMock.Object;

            var referenceDate = DateTime.Today;

            var result = _fixture._traderService.ClassifyCategory(trade, referenceDate);

            // Assert
            Assert.Equal(CategoryEnum.MEDIUMRISK, result);
        }

        [Fact]
        public void ClassifyCategory_ShouldReturnUndefinedCategory()
        {
            var tradeMock = new Mock<ITrade>();
            tradeMock.Setup(t => t.NextPaymentDate).Returns(DateTime.Now.Date);
            tradeMock.Setup(t => t.ClientSector).Returns("Private");
            tradeMock.Setup(t => t.Value).Returns(1_000);

            var trade = tradeMock.Object;

            var referenceDate = DateTime.Today;

            var result = _fixture._traderService.ClassifyCategory(trade, referenceDate);

            // Assert
            Assert.Equal(CategoryEnum.UNDEFINED, result);
        }
    }
}