using System;
using System.Collections.Generic;
using VismaTask.Controllers;
using VismaTask.Models;
using System.Linq;

namespace VismaTask
{
    class ConsoleUI
    {
        public void ConsoleInputs(RestaurantStockController stock, RestaurantMenuController menu, CustomerOrdersController order)
        {
            string line;
            while (true)
            {
                Console.Write("         To see the list of items in stock type in 'stocklist'\n");
                Console.Write("     To see the list of menu items type in 'menulist'\n");
                Console.Write("To see the list of orders type in 'orderlist'\n");

                line = Console.ReadLine();
                switch (line)
                {
                    case "stocklist":
                        PrintStock(stock);
                        break;
                    case "menulist":
                        PrintMenu(menu, stock);
                        break;
                    case "orderlist":
                        PrintOrders(order, menu);
                        break;
                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
                Console.WriteLine("Press enter to continue.");
                Console.ReadKey();


                Console.Clear();
            }

        }

        public void PrintStock(RestaurantStockController stock)
        {
            string dash = new string('-', 77);
            Console.WriteLine(dash);
            Console.WriteLine(string.Format("|{0,5}|{1,20}|{2,20}|{3,6}|{4,20}|", "ID", "Name", "PortionCount", "Unit", "PortionSize"));

            Console.WriteLine(dash);
            foreach (var item in stock.StockList)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine(dash);
            PrintStockOptions(stock);
        }

        public void PrintStockOptions(RestaurantStockController stock)
        {
            Console.WriteLine("To create a new stock item type in 'stockcreate'");
            Console.WriteLine("To update an existing stock item type in 'stockupdate'");
            Console.WriteLine("To delete an existing stock item type in 'stockdelete'");
            var command = Console.ReadLine();

            

            if (command == "stockdelete")
            {
                Console.WriteLine("Type in the following parameters (seperated by one whitespace character each):");
                Console.WriteLine("ID(int, of an existing entry)");
            }
            else if(command == "stockupdate" || command == "stockcreate")
            {
                //ID is only used for identifying existing entries and will not be used in the case of creating a new stock entry
                Console.WriteLine("Type in the following parameters (seperated by one whitespace character each):");
                Console.WriteLine("ID(int), Name(string), PortionCount(int), Unit(string), PortionSize(double)");
                Console.WriteLine("If you're updating an existing entry these parameters will replace it.");
            }
            else
            {
                Console.WriteLine("Invalid command.");
                return;
            }

            
            var line = Console.ReadLine();
            var parts = line.Split(' ');
            string id, name, portionCount, unit, portionSize = "";
            int idConverted, portionCountConverted = -1;
            double portionSizeConverted = -1;
            RestaurantStock outputStock;

            if (parts.Length == 1)
            {
                id = parts[0];

                if (!Int32.TryParse(id, out idConverted))
                {
                    Console.WriteLine("The ID parameter value is not a parsable integer.");
                    return;
                }
                if(command == "stockdelete")
                {
                    outputStock = stock.DeleteStock(idConverted);
                }
                else
                {
                    Console.WriteLine("Invalid command.");
                    return;
                }
                
            }
            else if (parts.Length == 5)
            {
                id = parts[0];
                name = parts[1];
                portionCount = parts[2];
                unit = parts[3];
                portionSize = parts[4];

                if(!Int32.TryParse(id, out idConverted))
                {
                    Console.WriteLine("The ID parameter value is not a parsable integer.");
                    return;
                }
                if (!Int32.TryParse(portionCount, out portionCountConverted))
                {
                    Console.WriteLine("The PortionCount parameter value is not a parsable integer.");
                    return;
                }
                if (!Double.TryParse(portionSize, out portionSizeConverted))
                {
                    Console.WriteLine("The PortionSize parameter value is not a parsable double.");
                    return;
                }

                switch (command)
                {
                    case "stockcreate":
                        outputStock = stock.CreateStock(name, portionCountConverted, unit, portionSizeConverted);
                        break;

                    case "stockupdate":
                        RestaurantStock updatedStock = new RestaurantStock(idConverted, name, portionCountConverted, unit, portionSizeConverted);
                        outputStock = stock.UpdateStock(idConverted, updatedStock);
                        break;

                    default:
                        Console.WriteLine("Command does not exist.");
                        return;
                }
            }
            else
            {
                Console.WriteLine("Invalid number of parameters.");
                return;
            }

            string dash = new string('-', 77);
            Console.WriteLine(dash);
            Console.WriteLine(string.Format("|{0,5}|{1,20}|{2,20}|{3,6}|{4,20}|", "ID", "Name", "PortionCount", "Unit", "PortionSize"));

            Console.WriteLine(dash);
            Console.WriteLine(outputStock.ToString());
            Console.WriteLine(dash);

        }

        public void PrintMenu(RestaurantMenuController menu, RestaurantStockController stock)
        {
            string dash = new string('-', 59);
            Console.WriteLine(dash);
            Console.WriteLine(string.Format("|{0,5}|{1,30}|{2,20}|", "ID", "Name", "ProductIDs"));

            Console.WriteLine(dash);
            foreach (var item in menu.MenuList)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine(dash);
            PrintMenuOptions(menu, stock);
        }

        public void PrintMenuOptions(RestaurantMenuController menu, RestaurantStockController stock)
        {
            Console.WriteLine("To create a new menu item type in 'menucreate'");
            Console.WriteLine("To update an existing menu item type in 'menuupdate'");
            Console.WriteLine("To delete an existing menu item type in 'menudelete'");
            var command = Console.ReadLine();

            if (command == "menudelete")
            {
                Console.WriteLine("Type in the following parameters (seperated by one whitespace character each):");
                Console.WriteLine("ID(int, of an existing entry)");
            }
            else if(command == "menuupdate" || command =="menucreate")
            {
                //ID is only used for identifying existing entries and will not be used in the case of creating a new menu entry
                Console.WriteLine("Type in the following parameters (seperated by one whitespace character each):");
                Console.WriteLine("ID(int), Name(string), Products(int array of IDs)");
                Console.WriteLine("If you're updating an existing entry these parameters will replace it.");
            }
            else
            {
                Console.WriteLine("Invalid command.");
                return;
            }

            var line = Console.ReadLine();
            var parts = line.Split(' ');
            string id, name = "";
            int idConverted, productConverted = -1;
            List<string> products = new List<string>();
            RestaurantMenu outputMenu;

            if (parts.Length == 1)
            {
                id = parts[0];

                if (!Int32.TryParse(id, out idConverted))
                {
                    Console.WriteLine("The ID parameter value is not a parsable integer.");
                    return;
                }
                if (command == "menudelete")
                {
                    outputMenu = menu.DeleteMenuItem(idConverted);
                }
                else
                {
                    Console.WriteLine("Invalid command.");
                    return;
                }

            }
            else if (parts.Length >= 3)
            {
                id = parts[0];
                name = parts[1];
                for (int i = 2; i < parts.Length; i++)
                {
                    products.Add(parts[i]);
                }

                if (!Int32.TryParse(id, out idConverted))
                {
                    Console.WriteLine("The ID parameter value is not a parsable integer.");
                    return;
                }

                List<RestaurantStock> ingredients = new List<RestaurantStock>();
                foreach(var item in products)
                {
                    if (!Int32.TryParse(item, out productConverted))
                    {
                        Console.WriteLine("One of the products are not a parsable integer.");
                        return;
                    }
                    RestaurantStock newIngredient = stock.StockList.FirstOrDefault(s => s.ID == productConverted);
                    if(newIngredient == null)
                    {
                        Console.WriteLine("One of the specified product IDs do not exist.");
                        return;
                    }
                    ingredients.Add(newIngredient);
                }
                
                switch (command)
                {
                    case "menucreate":
                        outputMenu = menu.CreateMenuItem(name, ingredients);
                        break;

                    case "menuupdate":
                        RestaurantMenu updatedStock = new RestaurantMenu(idConverted, name, ingredients);
                        outputMenu = menu.UpdateMenuItem(idConverted, updatedStock);
                        break;

                    default:
                        Console.WriteLine("Command does not exist.");
                        return;
                }
            }
            else
            {
                Console.WriteLine("Invalid number of parameters.");
                return;
            }

            string dash = new string('-', 59);
            Console.WriteLine(dash);
            Console.WriteLine(string.Format("|{0,5}|{1,30}|{2,20}|", "ID", "Name", "ProductIDs"));

            Console.WriteLine(dash);
            Console.WriteLine(outputMenu.ToString());
            Console.WriteLine(dash);

        }

        public void PrintOrders(CustomerOrdersController order, RestaurantMenuController menu)
        {
            string dash = new string('-', 59);
            Console.WriteLine(dash);
            Console.WriteLine(string.Format("|{0,5}|{1,30}|{2,20}|", "ID", "TimeStamp", "MenuItemIDs"));

            Console.WriteLine(dash);
            foreach (var item in order.OrderList)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine(dash);
            PrintOrderOptions(order, menu);
        }

        public void PrintOrderOptions(CustomerOrdersController order, RestaurantMenuController menu)
        {
            Console.WriteLine("To create a new order type in 'ordercreate'");
            var command = Console.ReadLine();

            if(command != "ordercreate")
            {
                Console.WriteLine("Invalid command.");
                return;
            }

            Console.WriteLine("Menu Items(int array of IDs)");

            var line = Console.ReadLine();
            var parts = line.Split(' ');
            int menuItemConverted = -1;
            List<string> rawInput = new List<string>();

            if (parts.Length >= 1)
            {
                for (int i = 0; i < parts.Length; i++)
                {
                    rawInput.Add(parts[i]);
                }

                List<RestaurantMenu> orderMenu = new List<RestaurantMenu>();
                foreach (var item in rawInput)
                {
                    if (!Int32.TryParse(item, out menuItemConverted))
                    {
                        Console.WriteLine("One of the menu items are not a parsable integer.");
                        return;
                    }
                    RestaurantMenu newMenuItem =  menu.MenuList.FirstOrDefault(s => s.ID == menuItemConverted);
                    if (newMenuItem == null)
                    {
                        Console.WriteLine("One of the specified product IDs do not exist.");
                        return;
                    }
                    orderMenu.Add(newMenuItem);
                }

                bool canCreate = order.CreateOrder(orderMenu);
                if (!canCreate)
                {
                    Console.WriteLine("Not enough ingredients in stock to place order.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Invalid number of parameters.");
                return;
            }

            string dash = new string('-', 59);
            Console.WriteLine(dash);
            Console.WriteLine(string.Format("|{0,5}|{1,30}|{2,20}|", "ID", "Name", "MenuItemIDs"));

            Console.WriteLine(dash);
            Console.WriteLine(order.OrderList.Last().ToString());
            Console.WriteLine(dash);
        }
    }
}
