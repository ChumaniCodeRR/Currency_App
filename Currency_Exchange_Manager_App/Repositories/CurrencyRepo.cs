using Currency_Exchange_Manager_App.Model;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace Currency_Exchange_Manager_App.Repositories
{
    public class CurrencyRepo : ICurrencyInfo
    {
        private readonly DataAppContext _ctx;

        public CurrencyRepo(DataAppContext ctx)
        {
            _ctx = ctx;

        }

        public async Task<IEnumerable<CurrencyInfo>> GetCurrencyAsync()
        {
            var currency = await _ctx.Currency_Info.ToListAsync();
            return currency;
        }

        public async Task<CurrencyInfo> GetCurrencyByIdAsync(int id)
        {
            return await _ctx.Currency_Info.FindAsync(id);

        }

        public async Task<CurrencyInfo> GetCurrencyByNameAsync(string name)
        {
            return await this._ctx.Currency_Info.FirstOrDefaultAsync(c => c.Currency_details == name).ConfigureAwait(false);

        }

        public async Task<CurrencyInfo> CreateCurrencyAsync(CurrencyInfo currency)
        {
            _ctx.Currency_Info.Add(currency);
            await _ctx.SaveChangesAsync();
            return currency;
        }

        public async Task UpdateCurrencyAsync(CurrencyInfo currency)
        {
            _ctx.Currency_Info.Update(currency);
            await _ctx.SaveChangesAsync();

        }

        public async Task DeleteCurrencyAsync(CurrencyInfo currency)
        {
            _ctx.Currency_Info.Remove(currency);
            await _ctx.SaveChangesAsync();

        }
    }
}
