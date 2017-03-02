using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkingWithVisualStudio.Models;
using Xunit;

namespace WorkingWithVisualStudio.Tests
{
    public class ProductTests
    {
        [Fact]
        public void CanChangeProductName()
        {
            var p = new Product { Name = "Test", Price = 10M };
            p.Name = "New Name";
            Assert.Equal("New Name", p.Name);
        }

        [Fact]
        public void CanChangeProductPrice()
        {
            var p = new Product { Name = "Test", Price = 10M };
            p.Price = 200M;
            Assert.Equal(200M, p.Price);
        }


    }
}
