using System;
using Xunit;
using OrderBot;
using PizzaBot.Orders;
using System.Collections.Generic;
using System.Linq;

namespace OrderBot.tests
{
    [Collection("Tests of the Order Class")]
    public class OrderTests
    {
      [Fact(DisplayName = "Check that a Single medium Pizza Order is priced correctly")]
      public void SinglePizzaPriceTest()
      {
        Order o = new Order("tester");
        o.AddPizza(new Pizza(
          new List<Topping>() {Topping.CHEESE, Topping.PEPPERONI}
        ));
        o.Pizzas[0].AddHalfTopping(Topping.PEPPERONI);
        Assert.Equal(o.CalculatePrice(), 11.90m);
      }

      [Fact(DisplayName = "Check that a Single small Pizza Order is priced correctly")]
      public void SingleSmallPizzaPriceTest()
      {
        Order o = new Order("tester");
        o.AddPizza(new Pizza(
          new List<Topping>() {Topping.CHEESE, Topping.PEPPERONI}, Base.TOMATO, Size.SMALL
        ));
        Assert.Equal(o.CalculatePrice(), 10.71m);
      }

    [Fact(DisplayName = "Check that a Single Large Pizza Order is priced correctly")]
      public void SingleLargePizzaPriceTest()
      {
        Order o = new Order("tester");
        o.AddPizza(new Pizza(
          new List<Topping>() {Topping.CHEESE, Topping.PEPPERONI}, Base.TOMATO, Size.LARGE
        ));
        Assert.Equal(o.CalculatePrice(), 13.09m);
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

      [Fact(DisplayName = "Construct Order with Input String Provided")]
      public void AltCtor()
      {
        Order o = new Order("tester", "1 large pizza");
        Assert.True(o.OrderString == "1 large pizza");
      }
    }
}