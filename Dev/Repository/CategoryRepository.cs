using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public Category Save(Category category)
        {
            _context.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
            return category;
        }

        public List<Category> List()
        {
            return _context.Categories
            .Include(c => c.Products)
            .ToList();
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public Category GetById(int Id)
        {
            return _context.Categories.Find(Id);
        }
    }
}