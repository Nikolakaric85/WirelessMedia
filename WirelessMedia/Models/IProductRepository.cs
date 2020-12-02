using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WirelessMedia.Models;

namespace WirelessMedia.Controllers
{
    public interface IProductRepository
    {
        IEnumerable<Products> allProducts { get; }

        Products Add(Products products);

        Products Update(Products products);

        Products Delete(int id);
    }
}
