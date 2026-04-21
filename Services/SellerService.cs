using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly AppDbContext _context;

        public SellerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context
            .Seller
            .Include(seller => seller.Department)
            .ToListAsync();
        }

        public async Task InsertAsync(Seller seller)
        {
            await _context.AddAsync(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context
            .Seller
            .Include(x => x.Department)
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var seller = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(seller);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == id);
            if (!hasAny)
            {
               throw new NotFoundException("Id not found"); 
            }

            var seller = await FindByIdAsync(id);
            seller.Name = obj.Name;
            seller.Email = obj.Email;
            seller.BirthDate = obj.BirthDate;
            seller.BaseSalary = obj.BaseSalary;
            seller.DepartmentId = obj.DepartmentId;

            try
            {
                _context.Seller.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}