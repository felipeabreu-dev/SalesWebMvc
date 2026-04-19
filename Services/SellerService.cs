using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly AppDbContext _context;

        public SellerService(AppDbContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context
            .Seller
            .Include(seller => seller.Department)
            .ToList();
        }

        public void Insert(Seller seller)
        {
            _context.Add(seller);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context
            .Seller
            .Include(x => x.Department)
            .FirstOrDefault(x => x.Id == id);
        }

        public void Remove(int id)
        {
            var seller = _context.Seller.Find(id);
            _context.Seller.Remove(seller);
            _context.SaveChanges();
        }
    }
}