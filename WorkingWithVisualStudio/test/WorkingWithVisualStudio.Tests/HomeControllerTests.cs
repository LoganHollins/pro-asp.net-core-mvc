﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkingWithVisualStudio.Controllers;
using WorkingWithVisualStudio.Models;
using Xunit;
using Moq;

namespace WorkingWithVisualStudio.Tests
{
    class ModelCompleteFakeRepository : IRepository
    {
        public IEnumerable<Product> Products { get; set; }

        //public IEnumerable<Product> Products { get; } = new Product[] {
        //    new Product { Name = "P1", Price = 275M },
        //    new Product { Name = "P2", Price = 48.95M },
        //    new Product { Name = "P3", Price = 19.50M },
        //    new Product { Name = "P3", Price = 34.95M }
        //};

        public void AddProduct(Product p)
        {
            // do nothing - not required for test
        }
    }



    class ModelCompleteFakeRepositoryPricesUnder50 : IRepository
    {
        public IEnumerable<Product> Products { get; } = new Product[] {
            new Product { Name = "P1", Price = 5M },
            new Product { Name = "P2", Price = 48.95M },
            new Product { Name = "P3", Price = 19.50M },
            new Product { Name = "P3", Price = 34.95M }
        };
        public void AddProduct(Product p)
        {
            // do nothing - not required for test
        }
    }

    public class HomeControllerTests
    {
        [Theory]
        [ClassData(typeof(ProductTestData))]
        public void IndexActionModelIsCompleted(Product[] products)
        {
            var mock = new Mock<IRepository>();
            mock.SetupGet(m => m.Products).Returns(products);
            var controller = new HomeController
            {
                Repository = mock.Object
            };
            
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            Assert.Equal(controller.Repository.Products, model, Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name && p1.Price == p2.Price));
        }
        class PropertyOnceFakeRepository : IRepository
        {
            public int PropertyCounter { get; set; } = 0;

            public IEnumerable<Product> Products
            {
                get
                {
                    PropertyCounter++;
                    return new[] { new Product { Name = "P1", Price = 100 } };
                }
            }

            public void AddProduct(Product p)
            {
                //Nothing
            }
        }

        [Fact]
        public void RepositoryPropertyCalledOnce()
        {
            //Arrange
            var mock = new Mock<IRepository>();
            mock.SetupGet(m => m.Products)
                .Returns(new[] { new Product { Name = "P1", Price = 100 } });

            var repo = new PropertyOnceFakeRepository();
            var controller = new HomeController { Repository = mock.Object };

            // Act
            var result = controller.Index();

            // Assert
            mock.VerifyGet(m => m.Products, Times.Once);
        }

        [Fact]
        public void IndexActionModelIsCompletePricesUnder50()
        {
            // Arrange
            var controller = new HomeController();
            controller.Repository = new ModelCompleteFakeRepositoryPricesUnder50();

            // Act
            var model = (controller.Index() as ViewResult)?.ViewData.Model
            as IEnumerable<Product>;

            // Assert
            Assert.Equal(controller.Repository.Products, model,
            Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name
            && p1.Price == p2.Price));
        }
    }
}
