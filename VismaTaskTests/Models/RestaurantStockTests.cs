namespace VismaTaskTests.Models
{
    using VismaTask.Models;
    using System;
    using Xunit;

    public class RestaurantStockTests
    {
        private RestaurantStock _testClass;
        private int _iD;
        private string _name;
        private int _portionCount;
        private string _unit;
        private double _portionSize;

        public RestaurantStockTests()
        {
            _iD = 1189643973;
            _name = "TestValue924526032";
            _portionCount = 167862104;
            _unit = "TestValue1583570175";
            _portionSize = 175096571.76;
            _testClass = new RestaurantStock(_iD, _name, _portionCount, _unit, _portionSize);
        }

        [Fact]
        public void CanConstruct()
        {
            var instance = new RestaurantStock(_iD, _name, _portionCount, _unit, _portionSize);
            Assert.NotNull(instance);
            instance = new RestaurantStock(_name);
            Assert.NotNull(instance);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotConstructWithInvalidName(string value)
        {
            Assert.Throws<ArgumentNullException>(() => new RestaurantStock(718072937, value, 217105587, "TestValue259013867", 2022796726.83));
            Assert.Throws<ArgumentNullException>(() => new RestaurantStock(value));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotConstructWithInvalidUnit(string value)
        {
            Assert.Throws<ArgumentNullException>(() => new RestaurantStock(80750589, "TestValue102253474", 802313747, value, 1322949233.43));
        }

        [Fact]
        public void CanCallSetName()
        {
            var name = "TestValue801052334";
            _testClass.SetName(name);
            Assert.Equal(_testClass.Name, name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallSetNameWithInvalidName(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.SetName(value));
        }

        [Fact]
        public void CanCallSetUnit()
        {
            var unit = "TestValue717937559";
            _testClass.SetUnit(unit);
            Assert.Equal(_testClass.Unit, unit);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallSetUnitWithInvalidUnit(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.SetUnit(value));
        }

        [Fact]
        public void IDIsInitializedCorrectly()
        {
            Assert.Equal(_iD, _testClass.ID);
        }

        [Fact]
        public void NameIsInitializedCorrectly()
        {
            _testClass = new RestaurantStock(_iD, _name, _portionCount, _unit, _portionSize);
            Assert.Equal(_name, _testClass.Name);
            _testClass = new RestaurantStock(_name);
            Assert.Equal(_name, _testClass.Name);
        }

        [Fact]
        public void PortionCountIsInitializedCorrectly()
        {
            Assert.Equal(_portionCount, _testClass.PortionCount);
        }

        [Fact]
        public void CanSetAndGetPortionCount()
        {
            var testValue = 1550486778;
            _testClass.PortionCount = testValue;
            Assert.Equal(testValue, _testClass.PortionCount);
        }

        [Fact]
        public void UnitIsInitializedCorrectly()
        {
            Assert.Equal(_unit, _testClass.Unit);
        }

        [Fact]
        public void PortionSizeIsInitializedCorrectly()
        {
            Assert.Equal(_portionSize, _testClass.PortionSize);
        }

        [Fact]
        public void CanSetAndGetPortionSize()
        {
            var testValue = 1669967310.33;
            _testClass.PortionSize = testValue;
            Assert.Equal(testValue, _testClass.PortionSize);
        }
    }
}