using MellonBank.Data;
using MellonBank.Models;
using Microsoft.EntityFrameworkCore;

namespace MellonBank.Repositories
{
    public class CurrencyRepository
    {
        private readonly ApplicationDbContext _db;
        public CurrencyRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Currency>> GetRates()
        {
            return await _db.Currencies.ToListAsync();
        }
    }
}
