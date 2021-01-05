namespace VismaTaskTests.Models
{
    using VismaTask.Models;
    using System;
    using Xunit;
    using System.Collections.Generic;

    public class CustomerOrderTests
    {
        private CustomerOrder _testClass;
        private int _iD;
        private DateTime _timeStamp;
        private List<RestaurantMenu> _menuItems;

        public CustomerOrderTests()
        {
            _iD = 715711756;
            _timeStamp = new DateTime(1783697321);
            _menuItems = new List<RestaurantMenu>();
            _testClass = new CustomerOrder(_iD, _timeStamp, _menuItems);
        }

        [Fact]
        public void CanConstruct()
        {
            var instance = new CustomerOrder(_iD, _timeStamp, _menuItems);
            Assert.NotNull(instance);
        }

        [Fact]
        public void CannotConstructWithNullMenuItems()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomerOrder(2117097238, new DateTime(795641092), default(List<RestaurantMenu>)));
        }

        [Fact]
        public void CanCallSetTimeStamp()
        {
            var timeStamp = new DateTime(1278069646);
            _testClass.SetTimeStamp(timeStamp);
            Assert.Equal(timeStamp, _testClass.TimeStamp);
        }

        [Fact]
        public void CanCallSetMenuItems()
        {
            var stockItems = new List<RestaurantStock>
            {
                new RestaurantStock("newStock")
            };
            var menuItems = new List<RestaurantMenu>
            {
                new RestaurantMenu(1, "newItem", stockItems)
            };
            _testClass.SetMenuItems(menuItems);
            Assert.NotEmpty(_testClass.MenuItems);
        }

        [Fact]
        public void CannotCallSetMenuItemsWithNullItems()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.SetMenuItems(default(List<RestaurantMenu>)));
        }

        [Fact]
        public void IDIsInitializedCorrectly()
        {
            Assert.Equal(_iD, _testClass.ID);
        }

        [Fact]
        public void CanSetAndGetID()
        {
            var testValue = 1928738590;
            _testClass.ID = testValue;
            Assert.Equal(testValue, _testClass.ID);
        }

        [Fact]
        public void TimeStampIsInitializedCorrectly()
        {
            Assert.Equal(_timeStamp, _testClass.TimeStamp);
        }

        [Fact]
        public void MenuItemsIsInitializedCorrectly()
        {
            Assert.Equal(_menuItems, _testClass.MenuItems);
        }
    }
}