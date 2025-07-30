using RiskProject.Domain.Enum;

namespace RiskProject.Domain.Interfaces
{
    public interface ITrade
    {
        double Value { get; } 
        string ClientSector { get; }
        ClientSectorEnum ClientSectorEnum { get; }
        DateTime NextPaymentDate { get; }
    }
}
