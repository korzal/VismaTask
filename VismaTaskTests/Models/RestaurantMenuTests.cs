namespace VismaTaskTests.Models
{
    using VismaTask.Models;
    using System;
    using Xunit;
    using System.Collections.Generic;

    public class RestaurantMenuTests
    {
        private RestaurantMenu _testClass;
        private int _iD;
        private string _name;
        private List<RestaurantStock> _products;

        public RestaurantMenuTests()
        {
            _iD = 1753651214;
            _name = "TestValue121694037";
            _products = new List<RestaurantStock>();
            _testClass = new RestaurantMenu(_iD, _name, _products);
        }

        [Fact]
        public void CanConstruct()
        {
            var instance = new RestaurantMenu(_iD, _name, _products);
            Assert.NotNull(instance);
        }

        [Fact]
        public void CannotConstructWithNullProducts()
        {
            Assert.Throws<ArgumentNullException>(() => new RestaurantMenu(1522118326, "TestValue1964622064", default(List<RestaurantStock>)));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotConstructWithInvalidName(string value)
        {
            Assert.Throws<ArgumentNullException>(() => new RestaurantMenu(1394100855, value, new List<RestaurantStock>()));
        }

        [Fact]
        public void CanCallSetProducts()
        {
            var products = new List<RestaurantStock>
            {
                new RestaurantStock("newStock")
            };

            _testClass.SetProducts(products);

            Assert.NotEmpty(_testClass.Products);
        }

        [Fact]
        public void CannotCallSetProductsWithNullProducts()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.SetProducts(default(List<RestaurantStock>)));
        }


        [Fact]
        public void IDIsInitializedCorrectly()
        {
            Assert.Equal(_iD, _testClass.ID);
        }

        [Fact]
        public void CanSetAndGetID()
        {
            var testValue = 1213889096;
            _testClass.ID = testValue;
            Assert.Equal(testValue, _testClass.ID);
        }

        [Fact]
        public void NameIsInitializedCorrectly()
        {
            Assert.Equal(_name, _testClass.Name);
        }

        [Fact]
        public void CanSetAndGetName()
        {
            var testValue = "TestValue1238969310";
            _testClass.Name = testValue;
            Assert.Equal(testValue, _testClass.Name);
        }

        [Fact]
        public void ProductsIsInitializedCorrectly()
        {
            Assert.Equal(_products, _testClass.Products);
        }
    }
}