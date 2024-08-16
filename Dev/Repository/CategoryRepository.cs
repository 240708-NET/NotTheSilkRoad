using Models;

namespace Repository
{
    public class CategoryRepository : IRepository<Category>
    {
        DataContext _context;
        public CategoryRepositor(DataContext context)
        {
            _context = context;
        }

        Category Save(Category category)
        {
            _context.Add(category);
            _context.SaveChanges();
            return category;
        }

        Category Update(int Id, Category category)
        {
            Category categoryToUpdate = _context.Category.Find(Id);

            if (order != null)
            {
                categoryToUpdate.Description = category.Description;
                _context.SaveChanges();
                return _context.Category.Find(Id);
            }
            return null;
        }

        List<Category> List()
        {
            return _context.Categories.ToList();
        }

        void Delete(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        Category GetById(int Id)
        {
            return _context.Categories.Find(Id);
        }
    }
}