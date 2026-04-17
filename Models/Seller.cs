namespace SalesWebMvc.Models
{
    public class Seller
    {
        public Seller()
        {
        }

        public Seller(string name, string email, double baseSalary) 
        {
            Name = name;
            Email = email;
            BaseSalary = baseSalary;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }

        public Department Department { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public void AddSale(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSale(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales
            .Where(sale => sale.Date.CompareTo(initial) >= 0 && sale.Date.CompareTo(final) <= 0)
            .Sum(sale => sale.Amount);
        }
    }
}