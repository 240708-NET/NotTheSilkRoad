using Models;

namespace Repository
{
    public class CategoryRepository : IRepository<Category>
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

        public Category Update(int Id, Category category)
        {
            Category categoryToUpdate = _context.Categories.Find(Id);

            if (category != null)
            {
                categoryToUpdate.Description = category.Description;
                _context.SaveChanges();
                return categoryToUpdate;
            }
            return null;
        }

        public List<Category> List()
        {
            return _context.Categories.ToList();
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