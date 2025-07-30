
using RiskProject.Domain.Enum;
using RiskProject.Domain.Interfaces;
using System.Globalization;

namespace RiskProject.Domain
{
    public class Trade : ITrade
    {
        public double Value { get; }

        public string ClientSector { get; }
        public ClientSectorEnum ClientSectorEnum
        {
            get
            {
                ClientSectorEnum result;
                if (!System.Enum.TryParse(ClientSector, true, out result))
                {
                    throw new ArgumentException($"Unable to convert the value '{ClientSector}' into a valid client sector.");
                }

                return result;
            }
        }

        public DateTime NextPaymentDate { get; }        
        
        public Trade(double value, string clientSector, DateTime nextPaymentDay)
        {
            Value = value;
            ClientSector = clientSector;
            NextPaymentDate = nextPaymentDay;            
        }

        public Trade(string consoleInputTrader)
        {
            var strSplited = consoleInputTrader.Split(" ");

            if (strSplited.Length < 3)
                throw new ArgumentOutOfRangeException(nameof(strSplited), $"Expected at least 3 arguments in the input string, but received {strSplited.Length}.");

            Value = double.Parse(strSplited[0]);
            ClientSector = strSplited[1];
            NextPaymentDate = DateTime.ParseExact(strSplited[2], "MM/dd/yyyy", CultureInfo.InvariantCulture);
        }
    }
}
