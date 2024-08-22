using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
    public class SellerRepository : ISellerRepository
    {
        DataContext _context;
        public SellerRepository(DataContext context)
        {
            _context = context;
        }

        public Seller Save(Seller seller)
        {
            _context.Add(seller);
            _context.SaveChanges();
            return seller;
        }

        public Seller Update(Seller seller)
        {
            _context.Entry(seller).State = EntityState.Modified;
            _context.SaveChanges();
            return seller;
        }

        public List<Seller> List()
        {
            return _context.Sellers
            .Include(s => s.Products)
            .ToList();
        }

        public void Delete(Seller seller)
        {
            _context.Users.Remove(seller);
            _context.SaveChanges();
        }

        public Seller GetById(int Id)
        {
            return _context.Sellers.Find(Id);
        }
    }
}