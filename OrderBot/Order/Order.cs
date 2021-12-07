using System.Collections.Generic;
using System.Linq;

namespace PizzaBot.Orders 
{
  public class Order
  {
    internal List<Pizza> Pizzas;
    internal string Customer;
    internal string OrderString
    {
        get; set;
    }
    
    public Order(string s)
    {
      Pizzas = new List<Pizza>();
      Customer = s;
    }
    public Order(string s, string raw)
    {
     Pizzas = new List<Pizza>();
     Customer = s;
     OrderString = raw;
    }

    internal void AddPizza(Pizza p)
    {
      Pizzas.Add(p);
    }
    internal decimal CalculatePrice()
    {
      return Pizzas.Select(e => e.CalculatePrice()).Sum();
    }
  }
}