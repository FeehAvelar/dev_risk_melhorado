using RiskProject.Domain.Enum;
using RiskProject.Domain.Interfaces;

namespace RiskProject.Domain
{
    public class ExpiredCategory : CategoryValidate
    {        
        public ExpiredCategory() : base(CategoryEnum.EXPIRED, priority: 0) 
        {
        }
        
        public override bool ValidateCatagory(ITrade trade, DateTime referenceDate)
        {
            var dateExpired = trade.NextPaymentDate.AddDays(30);

            return referenceDate > dateExpired;            
        }
    }
}
