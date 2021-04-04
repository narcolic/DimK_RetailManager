using System.Configuration;

namespace DKRDesktopUI.Library.Helpers
{
    public class ConfigHelper : IConfigHelper
    {
        public decimal GetTaxRate() => decimal.TryParse(ConfigurationManager.AppSettings["taxRate"], out decimal taxRate) ? taxRate / 100 : throw new ConfigurationErrorsException("Tax Rate not setup properly.");
    }
}