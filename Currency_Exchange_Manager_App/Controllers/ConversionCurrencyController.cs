using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Currency_Exchange_Manager_App.Model;
using Currency_Exchange_Manager_App;
using Currency_Exchange_Manager_App.Repositories;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data.SqlTypes;
using static System.Net.WebRequestMethods;
using Microsoft.CodeAnalysis.RulesetToEditorconfig;
using StackExchange.Redis;


namespace Currency_Exchange_Manager_App.Controllers
{
    [Route("api/ConversionCurrency")]
    [ApiController]
    public class ConversionCurrencyController : ControllerBase
    {
        private readonly IConversionCurrency _conversionCurrency;
        private readonly ICurrencyInfo _currencyInfo;
        private readonly ICacheService _cacheSerivce;
        private readonly ILogger<ConversionCurrencyController> _logger;

        public ConversionCurrencyController(IConversionCurrency conversionCurrency, ICacheService cacheSerivce,
            ICurrencyInfo currencyInfo,
            ILogger<ConversionCurrencyController> logger)
        {
            _conversionCurrency = conversionCurrency;
            _currencyInfo = currencyInfo;
            _cacheSerivce = cacheSerivce;
            _logger = logger;
        }

        //create api currency conversion values, using from and to with amount
        [HttpPost]
        public async Task<IActionResult> AddConverisonRate(Convert_Currency convert_Currency)
        {
            try
            {

                /*create conversion rate to be saved on database*/
                if (ModelState.IsValid)
                {
                    // find currency saved on currency info 

                    string firstCurrency = "";
                    string secondCurrency = "";

                    int amount = 0;
                    int conver_rate = 0;
                    amount = Convert.ToInt32(convert_Currency.Amount);
                    conver_rate = Convert.ToInt32(convert_Currency.Conversion_Rate);


                    firstCurrency = convert_Currency.currency_from;
                    secondCurrency = convert_Currency.currency_to;

                    /*third party api gateway for converison*/
                    string urlString = $"http://currencies.apps.grandtrunk.net/getlatest/{firstCurrency}/{secondCurrency}";

                    using (WebClient client = new WebClient())
                    {
                        var result = await client.DownloadStringTaskAsync(urlString);

                        convert_Currency.Conversion_Rate = amount * Convert.ToDouble(result);

                    }
                }

                _cacheSerivce.RemoveData("ConversionCurrency");
                var createConversionCurrency = await _conversionCurrency.CreateConversionCurrencyAsync(convert_Currency);
                return CreatedAtAction(nameof(AddConverisonRate), createConversionCurrency);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        //return api history currency conversion values 
        [HttpGet]
        public async Task<IActionResult> GetConversionHistory()
        {
            try
            {
                var cacheData = _cacheSerivce.GetData<IEnumerable<Convert_Currency>>("Convert_Currency");
                if (cacheData != null)
                {
                    //return cacheData;
                }

                var expirationTIme = DateTimeOffset.Now.AddMinutes(15.0);

                var convertion = await _conversionCurrency.GetConversionListAsync();

                _cacheSerivce.SetData<IEnumerable<Convert_Currency>>("Convert_Currency", cacheData, expirationTIme);


                //return Ok(200,convertion);
                return StatusCode(200, convertion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        statusCode = 500,
                        message = ex.Message
                    });
            }
        }

        //return api single currency conversion values 
        [HttpGet("{id}")]
        public async Task<IActionResult> ReturnSingleConversion(int id)
        {
            try
            {
                var conversion = await _conversionCurrency.GetConversionByIdAsync(id);

                if (conversion == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        message = "record not found"
                    });
                }

                return Ok(conversion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        statusCode = 500,
                        message = ex.Message
                    });
            }
        }

        //delete currency conversion values 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConversion(int id)
        {
            try
            {
                var existingcurrecnyConversion = await _conversionCurrency.GetConversionByIdAsync(id);
                if (existingcurrecnyConversion == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        message = "record not found"
                    });
                }
                _cacheSerivce.RemoveData("ConversionCurrency");
                await _conversionCurrency.DeleteConversionAsync(existingcurrecnyConversion);
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        statusCode = 500,
                        message = ex.Message
                    });
            }
        }
    }
}
