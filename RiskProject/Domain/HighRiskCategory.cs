using RiskProject.Domain.Enum;
using RiskProject.Domain.Interfaces;

namespace RiskProject.Domain
{
    public class HighRiskCategory : ICategory
    {
        public CategoryEnum Category { get => CategoryEnum.HIGHRISK; }
        public bool ValidateCatagory(ITrade trade, DateTime referenceDate)
        {
            return trade.Value > 1_000_000 && trade.ClientSectorEnum == ClientSectorEnum.PRIVATE;
        }
    }
}
