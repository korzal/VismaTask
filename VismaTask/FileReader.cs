using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using VismaTask.Models;
using VismaTask.Controllers;

namespace VismaTask
{
    class FileReader
    {

        public void RestaurantStockReader(string directory, RestaurantStockController stock)
        {
            using (var reader = new StreamReader(directory))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.AutoMap<RestaurantStock>();

                int index = 0;
                while (csv.Read())
                {
                    if (index == 0)
                    {
                        index++;
                        continue;
                    }
                    var id = Convert.ToInt32(csv.GetField(0));
                    var name = csv.GetField(1);
                    var portionCount = Convert.ToInt32(csv.GetField(2));
                    var unit = csv.GetField(3);
                    var portionSize = Convert.ToDouble(csv.GetField(4));

                    stock.CreateStock(name, portionCount, unit, portionSize);
                    index++;
                }
            }
        }

        public void RestaurantMenuReader(string directory, RestaurantMenuController menu, List<RestaurantStock> stockList)
        {
            using (var reader = new StreamReader(directory))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.AutoMap<RestaurantMenu>();

                int index = 0;
                while (csv.Read())
                {
                    if(index == 0)
                    {
                        index++;
                        continue;
                    }
                    var ID = csv.GetField(0);
                    var name = csv.GetField(1);
                    var products = csv.GetField(2);
                    

                    var prodArray = products.Split(" ");

                    List<RestaurantStock> productList = new List<RestaurantStock>();
                    foreach(var item in prodArray)
                    {
                        var stock = stockList.FirstOrDefault(s => s.ID == Convert.ToInt32(item));
                        if(stock == null)
                        {
                            throw new IOException("Ingredient from within menu item does not exist");
                        }
                        productList.Add(stock);
                    }

                    RestaurantMenu menuItem = new RestaurantMenu(Convert.ToInt32(ID), name.ToString(), productList);
                    menu.MenuList.Add(menuItem);
                    index++;
                }
                
            }
        }

        public void CustomerOrderReader(string directory, CustomerOrdersController order, List<RestaurantMenu> menuList)
        {
            using (var reader = new StreamReader(directory))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.AutoMap<CustomerOrder>();

                int index = 0;
                while (csv.Read())
                {
                    if (index == 0)
                    {
                        index++;
                        continue;
                    }
                    var ID = csv.GetField(0);
                    var timeStamp = csv.GetField(1);
                    var menuItems = csv.GetField(2);


                    var menuArray = menuItems.Split(" ");

                    List<RestaurantMenu> itemList = new List<RestaurantMenu>();
                    foreach (var item in menuArray)
                    {
                        var menu = menuList.FirstOrDefault(s => s.ID == Convert.ToInt32(item));
                        if (menu == null)
                        {
                            throw new IOException("Ingredient from within menu item does not exist");
                        }
                        itemList.Add(menu);
                    }

                    order.CreateOrder(itemList);
                    index++;
                }
            }
        }
    }
}
