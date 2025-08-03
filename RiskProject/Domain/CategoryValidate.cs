using RiskProject.Domain.Enum;
using RiskProject.Domain.Interfaces;

namespace RiskProject.Domain
{
    public abstract class CategoryValidate : ICategory, ICategoryPriority
    {        
        public int Priority { get; }

        public CategoryEnum Category { get; }

        protected CategoryValidate(CategoryEnum category)
        {
            Category = category;
            Priority = int.MaxValue;
        }
        protected CategoryValidate(CategoryEnum category, int priority) : this(category) 
        {            
            Priority = priority;
        }

        public abstract bool ValidateCatagory(ITrade trade, DateTime referenceDate);
    }
}
