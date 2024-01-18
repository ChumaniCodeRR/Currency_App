using Currency_Exchange_Manager_App.Model;

namespace Currency_Exchange_Manager_App.Repositories
{
    public interface IConversionCurrency
    {
        Task<Convert_Currency> CreateConversionCurrencyAsync(Convert_Currency convert_Currency);

        Task<IEnumerable<Convert_Currency>> GetConversionListAsync();

        Task<Convert_Currency> GetConversionByIdAsync(int id);

        Task DeleteConversionAsync(Convert_Currency convert_Currency);
    }
}
