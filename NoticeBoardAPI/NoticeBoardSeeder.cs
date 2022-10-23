using NoticeBoardAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI
{
    public class NoticeBoardSeeder
    {
        private readonly NoticeBoardDbContext _dbContext;

        public NoticeBoardSeeder(NoticeBoardDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Categories.Any())
                {
                    var categories = GetCategories();
                    _dbContext.Categories.AddRange(categories);
                    _dbContext.SaveChanges();
                }
            }

        }

        private IEnumerable<Category> GetCategories()
        {
            var categories = new List<Category>()
            {
                new Category() { Name = "Job Offers"},
                new Category() { Name = "Rent"},
                new Category() { Name = "Sell"},
                new Category() { Name = "Buy"},
                new Category() { Name = "Exchange"}

            };

            return categories;
        }
    }
}
