namespace GLMS.Web.Interfaces;

public interface ICurrencyService
{
    Task<decimal> GetZarRateAsync();
    Task<decimal> ConvertUsdToZarAsync(decimal usdAmount);
}
