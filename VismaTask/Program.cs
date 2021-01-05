using VismaTask.Controllers;
using System.IO;

namespace VismaTask
{

    class Program
    {
        static void Main(string[] args)
        {

            RestaurantStockController stock = new RestaurantStockController();
            RestaurantMenuController menu = new RestaurantMenuController(stock);
            CustomerOrdersController order = new CustomerOrdersController(menu);

            FileReader reader = new FileReader();
            Directory.SetCurrentDirectory("../../../Data");
            reader.RestaurantStockReader("RestaurantStock.csv", stock);

            reader.RestaurantMenuReader("RestaurantMenu.csv", menu, stock.StockList);

            reader.CustomerOrderReader("CustomerOrder.csv", order, menu.MenuList);

            
            ConsoleUI ui = new ConsoleUI();
            ui.ConsoleInputs(stock, menu, order);

        }

        
    }
}
