namespace VismaTaskTests.Controllers
{
    using VismaTask.Controllers;
    using System;
    using Xunit;
    using System.Collections.Generic;
    using VismaTask.Models;
    using System.Linq;

    public class RestaurantMenuControllerTests
    {
        private RestaurantMenuController _testClass;
        private RestaurantStockController _stock;

        public RestaurantMenuControllerTests()
        {
            List<RestaurantStock> stockList = new List<RestaurantStock>
            {
                new RestaurantStock(1, "item1", 10, "kg", 0.3),
                new RestaurantStock(2, "item2", 10, "kg", 0.3),
                new RestaurantStock(3, "item3", 10, "kg", 0.3),
                new RestaurantStock(4, "item4", 10, "kg", 0.3)
            };

            _stock = new RestaurantStockController();
            _stock.StockList.AddRange(stockList);
            _testClass = new RestaurantMenuController(_stock);
        }

        [Fact]
        public void CanConstruct()
        {
            var instance = new RestaurantMenuController(_stock);
            Assert.NotNull(instance);
        }

        [Fact]
        public void CannotConstructWithNullStock()
        {
            Assert.Throws<ArgumentNullException>(() => new RestaurantMenuController(default(RestaurantStockController)));
        }

        [Fact]
        public void CanCallCreateMenuItem()
        {
            var name = "TestValue1436656470";
            var ingredients = new List<RestaurantStock>();

            var result = _testClass.CreateMenuItem(name, ingredients);
            var expected = _testClass.MenuList.FirstOrDefault(m => m.ID == result.ID);

            Assert.Equal(result, expected);
        }

        [Fact]
        public void CannotCallCreateMenuItemWithNullIngredients()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateMenuItem("TestValue1323900415", default(List<RestaurantStock>)));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateMenuItemWithInvalidName(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateMenuItem(value, new List<RestaurantStock>()));
        }

        [Fact]
        public void CreateMenuItemPerformsMapping()
        {
            var name = "TestValue2086597016";
            var ingredients = new List<RestaurantStock>();

            var result = _testClass.CreateMenuItem(name, ingredients);

            Assert.Equal(name, result.Name);
        }

        [Fact]
        public void CanCallUpdateMenuItem()
        {
            var id = 1359805206;
            var updatedMenuItem = new RestaurantMenu(id, "TestValue1018397393", new List<RestaurantStock>());
            var newMenuItem = new RestaurantMenu(id, "MenuItemToUpdate", new List<RestaurantStock>());
            _testClass.MenuList.Add(newMenuItem);

            var result = _testClass.UpdateMenuItem(id, updatedMenuItem);
            var expected = _testClass.MenuList.FirstOrDefault(m => m.ID == result.ID);

            Assert.Equal(result.ID, expected.ID);
            Assert.Equal(result.Name, expected.Name);
            Assert.Equal(result.Products, expected.Products);
        }

        [Fact]
        public void CannotCallUpdateMenuItemWithNullUpdatedMenuItem()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.UpdateMenuItem(1400479998, default(RestaurantMenu)));
        }

        [Fact]
        public void CanCallDeleteMenuItem()
        {
            var id = 292441864;
            RestaurantMenu newMenuItem = new RestaurantMenu(id, "ItemToDelete", new List<RestaurantStock>());
            _testClass.MenuList.Add(newMenuItem);

            var result = _testClass.DeleteMenuItem(id);

            var expected = _testClass.MenuList.FirstOrDefault(m => m.ID == result.ID);
            Assert.Null(expected);
        }

        [Fact]
        public void CanGetMenuList()
        {
            Assert.IsType<List<RestaurantMenu>>(_testClass.MenuList);
        }

        [Fact]
        public void StockIsInitializedCorrectly()
        {
            Assert.Equal(_stock, _testClass.Stock);
        }
    }
}