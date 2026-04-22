using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly AppDbContext _context;

        public SalesRecordService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> findByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate);
            }

            return await result
            .Include(x => x.Seller)
            .Include(x => x.Seller.Department)
            .OrderByDescending(x => x.Date)
            .ToListAsync();
        }
    }
}