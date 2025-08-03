using RiskProject.Domain.Enum;
using RiskProject.Domain.Interfaces;

namespace RiskProject.Domain
{
    public class MediumRiskCategory : CategoryValidate
    {
        public MediumRiskCategory() : base(CategoryEnum.MEDIUMRISK, priority: 2)
        {
        }

        public override bool ValidateCatagory(ITrade trade, DateTime referenceDate)
        {
            return trade.Value > 1_000_000 && trade.ClientSectorEnum == ClientSectorEnum.PUBLIC;
        }        
    }
}
