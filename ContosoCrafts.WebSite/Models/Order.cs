using System.Text.Json.Serialization;
using System.Text.Json;
using System;

namespace ContosoCrafts.WebSite.Models
{
	public class Order
	{
		public Order(MenuItem item, MenuItem[] sides, string size, string iceLevel, string sugarLevel)
		{
			TimeStamp = DateTime.Now.ToString();
			Item = item;
			Sides = sides;
			Size = size;
			IceLevel = iceLevel;
			SugarLevel = sugarLevel;
			OrderSubtotal = CalculateOrderSubtotal();
			OrderTotal = CalculateOrderTotal();
		}

		private const double SalesTax = 0.09;
		public string TimeStamp { get; set; }
		public MenuItem Item { get; set; }
		public string? Size { get; set; }
		public MenuItem?[] Sides { get; set; }
		public string? IceLevel { get; set; }
		public string? SugarLevel {  get; set; }
		public double OrderTotal { get; set; }
		public double OrderSubtotal { get; set; }

		public double CalculateOrderSubtotal()
		{
			
			double total = 0.0;
			if (Item.Price == null)
			{
				Item.Price = "$10.0";
			}
			if(Sides != null)
			{
				foreach (MenuItem item in Sides)
				{
					if(item.Price == null)
					{
						item.Price = "$3.00";
					}
					// remove dollar sign
					string price = item.Price.Substring(1);
					// convert to double
					double sidePrice = Convert.ToDouble(price);
					total += sidePrice;
				}
			}
			// remove dollar sign
			string? productPrice = Item.Price.Substring(1);
			return Math.Round(Convert.ToDouble(productPrice) + total, 2);
		}

		public double CalculateOrderTotal()
		{
			double salesTax = SalesTax * OrderTotal;
			return Math.Round(salesTax, 2);
		}

		public override string ToString()
		{
			return JsonSerializer.Serialize<Order>(this);
		}
	}
}
