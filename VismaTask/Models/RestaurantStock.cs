using System;
using System.Collections.Generic;
using System.Text;

namespace VismaTask.Models
{
    class RestaurantStock
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public int PortionCount { get; set; }
        public string Unit { get; private set; }
        public double PortionSize { get; set; }

        public RestaurantStock(int ID, string Name, int PortionCount, string Unit, double PortionSize)
        {
            if(Name == null || Name == "" || Name.StartsWith(" "))
            {
                throw new ArgumentNullException(nameof(Name));
            }

            if(Unit == null || Unit == "" || Unit.StartsWith(" "))
            {
                throw new ArgumentNullException(nameof(Unit));
            }

            this.ID = ID;
            this.Name = Name;
            this.PortionCount = PortionCount;
            this.Unit = Unit;
            this.PortionSize = PortionSize;
        }

        //default constructor when creating a new stock item, for ingredients required for a new menu item
        public RestaurantStock(string name)
        {
            if (name == null || name == "" || name.StartsWith(" "))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            PortionCount = 10;
            Unit = "kg";
            PortionSize = 0.3;
        }

        public void SetName(string name)
        {
            if(name == null || name == "" || name.StartsWith(" "))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Name = name;
        }

        public void SetUnit(string unit)
        {
            if(unit == null || unit == "" || unit.StartsWith(" "))
            {
                throw new ArgumentNullException(nameof(unit));
            }
            Unit = unit;
        }

        public override string ToString()
        {
            return String.Format("|{0,5}|{1,20}|{2,20}|{3,6}|{4,20}|", ID, Name, PortionCount, Unit, PortionSize);
        }
    }
}
