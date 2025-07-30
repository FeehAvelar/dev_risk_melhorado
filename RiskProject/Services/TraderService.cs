using RiskProject.Domain.Enum;
using RiskProject.Domain.Interfaces;

namespace RiskProject.Services
{
    public class TraderService
    {
        private readonly IEnumerable<ICategory> _categories;

        public TraderService(IEnumerable<ICategory> categories)
        {
            _categories = categories;
        }

        public CategoryEnum ClassifyCategory(ITrade trade, DateTime referenceDate)
        {            
            foreach (var category in _categories)
            {
                if (category.ValidateCatagory(trade, referenceDate))
                    return category.Category;
            }
            
            return CategoryEnum.UNDEFINED;
        }
    }
}
