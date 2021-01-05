using System;
using System.Collections.Generic;
using VismaTask.Models;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("VismaTaskTests")]
namespace VismaTask.Controllers
{
    class RestaurantStockController
    {
        public List<RestaurantStock> StockList { get; private set; }

        public RestaurantStockController()
        {
            StockList = new List<RestaurantStock>();
        }

        public RestaurantStock CreateStock(string name, int portionCount, string unit, double portionSize)
        {
            int id;
            //to somewhat imitate the behaviour of a database and avoid ID clashing, they are automatically incremented upon creation
            //when called within the controller context
            if (StockList.Count == 0)
            {
                id = 1;
            }
            else
            {
                id = StockList.Last().ID + 1;
            }

            if(name == null || name == "" || name.StartsWith(" "))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if(unit == null || unit == "" || unit.StartsWith(" "))
            {
                throw new ArgumentNullException(nameof(unit));
            }

            RestaurantStock stock = new RestaurantStock(id, name, portionCount, unit, portionSize);
            StockList.Add(stock);

            return stock;
        }

        public RestaurantStock UpdateStock(int id, RestaurantStock updatedStockItem)
        {
            if (updatedStockItem == null)
            {
                throw new ArgumentNullException(nameof(updatedStockItem));
            }

            var stockToUpdate = StockList.FirstOrDefault(s => s.ID == id);
            if(stockToUpdate == null)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            var index = StockList.IndexOf(stockToUpdate);

            //creating a deep copy as to not accidentally lose the reference to the newly updated entry
            stockToUpdate = new RestaurantStock(id, updatedStockItem.Name, updatedStockItem.PortionCount, updatedStockItem.Unit, updatedStockItem.PortionSize);
            StockList[index] = stockToUpdate;

            return stockToUpdate;
        }

        public bool CanDeduceFromStock(List<RestaurantStock> itemsToDeduce)
        {
            if(itemsToDeduce == null)
            {
                throw new ArgumentNullException(nameof(itemsToDeduce));
            }
            foreach (var item in itemsToDeduce)
            {
                var stock = StockList.FirstOrDefault(s => s.ID == item.ID);
                if (stock == null)
                {
                    throw new ArgumentNullException(nameof(stock));
                }

                if (stock.PortionCount < 1)
                {
                    return false;
                }
            }
            return true;
        }

        public void DeduceFromStock(List<RestaurantStock> itemsToDeduce)
        {
            if(itemsToDeduce == null)
            {
                throw new ArgumentNullException(nameof(itemsToDeduce));
            }
            foreach (var item in itemsToDeduce)
            {
                var stock = StockList.FirstOrDefault(s => s.ID == item.ID);
                if (stock == null)
                {
                    throw new ArgumentNullException(nameof(stock));
                }

                stock.PortionCount--;
            }
        }

        public RestaurantStock DeleteStock(int id)
        {
            var stockToDelete = StockList.FirstOrDefault(s => s.ID == id);
            if(stockToDelete == null)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            StockList.Remove(stockToDelete);
            return stockToDelete;
        }
    }
}
