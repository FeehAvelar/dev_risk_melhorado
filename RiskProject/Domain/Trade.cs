using RiskProject.Domain.Enum;
using RiskProject.Domain.Interfaces;
using System.Globalization;

namespace RiskProject.Domain
{
    public class PaymentDateObjectValue
    {
        public DateTime Value { get; }
                
        public PaymentDateObjectValue(string date)
        {
            Value = ValidateConvertDate(date);
        }

        private DateTime ValidateConvertDate(string date)
        {
            DateTime result;

            var canConvert = DateTime.TryParseExact(date, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
            
            if (!canConvert)
                throw new ArgumentException($"Impossible convert {date} to DateTime, we expected date in this formart 'MM/dd/yyyy'");

            return result;
        }        
    }

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
            NextPaymentDate = new PaymentDateObjectValue(strSplited[2]).Value;
        }
    }
}
