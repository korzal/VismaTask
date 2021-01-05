using System;
using System.Collections.Generic;

namespace VismaTask.Models
{
    class CustomerOrder
    {
        public int ID { get; set; }
        public DateTime TimeStamp { get; private set; }

        public List<RestaurantMenu> MenuItems { get; private set; }

        public CustomerOrder(int ID, DateTime TimeStamp, List<RestaurantMenu> MenuItems)
        {
            this.ID = ID;
            this.TimeStamp = TimeStamp;
            this.MenuItems = new List<RestaurantMenu>();
            this.MenuItems.AddRange(MenuItems);
        }

        public void SetTimeStamp(DateTime timeStamp)
        {
            if(timeStamp > DateTime.Now)
            {
                throw new ArgumentException("Order cannot be placed later the current time.");
            }
            TimeStamp = timeStamp;
        }

        public void SetMenuItems(List<RestaurantMenu> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            MenuItems.Clear();
            MenuItems.AddRange(items);
        }

        public override string ToString()
        {
            string menu = "";
            foreach (var item in MenuItems)
            {
                menu += item.ID + " ";
            }
            return string.Format("|{0,5}|{1,30}|{2,20}|", ID, TimeStamp, menu);
        }
    }
}
