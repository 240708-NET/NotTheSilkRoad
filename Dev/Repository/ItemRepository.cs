using Models;

namespace Repository
{
    public class ItemRepository : IRepository<Item>
    {
        DataContext _context;
        public ItemRepository(DataContext context)
        {
            _context = context;
        }

        public Item Save(Item item)
        {
            _context.Add(item);
            _context.SaveChanges();
            return item;
        }

        public Item Update(int Id, Item item)
        {
            Item itemToUpdate = _context.Items.Find(Id);

            if (item != null)
            {
                itemToUpdate.Product = item.Product;
                itemToUpdate.Quantity = item.Quantity;
                itemToUpdate.Price = item.Price;
                _context.SaveChanges();
                return itemToUpdate;
            }
            return null;
        }

        public List<Item> List()
        {
            return _context.Items.ToList();
        }

        public void Delete(Item item)
        {
            _context.Items.Remove(item);
            _context.SaveChanges();
        }

        public Item GetById(int Id)
        {
            return _context.Items.Find(Id);
        }
    }
}