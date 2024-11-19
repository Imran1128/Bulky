using Bulky.DataAccess.Data;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository :Repository<Product>, IProductRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public ProductRepository(ApplicationDBContext db) :base(db) 
        {
            _dbContext = db;
        }

        public void save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Product obj)
        {
            _dbContext.Update(obj);
        }
    }
}
