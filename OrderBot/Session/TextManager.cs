using System;
using PizzaBot.Orders;

namespace PizzaBot.Sessions
{
  internal class TextManager
  {
    internal static string Greet()
    {
      return "Hello! Welcome to Jim's Pizzaria! I can take your order now, or if you need assistance just type \"help\"";
    }

    internal static string FormatPizza(Order order)
    {
      string ret = "";
      foreach(Pizza p in order.Pizzas)
      {
        ret += $"\n\t 1 {p.Size} Pizza with:";
        foreach(Topping t in p.FullToppings)
        {
          ret += $"\n\t\t {t.ToString()},";
        }
        if(p.HalfToppings.Count != 0)
        {
          foreach(Topping t in p.HalfToppings)
          {
            ret += $"\n\t\t {t.ToString()} on half,";
          } 
        }
        ret = ret.Remove(ret.Length - 1);
      }
      return ret;
    }
  }
}