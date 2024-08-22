using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
    public class ItemRepository : IItemRepository
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

        public Item Update(Item item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }

        public List<Item> List()
        {
            return _context.Items
            .Include(i => i.Product)
            .ToList();
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