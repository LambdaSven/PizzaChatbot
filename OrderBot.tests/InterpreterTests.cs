using System;
using Xunit;
using OrderBot;
using PizzaBot.Orders;
using System.Collections.Generic;
using System.Linq;
using PizzaBot.Interpretation;

namespace OrderBot.tests
{
    [Collection("Tests of the Interpreter Class")]
    public class InterpreterTests
    {
      [Fact(DisplayName = "Interpreter Test")]
      public void InterpreterTest()
      {
        Order o = Interpreter.Interpret("1 pepperoni pizza", "tester");
        Order test = new Order("tester");
        test.AddPizza(new Pizza(new List<Topping> {Topping.CHEESE, Topping.PEPPERONI}));
        Assert.True(o.Pizzas.SequenceEqual(test.Pizzas));
      }
    }
}