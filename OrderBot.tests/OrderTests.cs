using System;
using Xunit;
using OrderBot;
using PizzaBot.Orders;
using System.Collections.Generic;
using System.Linq;

namespace OrderBot.tests
{
    [Collection("Tests of the Order Class")]
    public class ToppingTests
    {
      [Fact(DisplayName = "Check that a Single Pizza Order is priced correctly")]
      public void SinglePizzaPriceTest()
      {
        Order o = new Order("tester");
        o.AddPizza(new Pizza(
          new List<Topping>() {Topping.CHEESE, Topping.PEPPERONI}
        ));
        Assert.Equal(o.CalculatePrice(), 11.90m);
      }

      [Fact(DisplayName = "Test that Adding a pizza works successfully")]
      public void AddPizzaTest(){
        Order o = new Order("tester");
        Pizza p = new Pizza(
          new List<Topping>() {Topping.CHEESE, Topping.PEPPERONI}
        );
        o.AddPizza(p);
        Assert.Contains(p, o.Pizzas);
      }
    }
}