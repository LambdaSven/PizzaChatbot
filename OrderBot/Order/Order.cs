using System.Collections.Generic;
using System.Linq;

namespace PizzaBot.Orders 
{
  public class Order
  {
    internal List<Pizza> Pizzas;
    internal string Customer;

    public Order(string s)
    {
     Pizzas = new List<Pizza>();
     Customer = s;
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