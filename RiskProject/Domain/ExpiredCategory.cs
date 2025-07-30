using RiskProject.Domain.Enum;
using RiskProject.Domain.Interfaces;

namespace RiskProject.Domain
{
    public class ExpiredCategory : ICategory
    {
        public CategoryEnum Category { get => CategoryEnum.EXPIRED; }
        public bool ValidateCatagory(ITrade trade, DateTime referenceDate)
        {
            var dateExpired = trade.NextPaymentDate.AddDays(30);

            return referenceDate > dateExpired;            
        }
    }
}
