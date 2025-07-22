using MellonBank.Models;
using MellonBank.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MellonBank.Views.Currencies
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyRepository _currencyRepository;
        public CurrencyController(CurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Currency>>> GetRates()
        {
            var rates = await _currencyRepository.GetRates();

            if (rates == null || rates.Any() == false)
                return NotFound();

            return Ok(rates);
        }
    }
}
