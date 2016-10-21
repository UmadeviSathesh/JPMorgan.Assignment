using JPMorgan.Assignment.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPMorgan.Assignment
{
    public class Stock
    {
     public String StockSymbol { get; set; }
	 public StockType Type { get; set; }
     public Double LastDividend { get; set; }
    public Double FixedDividend { get; set; }
    public Double ParValue { get; set; }
    public List<Trade> Trades { get; set; }

        public Stock()
    {
        Trades = new List<Trade>();
    }
    
        /// <summary>
        /// Calculate Dividend
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
	public Double Dividend(Double price)
    {
		switch(Type) {
			case StockType.COMMON:
				return LastDividend/price;
			case StockType.PREFERRED:
				return FixedDividend*(ParValue/price);
			default:
				return 0.0;
        }
	}
	
        
/// <summary>
/// Calculate PERatio
/// </summary>
/// <param name="price"></param>
    /// <returns>PERatio</returns>
	public Double PERatio(Double price) {
		return price/LastDividend;
	}

/// <summary>
/// Buy Stock with a specific quantity and price
/// </summary>
/// <param name="quantity"></param>
/// <param name="price"></param>
	public void buy(int quantity, Double price) {
        Trades.Add(new Trade() { Price = price, Quantity = quantity, TradeTime = DateTime.Now, Type = TradeType.BUY });
	}

/// <summary>
/// Sell stock 
/// </summary>
/// <param name="quantity"></param>
/// <param name="price"></param>
	public void sell(int quantity, Double price) {
        Trades.Add(new Trade() { Price = price, Quantity = quantity, TradeTime = DateTime.Now, Type = TradeType.SELL});	
	}
	
/// <summary>
/// Last known trade price
/// </summary>
/// <returns></returns>
	public Double getPrice() {
		if (Trades.Count() > 0) {
			// Uses the last trade price as price
            return Trades.LastOrDefault().Price;
		} else {
			// No trades = price 0? :)
			return 0.0;
		}
	}
	
/// <summary>
    /// Calculate the Volume Weighted Stock Price
/// </summary>
/// <returns></returns>
	public Double calculateVolumeWeightedStockPrice() {
		// Date 15 minutes ago
		DateTime startTime = DateTime.Now.AddMinutes(-15);
		// Get trades for the last 15 minutes
        List<Trade> trades = Trades.Where(x => x.TradeTime > startTime).ToList();
		// Calculate
		Double volumeWeigthedStockPrice = 0.0;
		int totalQuantity = 0;
        foreach (Trade t in trades)
        {
            totalQuantity += t.Quantity;
            volumeWeigthedStockPrice += t.Price * t.Quantity;
        }
		return volumeWeigthedStockPrice/totalQuantity;
	}
    }
}
