namespace VismaTaskTests.Controllers
{
    using VismaTask.Controllers;
    using System;
    using System.Linq;
    using Xunit;
    using VismaTask.Models;
    using System.Collections.Generic;
    using Moq;

    public class RestaurantStockControllerTests
    {
        private RestaurantStockController _testClass;

        public RestaurantStockControllerTests()
        {
            _testClass = new RestaurantStockController();
        }


        [Fact]
        public void CanConstruct()
        {
            var instance = new RestaurantStockController();
            Assert.NotNull(instance);
        }

        [Fact]
        public void CanCallCreateStock()
        {
            var name = "TestValue1547037693";
            var portionCount = 188352818;
            var unit = "TestValue632545939";
            var portionSize = 1088176251.69;
            var result = _testClass.CreateStock(name, portionCount, unit, portionSize);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateStockWithInvalidName(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateStock(value, 1415846948, "TestValue782270163", 1519764712.29));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateStockWithInvalidUnit(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateStock("TestValue1363609080", 657433508, value, 1054570957.11));
        }

        [Fact]
        public void CreateStockPerformsMapping()
        {
            var name = "TestValue380729978";
            var portionCount = 1220964967;
            var unit = "TestValue1855631376";
            var portionSize = 339915997.08;
            var result = _testClass.CreateStock(name, portionCount, unit, portionSize);
            Assert.Equal(name, result.Name);
            Assert.Equal(portionCount, result.PortionCount);
            Assert.Equal(unit, result.Unit);
            Assert.Equal(portionSize, result.PortionSize);
        }

        [Fact]
        public void CanCallUpdateStock()
        {
            var id = 1623614257;
            var updatedStockItem = new RestaurantStock(id, "TestValue114855862", 10, "kg", 0.3);
            var newStockItem = new RestaurantStock(id, "NewItem", 20, "kg", 5.1);
            _testClass.StockList.Add(newStockItem);
            
            var result = _testClass.UpdateStock(id, updatedStockItem);
            var expected = _testClass.StockList.FirstOrDefault(s => s.ID == id);
            Assert.Equal(result, expected);
        }

        [Fact]
        public void CannotCallUpdateStockWithNullUpdatedStockItem()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.UpdateStock(1785938551, null));
        }

        [Fact]
        public void CanCallCanDeduceFromStock()
        {
            var itemsToDeduce = new List<RestaurantStock>();
            var result = _testClass.CanDeduceFromStock(itemsToDeduce);
            Assert.True(result);
        }

        [Fact]
        public void CannotCallCanDeduceFromStockWithNullItemsToDeduce()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CanDeduceFromStock(default(List<RestaurantStock>)));
        }

        [Fact]
        public void CanCallDeduceFromStock()
        {
            var itemsToDeduce = new List<RestaurantStock>();
            var id = 915982367;
            var portionCount = 20;
            var newStockItem = new RestaurantStock(id, "NewItem", portionCount, "kg", 5.1);
            _testClass.StockList.Add(newStockItem);
            itemsToDeduce.Add(newStockItem);

            _testClass.DeduceFromStock(itemsToDeduce);
            var expected = _testClass.StockList.FirstOrDefault(s => s.ID == id);
            Assert.True(expected.PortionCount == portionCount - 1);
        }

        [Fact]
        public void CannotCallDeduceFromStockWithNullItemsToDeduce()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.DeduceFromStock(default(List<RestaurantStock>)));
        }

        [Fact]
        public void CanCallDeleteStock()
        {
            var id = 2099083611;
            var newStockItem = new RestaurantStock(id, "NewItemToDelete", 20, "kg", 5.1);
            _testClass.StockList.Add(newStockItem);
            var result = _testClass.DeleteStock(id);
            var expected = _testClass.StockList.FirstOrDefault(s => s.ID == result.ID);
            Assert.Equal(result, newStockItem);
            Assert.Null(expected);
        }

        [Fact]
        public void CanGetStockList()
        {
            Assert.IsType<List<RestaurantStock>>(_testClass.StockList);
        }
    }
}