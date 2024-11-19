﻿using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        void save();
        void Update(Product obj);
    }
}