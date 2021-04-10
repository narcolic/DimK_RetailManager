using System.Configuration;

namespace DKRDataManager.Library
{
    public static class ConfigHelper
    {
        public static decimal GetTaxRate() => decimal.TryParse(ConfigurationManager.AppSettings["taxRate"], out decimal taxRate) ? taxRate / 100 : throw new ConfigurationErrorsException("Tax Rate not setup properly.");
    }
}