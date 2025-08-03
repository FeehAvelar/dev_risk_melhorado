using RiskProject.Domain.Enum;
using RiskProject.Domain.Interfaces;

namespace RiskProject.Domain
{
    public class HighRiskCategory : CategoryValidate
    {
        public HighRiskCategory() : base(CategoryEnum.HIGHRISK, priority: 1)
        {
        }

        public override bool ValidateCatagory(ITrade trade, DateTime referenceDate)
        {
            return trade.Value > 1_000_000 && trade.ClientSectorEnum == ClientSectorEnum.PRIVATE;
        }
    }
}
