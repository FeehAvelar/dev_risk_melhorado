using RiskProject.Domain.Enum;
using RiskProject.Domain.Interfaces;

namespace RiskProject.Domain
{
    public class MediumRiskCategory : ICategory
    {
        public CategoryEnum Category { get => CategoryEnum.MEDIUMRISK; }

        public bool ValidateCatagory(ITrade trade, DateTime referenceDate)
        {
            return trade.Value > 1_000_000 && trade.ClientSectorEnum == ClientSectorEnum.PUBLIC;
        }
    }

}
