using Currency_Exchange_Manager_App.Model;
using Microsoft.EntityFrameworkCore;

namespace Currency_Exchange_Manager_App.Repositories
{
    public class ConversionCurrencyRepo : IConversionCurrency
    {
        private readonly DataAppContext _ctx;

        public ConversionCurrencyRepo(DataAppContext ctx)
        {
            _ctx = ctx;
        }
        /*create converion which is saved on database*/
        public async Task<Convert_Currency> CreateConversionCurrencyAsync(Convert_Currency convert_Currency)
        {
            _ctx.Convert_Currency.Add(convert_Currency);
            await _ctx.SaveChangesAsync();
            return convert_Currency;
        }

        /*list converion history from database*/
        public async Task<IEnumerable<Convert_Currency>> GetConversionListAsync()
        {
            var convertcurrency = await _ctx.Convert_Currency.ToListAsync();
            return convertcurrency;
        }

        /*list by single converions from database*/
        public async Task<Convert_Currency> GetConversionByIdAsync(int id)
        {
            return await _ctx.Convert_Currency.FindAsync(id);
        }

        public async Task DeleteConversionAsync(Convert_Currency convert_Currency)
        {
            _ctx.Convert_Currency.Remove(convert_Currency);
            await _ctx.SaveChangesAsync();

        }
    }
}
