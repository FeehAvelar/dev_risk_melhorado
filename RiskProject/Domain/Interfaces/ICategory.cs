using RiskProject.Domain.Enum;

namespace RiskProject.Domain.Interfaces
{
    public interface ICategory
    {        
        CategoryEnum Category { get; }

        bool ValidateCatagory(ITrade trade, DateTime referenceDate);
    }
}
