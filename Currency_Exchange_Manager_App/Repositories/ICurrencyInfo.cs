using Currency_Exchange_Manager_App.Model;

namespace Currency_Exchange_Manager_App.Repositories
{
    public interface ICurrencyInfo
    {
        /*interface methods, for CRUD*/
        Task<CurrencyInfo> CreateCurrencyAsync(CurrencyInfo currencyInfo);

        Task UpdateCurrencyAsync(CurrencyInfo currencyInfo);

        Task DeleteCurrencyAsync(CurrencyInfo currencyInfo);

        Task<IEnumerable<CurrencyInfo>> GetCurrencyAsync();

        Task<CurrencyInfo> GetCurrencyByIdAsync(int id);

        Task<CurrencyInfo> GetCurrencyByNameAsync(string name);

    }
}
