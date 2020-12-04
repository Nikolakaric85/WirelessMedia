using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WirelessMedia.Controllers;

namespace WirelessMedia.Models
{
    public class ProductRepository: IProductRepository
    {
        private readonly AppDbContext context;
        public ProductRepository(AppDbContext context)
        {
            this.context = context;
        }

       IEnumerable<Products> IProductRepository.allProducts => context.Products;

        public Products Add(Products products)
        {
            context.Products.Add(products);
            context.SaveChanges();
            return products;
        }

        public Products Update(Products products)
        {
            var _products = context.Products.Attach(products);
            _products.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return products;
        }

        public Products Delete(int id)
        {
            Products productsToDelete = context.Products.Find(id);
            context.Products.Remove(productsToDelete);
            context.SaveChanges();
            return productsToDelete;
                
        }

    }
}
