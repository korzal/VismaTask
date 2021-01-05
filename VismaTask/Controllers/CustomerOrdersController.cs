using System;
using System.Collections.Generic;
using System.Linq;
using VismaTask.Models;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("VismaTaskTests")]
namespace VismaTask.Controllers
{
    class CustomerOrdersController
    {
        public List<CustomerOrder> OrderList { get; private set; }
        public RestaurantMenuController Menu { get; private set; }

        public CustomerOrdersController(RestaurantMenuController menu)
        {
            if(menu == null)
            {
                throw new ArgumentNullException(nameof(menu));
            }
            OrderList = new List<CustomerOrder>();
            Menu = menu;
        }

        public bool CreateOrder(List<RestaurantMenu> menuList)
        {
            if(menuList == null)
            {
                throw new ArgumentNullException(nameof(menuList));
            }

            int id;
            //to somewhat imitate the behaviour of a database and avoid ID clashing, IDs are automatically incremented upon creation
            //when called within the controller context
            if (OrderList.Count == 0)
            {
                id = 1;
            }
            else
            {
                id = OrderList.Last().ID + 1;
            }

            List<RestaurantStock> stockToDeduce = new List<RestaurantStock>();
            foreach(var item in menuList)
            {
                var menuItem = Menu.MenuList.FirstOrDefault(m => m.ID == item.ID);
                if(menuItem == null)
                {
                    throw new ArgumentException(string.Format("Menu item {0} does not exist.", item.Name), nameof(menuList));
                }

                stockToDeduce.AddRange(item.Products);
                
            }
            //before actually deducing ingredients from the stock, a check is being run to confirm whether
            //enough ingredients are in stock to create an order
            bool canCreate = Menu.Stock.CanDeduceFromStock(stockToDeduce);
            if (canCreate)
            {
                //if enough ingredients are in stock, they are deduced and a new order is created
                Menu.Stock.DeduceFromStock(stockToDeduce);
                CustomerOrder order = new CustomerOrder(id, DateTime.Now, menuList);
                OrderList.Add(order);
                return canCreate;
            }

            return canCreate;
            
        }


    }
}
