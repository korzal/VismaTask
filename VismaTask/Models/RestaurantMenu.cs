using System;
using System.Collections.Generic;
using System.Text;

namespace VismaTask.Models
{
    class RestaurantMenu
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<RestaurantStock> Products { get; private set; }

        public RestaurantMenu(int ID, string Name, List<RestaurantStock> Products)
        {
            if(Name == null || Name == ""|| Name.StartsWith(" "))
            {
                throw new ArgumentNullException(nameof(Name));
            }
            this.ID = ID;
            this.Name = Name;
            this.Products = new List<RestaurantStock>();
            this.Products.AddRange(Products);
        }

        public void SetProducts(List<RestaurantStock> products)
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));

            Products.Clear();
            Products.AddRange(products);
        }

        public override string ToString()
        {
            string prods = "";
            foreach (var item in Products)
            {
                prods += item.ID + " ";
            }
            return string.Format("|{0,5}|{1,30}|{2,20}|", ID, Name, prods);
        }
    }
}
