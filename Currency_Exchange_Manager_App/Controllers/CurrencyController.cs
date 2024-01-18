using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Currency_Exchange_Manager_App.Model;
using Currency_Exchange_Manager_App;
using Currency_Exchange_Manager_App.Repositories;

namespace Currency_Exchange_Manager_App.Controllers
{
    [Route("api/Currency")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyInfo _currencyRepo;
        private readonly ILogger<CurrencyController> _logger;

        /*Create visible CRUD from API, using logger, with error handling*/
        public CurrencyController(ICurrencyInfo currencyRepo,
            ILogger<CurrencyController> logger)
        {
            _currencyRepo = currencyRepo;
            _logger = logger;
        }

        //adding currency values 
        [HttpPost]
        public async Task<IActionResult> AddCurrency(CurrencyInfo currency)
        {
            try
            {
                var createdCurrency = await _currencyRepo.CreateCurrencyAsync(currency);
                return CreatedAtAction(nameof(AddCurrency), createdCurrency);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        //updaing currency values 
        [HttpPut]
        public async Task<IActionResult> UpdateCurrency(CurrencyInfo currency)
        {
            try
            {
                var existingcurrencyInfo = await _currencyRepo.GetCurrencyByIdAsync
                    (currency.Idcurrency_info);
                if (existingcurrencyInfo == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        message = "record not found"
                    });
                }
                existingcurrencyInfo.Currency_details = currency.Currency_details;
                existingcurrencyInfo.Amount = currency.Amount;
                existingcurrencyInfo.Rates = currency.Rates;

                await _currencyRepo.CreateCurrencyAsync(existingcurrencyInfo);
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

        //delete currency values 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            try
            {
                var existingcurrecny = await _currencyRepo.GetCurrencyByIdAsync(id);
                if (existingcurrecny == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        message = "record not found"
                    });
                }
                await _currencyRepo.DeleteCurrencyAsync(existingcurrecny);
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

        //return all the currency values 
        [HttpGet]
        public async Task<IActionResult> GetCurrency()
        {
            try
            {
                var currency = await _currencyRepo.GetCurrencyAsync();
                return Ok(currency);
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

        //return a single currency values 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurrencyByID(int id)
        {
            try
            {
                var currency = await _currencyRepo.GetCurrencyByIdAsync(id);
                if (currency == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        message = "record not found"
                    });
                }

                return Ok(currency);
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

        //return a single currency by name 
        [HttpGet("{name}")]
        public async Task<IActionResult> GetCurrencyByName(string name)
        {
            try
            {
                var currency = await _currencyRepo.GetCurrencyByNameAsync(name);
                if (currency == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        message = "record not found"
                    });
                }

                return Ok(currency);
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
