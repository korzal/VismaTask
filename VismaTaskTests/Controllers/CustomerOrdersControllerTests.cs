namespace VismaTaskTests.Controllers
{
    using VismaTask.Controllers;
    using System;
    using Xunit;
    using System.Collections.Generic;
    using VismaTask.Models;

    
    public class CustomerOrdersControllerTests
    {
        private CustomerOrdersController _testClass;
        private RestaurantMenuController _menu;

        public CustomerOrdersControllerTests()
        {
            List<RestaurantStock> stockList = new List<RestaurantStock>
            {
                new RestaurantStock(1, "stockItem1", 10, "kg", 0.3),
                new RestaurantStock(2, "stockItem2", 10, "kg", 0.3),
                new RestaurantStock(3, "stockItem3", 10, "kg", 0.3),
                new RestaurantStock(4, "stockItem4", 10, "kg", 0.3)
            };

            List<RestaurantMenu> menuList = new List<RestaurantMenu>
            {
                new RestaurantMenu(1, "menuItem1", stockList),
                new RestaurantMenu(2, "menuItem2", stockList),
                new RestaurantMenu(3, "menuItem3", stockList),
            };

            RestaurantStockController _stock = new RestaurantStockController();
            _stock.StockList.AddRange(stockList);

            _menu = new RestaurantMenuController(_stock);
            _menu.MenuList.AddRange(menuList);

            _testClass = new CustomerOrdersController(_menu);
        }

        [Fact]
        public void CanConstruct()
        {
            var instance = new CustomerOrdersController(_menu);
            Assert.NotNull(instance);
        }

        [Fact]
        public void CannotConstructWithNullMenu()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomerOrdersController(default(RestaurantMenuController)));
        }

        [Fact]
        public void CanCallCreateOrder()
        {
            var menuList = new List<RestaurantMenu>();
            var result = _testClass.CreateOrder(menuList);
            Assert.True(result);
        }

        [Fact]
        public void CannotCallCreateOrderWithNullMenuList()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateOrder(default(List<RestaurantMenu>)));
        }

        [Fact]
        public void CanGetOrderList()
        {
            Assert.IsType<List<CustomerOrder>>(_testClass.OrderList);
        }

        [Fact]
        public void MenuIsInitializedCorrectly()
        {
            Assert.Equal(_menu, _testClass.Menu);
        }
    }
}