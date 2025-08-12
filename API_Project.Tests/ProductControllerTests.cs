using API_Project.API.Controllers;
using API_Project.API.Data;
using API_Project.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace API_Project.Tests
{
    public class ProductControllerTests
    {
        private AppDbContext GetInMemoryDbContext() //Gerçek SQL çalıştırmadan, hızlı test yapılır.
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "ProductTestDb_" + Guid.NewGuid()) //Guid.NewGuid() sayesinde her test kendi bağımsız veritabanını kullanıyor → testler birbirini etkilemiyor.
                .Options;

            var context = new AppDbContext(options);
            return context;
        }
      

        [Fact]
        public void Post_WithInvalidJsonTypeForInt_ShouldFailTestWithoutException()
        {
            // Arrange - StokSayisi int bekliyor, string veriyoruz
            var invalidJson = "{\"Barkod\":\"12345\", \"ItemId\":\"ITEM1\", \"Price\":\"ITEM1\", \"StokSayisi\":\"ITEM1\"}";

            // Act
            var product = System.Text.Json.JsonSerializer.Deserialize<Product>(invalidJson);

            // Assert - StokSayisi ve Price int değilse test fail olacak
            bool stokSayisiIsValid = int.TryParse(product.StokSayisi.ToString(), out _);
            bool priceIsValid = int.TryParse(product.Price.ToString(), out _);

            stokSayisiIsValid.Should().BeTrue("StokSayisi doğru tipte olmalı");
            priceIsValid.Should().BeTrue("Price doğru tipte olmalı");
        }


        [Fact]
        public void Post_WithInvalidJsonType_ShouldThrowJsonException()
        {
            // Arrange
            var invalidJson = "{\"Barkod\":123, \"ItemId\":123, \"Price\":10}"; // ItemId int bekliyor ama string geldi

            // Act
            Action act = () =>
            {
                var product = System.Text.Json.JsonSerializer.Deserialize<Product>(invalidJson);
            };

            // Assert
            act.Should().Throw<System.Text.Json.JsonException>();
        }


        [Fact]
        public void GetAllProducts_ShouldReturnAllProducts() // Test metotları genellikle "Should" ile başlar, bu da ne beklediğimizi açıklar.
        {
            // Arrange
            var context = GetInMemoryDbContext();
            context.Products.AddRange(
                new Product { Barkod = "12345", ItemId = "ITEM1", Price = 10, StokSayisi = 5, Beden = "M", Renk = "Gri" },
                new Product { Barkod = "67890", ItemId = "ITEM2", Price = 20, StokSayisi = 3, Beden = "S", Renk = "Mor" }
            );
            context.SaveChanges();

            var controller = new ProductController(context);

            // Act
            var result = controller.GetAllProducts() as OkObjectResult; // OkObjectResult, HTTP 200 durum kodu ile dönen bir nesnedir. && "as" anahtar kelimesi, sonucu OkObjectResult türüne dönüştürür

            // Assert
            result.Should().NotBeNull();
            var products = result.Value as List<Product>; // Value, OkObjectResult içindeki gerçek veriyi tutar.
            products.Should().HaveCount(2);
        }

        [Fact]
        public void CreateProduct_ShouldAddProduct() // Yeni ürün ekleme işlemi için test
        {
            var context = GetInMemoryDbContext();
            var controller = new ProductController(context);
            var product = new Product
            {
                Barkod = "11111",
                ItemId = "123ASD",
                Price = 15,
                StokSayisi = 10,
                Beden = "M",
                Renk = "Kırmızı"
            };

            var result = controller.CreateProduct(product) as OkObjectResult;

            result.Should().NotBeNull();
            context.Products.Count().Should().Be(1);
        }

        [Fact]
        public void UpdateProduct_ShouldModifyExistingProduct() // Ürün güncelleme işlemi için test
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var product = new Product
            {
                Barkod = "11111",
                ItemId = "ITEMX",
                Price = 15,
                StokSayisi = 10,
                Beden = "M",
                Renk = "Kırmızı"
            };
            context.Products.Add(product);
            context.SaveChanges();

            var controller = new ProductController(context);

            var updated = new Product
            {
                Id = product.Id,
                Barkod = "22222",
                ItemId = "ITEMY",
                Price = 25,
                StokSayisi = 20,
                Renk = "Mavi",
                Beden = "XL"
            };

            // Act
            var result = controller.UpdateProduct(updated) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();

            var dbProduct = context.Products.First();
            dbProduct.Barkod.Should().Be("22222");
            dbProduct.ItemId.Should().Be("ITEMY");
            dbProduct.Price.Should().Be(25);
            dbProduct.StokSayisi.Should().Be(20);
            dbProduct.Renk.Should().Be("Mavi");
            dbProduct.Beden.Should().Be("XL");
        }


        [Fact]
        public void DeleteProduct_ShouldRemoveProduct()
        {
            var context = GetInMemoryDbContext();
            var product = new Product { Barkod = "11111", ItemId = "ITEMX", Price = 15, StokSayisi = 10, Beden = "M", Renk = "Kırmızı" };
            context.Products.Add(product);
            context.SaveChanges();

            var controller = new ProductController(context);

            var result = controller.DeleteProduct(product.Id) as OkObjectResult;

            result.Should().NotBeNull();
            context.Products.Count().Should().Be(0);
        }
    }
}
