using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkingWithVisualStudio.Models
{
    public class SimpleRepository
    {
        private static SimpleRepository _sharedRepository = new SimpleRepository();
        private Dictionary<string, Product> _products = new Dictionary<string, Product>();
        
        public static SimpleRepository SharedRepository => _sharedRepository;

        public SimpleRepository()
        {
            var initialItems = new[]
            {
                new Product { Name = "Kayak", Price = 275M },
                new Product { Name = "Lifejacket", Price = 48.95M },
                new Product { Name = "Soccer ball", Price = 19.50M },
                new Product { Name = "Corner flag", Price = 34.95M }
            };

            foreach(var i in initialItems)
            {
                AddProduct(i);
            }
            _products.Add("Error", null);
        }

        public IEnumerable<Product> Products => _products.Values;
        public void AddProduct(Product p) => _products.Add(p.Name, p);
    }
}
