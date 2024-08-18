using Models;

namespace Repository
{
    public class SellerRepository : IRepository<Seller>
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

        public Seller Update(int Id, Seller seller)
        {
            Seller sellerToUpdate = _context.Sellers.Find(Id);

            if (seller != null)
            {
                sellerToUpdate.Name = seller.Name;
                sellerToUpdate.Email = seller.Email;
                sellerToUpdate.Password = seller.Password;
                _context.SaveChanges();
                return sellerToUpdate;
            }
            return null;
        }

        public List<Seller> List()
        {
            return _context.Sellers.ToList();
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