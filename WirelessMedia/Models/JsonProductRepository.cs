
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WirelessMedia.Controllers;

namespace WirelessMedia.Models
{
    public class JsonProductRepository : IProductRepository
    {
        List<Products> products = new List<Products>();


        public JsonProductRepository()
        {
                      
            string jsonString = File.ReadAllText("products.json");
            products = JsonSerializer.Deserialize<List<Products>>(jsonString);

        }

        IEnumerable<Products> IProductRepository.allProducts => products;

        public Products Add(Products addProduct)
        {
            string jsonString = File.ReadAllText("products.json");
            products = JsonSerializer.Deserialize<List<Products>>(jsonString);

            if (products.Count == 0)
            {
                products.Add(addProduct);
            }
            else
            {
                int id = products.Max(p => p.Id);                
                
                addProduct.Id = ++id;
                products.Add(addProduct);
            }

            jsonString = JsonSerializer.Serialize(products);
            File.WriteAllText("products.json", jsonString);
            return addProduct;
        }

        public Products Delete(int id)
        {
           
            products.Remove((products.Where(p => p.Id == id)).FirstOrDefault());

            string jsonString = JsonSerializer.Serialize(products);
            File.WriteAllText("products.json", jsonString);

            return null ;
        }

        public Products Update(Products productToUpdate)
        {
            string jsonString = File.ReadAllText("products.json");
            products = JsonSerializer.Deserialize<List<Products>>(jsonString);

            products.Remove((products.Where(p => p.Id == productToUpdate.Id)).FirstOrDefault());

            products.Add(productToUpdate);

            jsonString = JsonSerializer.Serialize(products);
            File.WriteAllText("products.json", jsonString);

            return productToUpdate;
        }

    }
}

