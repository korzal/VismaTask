using System;
using System.Collections.Generic;
using VismaTask.Models;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("VismaTaskTests")]
namespace VismaTask.Controllers
{
    class RestaurantMenuController
    {
        public List<RestaurantMenu> MenuList { get; private set; }
        public RestaurantStockController Stock { get; private set; }

        public RestaurantMenuController(RestaurantStockController stock)
        {
            if(stock == null)
            {
                throw new ArgumentNullException(nameof(stock));
            }
            MenuList = new List<RestaurantMenu>();
            Stock = stock;
        }

        public RestaurantMenu CreateMenuItem(string name, List<RestaurantStock> ingredients)
        {
            int id;
            //to somewhat imitate the behaviour of a database and avoid ID clashing, they are automatically incremented upon creation
            //when called within the controller context
            if (MenuList.Count == 0)
            {
                id = 1;
            }
            else
            {
                id = MenuList.Last().ID + 1;
            }
            
            if(name == null || name == "" || name.StartsWith(" "))
            {
                throw new ArgumentNullException(nameof(name));
            }

            RestaurantMenu menu = new RestaurantMenu(id, name, ingredients);
            MenuList.Add(menu);
            return menu;
        }


        public RestaurantMenu UpdateMenuItem(int id, RestaurantMenu updatedMenuItem)
        {
            if(updatedMenuItem == null)
            {
                throw new ArgumentNullException(nameof(updatedMenuItem));
            }
            var menuItemToUpdate = MenuList.FirstOrDefault(m => m.ID == id);

            if (menuItemToUpdate == null)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            var index = MenuList.IndexOf(menuItemToUpdate);
            MenuList[index] = new RestaurantMenu(id, updatedMenuItem.Name, updatedMenuItem.Products);
            return updatedMenuItem;
        }

        public RestaurantMenu DeleteMenuItem(int id)
        {
            var menuItemToDelete = MenuList.FirstOrDefault(m => m.ID == id);
            if (menuItemToDelete == null)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            MenuList.Remove(menuItemToDelete);
            return menuItemToDelete;
        }
    }
}
