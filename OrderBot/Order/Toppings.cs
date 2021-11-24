using System.Collections.Generic;

namespace PizzaBot.Orders
{
  internal enum Topping
  {
   PEPPERONI, SAUSAGE, BACON, CHICKEN, HAM, BEEF, STEAK, SALAMI, ONION, MUSHROOMS, 
   PEPPERS, PINEAPPLE, OLIVES, TOMATOES, SPINACH, JALAPENOS, PROVOLONE, CHEESE, CHEDDAR,
  }

  internal static class ToppingExtension
  {
    private static Dictionary<Topping, decimal> priceMap = new Dictionary<Topping, decimal>
    {
      {Topping.PEPPERONI, 1},
      {Topping.SAUSAGE, 1},
      {Topping.BACON, 1},
      {Topping.CHICKEN, 1},
      {Topping.HAM, 1},
      {Topping.BEEF, 1},
      {Topping.STEAK, 2},
      {Topping.SALAMI, 1},
      {Topping.ONION, 1},
      {Topping.MUSHROOMS, 1},
      {Topping.PEPPERS, 1},
      {Topping.PINEAPPLE, 1},
      {Topping.OLIVES, 1},
      {Topping.TOMATOES, 1},
      {Topping.SPINACH, 1},
      {Topping.JALAPENOS, 2},
      {Topping.PROVOLONE, 2},
      {Topping.CHEESE, 1},
      {Topping.CHEDDAR, 2},
    };
    internal static decimal Price(this Topping t)
    {
      return priceMap[t] * 1.95m;
    }
  }
}