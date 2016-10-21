using JPMorgan.Assignment.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPMorgan.Assignment
{
  public   class MainForm
    {
      private  List<Stock> testStocks = new List<Stock>();
      public MainForm()
      {
          System.Timers.Timer timer = new System.Timers.Timer();
          timer.Interval = 30000;
          timer.Elapsed += ShowVolumeWeightedStockPrice;
          timer.Start();
          SetUpTestData();
          DisplayStocksData();
          ShowOptions();
      }
      private void ShowVolumeWeightedStockPrice(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("Would you like to see Volume weighted stock price for each stock? Y/N \n");
            var strChoice = Console.ReadLine();
          if(strChoice=="Y")
          {
              Console.WriteLine("\nStock symbol\tType\tLast Dividend\tFixed Dividend\tPar Value\tVolume Weighted Stock Price\n");
              foreach (Stock stock in testStocks)
              {
                  Console.WriteLine("\n");
                  Console.WriteLine("\t{0}\t\t{1}\t{2}\t{3}\t{4}\t{5}",
                      stock.StockSymbol, stock.Type.ToString(), stock.LastDividend, stock.FixedDividend, stock.ParValue, stock.calculateVolumeWeightedStockPrice());
              }
          }            
        }

        private  void SetUpTestData()
        {
            testStocks.Add(new Stock() { StockSymbol = "TEA", Type = StockType.COMMON, LastDividend = 0, FixedDividend = 0, ParValue = 100 });
            testStocks.Add(new Stock() { StockSymbol = "POP", Type = StockType.COMMON, LastDividend = 8, FixedDividend = 0, ParValue = 100 });
            testStocks.Add(new Stock() { StockSymbol = "ALE", Type = StockType.COMMON, LastDividend = 23, FixedDividend = 0, ParValue = 60 });
            testStocks.Add(new Stock() { StockSymbol = "GIN", Type = StockType.PREFERRED, LastDividend = 8, FixedDividend = 0.2, ParValue = 100 });
            testStocks.Add(new Stock() { StockSymbol = "JOE", Type = StockType.COMMON, LastDividend = 13, FixedDividend = 0, ParValue = 250 });
        }

        private  void DisplayStocksData()
        {
            Console.WriteLine("\tStock symbol\tType\tLast Dividend\tFixed Dividend\tPar Value");
            foreach (Stock stock in testStocks)
            {
                Console.WriteLine("\n");
                Console.WriteLine("\t{0}\t\t{1}\t{2}\t{3}\t{4}",
                    stock.StockSymbol, stock.Type.ToString(), stock.LastDividend, stock.FixedDividend, stock.ParValue);
            }
        }

        private  void BuyStocks()
        {
            Console.WriteLine("Enter the symbol of stock you like to buy\n");
            var strStockSymbol = Console.ReadLine();
            if (ValidateStockSymbol(strStockSymbol) == true)
            {
                var stock = testStocks.Where(x => x.StockSymbol == strStockSymbol).FirstOrDefault();
                Console.WriteLine("Enter the quantity of stock you like to buy\n");
                int quantity = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the price\n");
                double price = double.Parse(Console.ReadLine());
                stock.buy(quantity, price);
                Console.WriteLine("You bought the stock successfully\n");
            }
        }
        private  void SellStocks()
        {
            Console.WriteLine("Enter the symbol of stock you like to sell\n");
            var strStockSymbol = Console.ReadLine();
            if (ValidateStockSymbol(strStockSymbol) == true)
            {
                var stock = testStocks.Where(x => x.StockSymbol == strStockSymbol).FirstOrDefault();
                Console.WriteLine("Enter the quantity of stock you like to sell\n");
                int quantity = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the price\n");
                double price = double.Parse(Console.ReadLine());
                stock.sell(quantity, price);
                Console.WriteLine("You sold the stock successfully\n");
            }
        }

        private  void ShowOptions()
        {
            Console.WriteLine("Options");
            Console.WriteLine("Press 1 to Buy");
            Console.WriteLine("Press 2 to Sell");
            Console.WriteLine("Press 3 to calculate the dividend yield");
            Console.WriteLine("Press 4 to calculate the P/E Ratio");
            Console.WriteLine("Press 5 to Show Global Beverage Corporation Exchange Index");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    BuyStocks();
                    ShowOptions();
                    break;
                case "2":
                    SellStocks();
                    ShowOptions();
                    break;
                case "3":
                    CalculateDividendForPrice();
                    ShowOptions();
                    break;
                case "4":
                    CalculatePERatio();
                    ShowOptions();
                    break;
                case "5":
                    Console.WriteLine("Index:{0}", allShareIndex());
                    ShowOptions();
                    break;
                default:
                    SetUpTestData();
                    ShowOptions();
                    break;
            }
        }

        private  void CalculatePERatio()
        {
            Console.WriteLine("Enter the symbol of stock you like to calculate\n");
            var strStockSymbol = Console.ReadLine();
            if (ValidateStockSymbol(strStockSymbol) == true)
            {
                var stock = testStocks.Where(x => x.StockSymbol == strStockSymbol).FirstOrDefault();
                Console.WriteLine("Enter the price to calculate dividend\n");
                double price = double.Parse(Console.ReadLine());
                Console.WriteLine("Dividend{0}\n", stock.PERatio(price));
            }
        }

        private  void CalculateDividendForPrice()
        {
            Console.WriteLine("Enter the symbol of stock you like to calculate\n");
            var strStockSymbol = Console.ReadLine();
            if (ValidateStockSymbol(strStockSymbol) == true)
            {
                var stock = testStocks.Where(x => x.StockSymbol == strStockSymbol).FirstOrDefault();
                Console.WriteLine("Enter the price to calculate dividend\n");
                double price = double.Parse(Console.ReadLine());
                Console.WriteLine("Dividend{0}\n", stock.Dividend(price));
            }
        }


        private  bool ValidateStockSymbol(string stockSymbol)
        {
            bool isvalid = true;
            if (stockSymbol.Length != 3)
            {
                isvalid = false;
                Console.WriteLine("Enter correct number of alphabets for stock symbol");
            }
            var stock = testStocks.Where(x => x.StockSymbol == stockSymbol).FirstOrDefault();
            if (stock == null)
            {
                isvalid = false;
                Console.WriteLine("Wrong stock symbol/Not found in our list of stocks");
            }
            return isvalid;
        }

        public  Double allShareIndex()
        {
            Double allShareIndex = 0.0;
            foreach (Stock stock in testStocks)
            {
                allShareIndex += stock.getPrice();
            }
            return Math.Pow(allShareIndex, 1.0 / testStocks.Count());
        }
    }
}
