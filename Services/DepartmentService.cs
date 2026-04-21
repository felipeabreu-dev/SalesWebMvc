using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly AppDbContext _context;

        public DepartmentService(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Department>> FindAllAsync()
        {
            return _context
            .Department
            .OrderBy(x => x.Name)
            .ToListAsync();
        }
    }
}