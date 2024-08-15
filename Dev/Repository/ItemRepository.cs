using Models;

namespace Repository
{
    public class ItemRepository : ItemRepository<Item>
    {
        DataContext _context;
        public ItemRepository(DataContext context)
        {
            _context = context;
        }

        Item Save(Item item)
        {
            _context.Add(item);
            _context.SaveChanges();
            return item;
        }

        Item Update(int Id, Item item)
        {
            Item itemToUpdate = _context.Item.Find(Id);

            if (item != null)
            {
                itemToUpdate.Product = item.Product;
                itemToUpdate.Quantity = item.Quantity;
                itemToUpdate.Price = item.Price;
                _context.SaveChanges();
                return _context.Item.Find(Id);
            }
            return null;
        }

        List<Item> List()
        {
            return _context.Items.ToList();
        }

        void Delete(Item item)
        {
            _context.Items.Remove(item);
            _context.SaveChanges();
        }

        Item GetById(int Id)
        {
            return _context.Items.Find(Id);
        }
    }
}