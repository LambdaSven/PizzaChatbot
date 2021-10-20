using System;
using Xunit;
using OrderBot;
using PizzaBot.Interpretation;
using System.Collections.Generic;
using System.Linq;
using PizzaBot.Orders;

namespace OrderBot.tests
{
    [Collection("Tests of the Parsing System")]
    public class ParserTests
    {
      [Fact(DisplayName = "Simple pizza order")]
      public void SimpleTest()
      {
        Order test = new Order("");

        Parser.Parse(test, Lexer.scan("1 large hawaiian pizza"));
        List<Pizza> intent = new List<Pizza>()
        {
          new Pizza(new List<Topping>(){Topping.CHEESE,
                                        Topping.PINEAPPLE,
                                        Topping.HAM,
                                        Topping.BACON}, Base.TOMATO, Size.LARGE)
        };
        Assert.True
        (
          test.Customer == "" &&
          test.Pizzas.SequenceEqual(intent)
        );
      }
      [Fact(DisplayName = "Pizza order with omission")]
      public void OmissionTest()
      {
        Order test = new Order("");

        Parser.Parse(test, Lexer.scan("1 small hawaiian pizza without ham"));
        List<Pizza> intent = new List<Pizza>()
        {
          new Pizza(new List<Topping>(){Topping.CHEESE,
                                        Topping.PINEAPPLE,
                                        Topping.BACON}, Base.TOMATO, Size.SMALL)
        };
        Assert.True
        (
          test.Customer == "" &&
          test.Pizzas.SequenceEqual(intent)
        );
      }
      [Fact(DisplayName = "Pizza order with addition")]
      public void AdditionTest()
      {
        Order test = new Order("");

        Parser.Parse(test, Lexer.scan("1 hawaiian pizza with mushrooms"));
        List<Pizza> intent = new List<Pizza>()
        {
          new Pizza(new List<Topping>(){Topping.CHEESE,
                                        Topping.PINEAPPLE,
                                        Topping.HAM,
                                        Topping.BACON,
                                        Topping.MUSHROOMS}, Base.TOMATO, Size.MEDIUM)
        };
        Assert.True
        (
          test.Customer == "" &&
          test.Pizzas.SequenceEqual(intent)
        );
      }
      [Fact(DisplayName = "Two Pizzas")]
      public void TwoPizzas()
      {
        Order test = new Order("");

        Parser.Parse(test, Lexer.scan("1 large hawaiian and 1 small pepperoni"));
        List<Pizza> intent = new List<Pizza>()
        {
          new Pizza(new List<Topping>(){Topping.CHEESE,
                                        Topping.PINEAPPLE,
                                        Topping.HAM,
                                        Topping.BACON}, Base.TOMATO, Size.LARGE),
          new Pizza(new List<Topping>(){Topping.CHEESE,
                                        Topping.PEPPERONI}, Base.TOMATO, Size.SMALL)
        };
        Assert.True
        (
          test.Customer == "" &&
          test.Pizzas.SequenceEqual(intent)
        );
      }

      [Fact(DisplayName = "Two of the Same Pizza")]
      public void DupePizza()
      {
        Order test = new Order("");

        Parser.Parse(test, Lexer.scan("2 small pepperoni pizzas"));
        List<Pizza> intent = new List<Pizza>()
        {
          new Pizza(new List<Topping>(){Topping.CHEESE,
                                        Topping.PEPPERONI}, Base.TOMATO, Size.SMALL),
          new Pizza(new List<Topping>(){Topping.CHEESE,
                                        Topping.PEPPERONI}, Base.TOMATO, Size.SMALL)
        };
        Assert.True
        (
          test.Customer == "" &&
          test.Pizzas.SequenceEqual(intent)
        );
      }
      [Fact(DisplayName = "Custom Pizza")]
      public void CustomPizza()
      {
        Order test = new Order("");

        Parser.Parse(test, Lexer.scan("1 ham and cheddar pizza and 1 with provolone and mushrooms"));
        List<Pizza> intent = new List<Pizza>()
        {
          new Pizza(new List<Topping>(){Topping.CHEESE,
                                        Topping.HAM,
                                        Topping.CHEDDAR}, Base.TOMATO, Size.MEDIUM),
          new Pizza(new List<Topping>(){Topping.CHEESE,
                                        Topping.PROVOLONE,
                                        Topping.MUSHROOMS}, Base.TOMATO, Size.MEDIUM)
        };
        Assert.True
        (
          test.Customer == "" &&
          test.Pizzas.SequenceEqual(intent)
        );
      }
    }
}