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

        Parser.Parse(test, Lexer.scan("2 small deluxe pizzas"));
        List<Pizza> intent = new List<Pizza>()
        {
          new Pizza(new List<Topping>(){Topping.CHEESE,
                                        Topping.PEPPERONI,
                                        Topping.HAM,
                                        Topping.PEPPERS,
                                        Topping.MUSHROOMS}, Base.TOMATO, Size.SMALL),
          new Pizza(new List<Topping>(){Topping.CHEESE,
                                        Topping.PEPPERONI,
                                        Topping.HAM,
                                        Topping.PEPPERS,
                                        Topping.MUSHROOMS}, Base.TOMATO, Size.SMALL)
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

        Parser.Parse(test, Lexer.scan("1 sausage and cheddar pizza and 1 with provolone and pineapple"));
        List<Pizza> intent = new List<Pizza>()
        {
          new Pizza(new List<Topping>(){Topping.CHEESE,
                                        Topping.SAUSAGE,
                                        Topping.CHEDDAR}, Base.TOMATO, Size.MEDIUM),
          new Pizza(new List<Topping>(){Topping.CHEESE,
                                        Topping.PROVOLONE,
                                        Topping.PINEAPPLE}, Base.TOMATO, Size.MEDIUM)
        };
        Assert.True
        (
          test.Customer == "" &&
          test.Pizzas.SequenceEqual(intent)
        );
      }
      [Fact(DisplayName = "Pizza with Base")]
      public void PizzaBase()
      {
        Order test = new Order("");
        Parser.Parse(test, Lexer.scan("1 m alfredo pizza with chicken"));

        Assert.True(test.Pizzas[0].Base == Base.ALFREDO);
      }
      [Fact(DisplayName = "Half Pizza Parse")]
      public void HalfParse()
      {
        Order test = new Order("");
        Parser.Parse(test, Lexer.scan("1 large veggie pizza with bacon on half"));
        Assert.True(test.Pizzas[0].HalfToppings.Contains(Topping.BACON));
        Assert.True(test.Pizzas[0].FullToppings.Count == 7);
      }
      [Fact(DisplayName = "All Toppings Successfully Parse")]
      public void AllToppings()
      {
        Order test = new Order("");
        Parser.Parse(test, Lexer.scan("1 medium pizza with pepperoni sausage bacon chicken ham beef steak salami onion mushrooms peppers pineapple olives tomatoes spinach jalapenos provolone cheddar"));
        Assert.True(test.Pizzas[0].FullToppings.Count == 19);
      }
      [Fact(DisplayName = "All Bases Parse")]
      public void AllBases()
      {
        Order test = new Order("");
        Parser.Parse(test, Lexer.scan("1 medium tomato pizza"));
        Assert.True(test.Pizzas[0].Base == Base.TOMATO);

        test = new Order("");
        Parser.Parse(test, Lexer.scan("1 medium marinara pizza"));
        Assert.True(test.Pizzas[0].Base == Base.MARINARA);

        test = new Order("");
        Parser.Parse(test, Lexer.scan("1 medium white pizza"));
        Assert.True(test.Pizzas[0].Base == Base.WHITE);

        test = new Order("");
        Parser.Parse(test, Lexer.scan("1 medium Alfredo pizza"));
        Assert.True(test.Pizzas[0].Base == Base.ALFREDO);
      }

      [Fact(DisplayName = "All Premade Pizzas")]
      public void AllPremade()
      {
        Order test = new Order("");
        Parser.Parse(test, Lexer.scan("1 medium cheese pizza"));
        Assert.True(test.Pizzas[0].FullToppings.Contains(Topping.CHEESE) 
        && test.Pizzas[0].FullToppings.Count == 2);

        test = new Order("");
        Parser.Parse(test, Lexer.scan("1 medium pepperoni pizza"));
        Assert.True(test.Pizzas[0].FullToppings.Contains(Topping.CHEESE) 
        && test.Pizzas[0].FullToppings.Contains(Topping.PEPPERONI) 
        && test.Pizzas[0].FullToppings.Count == 2);

        test = new Order("");
        Parser.Parse(test, Lexer.scan("1 medium veggie pizza"));
        Assert.True(test.Pizzas[0].FullToppings.Contains(Topping.CHEESE) 
        && test.Pizzas[0].FullToppings.Contains(Topping.PEPPERS) 
        && test.Pizzas[0].FullToppings.Contains(Topping.ONION) 
        && test.Pizzas[0].FullToppings.Contains(Topping.SPINACH) 
        && test.Pizzas[0].FullToppings.Contains(Topping.MUSHROOMS) 
        && test.Pizzas[0].FullToppings.Contains(Topping.TOMATOES) 
        && test.Pizzas[0].FullToppings.Contains(Topping.OLIVES) 
        && test.Pizzas[0].FullToppings.Count == 7);
        
        test = new Order("");
        Parser.Parse(test, Lexer.scan("1 medium canadian pizza"));
        Assert.True(test.Pizzas[0].FullToppings.Contains(Topping.CHEESE) 
        && test.Pizzas[0].FullToppings.Contains(Topping.BACON) 
        && test.Pizzas[0].FullToppings.Contains(Topping.PEPPERONI) 
        && test.Pizzas[0].FullToppings.Contains(Topping.MUSHROOMS) 
        && test.Pizzas[0].FullToppings.Count == 4);

        test = new Order("");
        Parser.Parse(test, Lexer.scan("1 medium meat pizza"));
        Assert.True(test.Pizzas[0].FullToppings.Contains(Topping.CHEESE) 
        && test.Pizzas[0].FullToppings.Contains(Topping.PEPPERONI) 
        && test.Pizzas[0].FullToppings.Contains(Topping.BACON) 
        && test.Pizzas[0].FullToppings.Contains(Topping.SAUSAGE) 
        && test.Pizzas[0].FullToppings.Count == 4);
        
      }
    }
}