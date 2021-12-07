using System;
using Xunit;
using OrderBot;
using PizzaBot.Interpretation;
using System.Collections.Generic;
using System.Linq;
using PizzaBot.Interface;

namespace OrderBot.tests
{
    [Collection("Tests of the Help Manager")]
    public class HelpManagerTests
    {
      [Fact(DisplayName = "Raw Help Request")]
      public void RawHelp()
      {
        string str = HelpManager.Help(new string[] {"help"}).ToLower();
        Assert.True(str.Contains("toppings")
        && str.Contains("pizza")
        && str.Contains("sauce"));
      }

      [Fact(DisplayName = "Default Behavior")]
      public void DefaultBevahior()
      {
        string str = HelpManager.Help(new string[] {}).ToLower();
        Assert.True(str.Contains("toppings")
        && str.Contains("pizza")
        && str.Contains("sauce"));
      }

      [Fact(DisplayName = "Bad Args Behavior")]
      public void BadArgsBehavior()
      {
        string str = HelpManager.Help(new string[] {"help", "with", "my", "order"}).ToLower();
        Assert.True(str.Contains("toppings")
        && str.Contains("pizza")
        && str.Contains("sauce"));
      }

      [Fact(DisplayName = "Toppings Request")]
      public void ToppingHelp()
      {
        string str = HelpManager.Help(new string[] {"help", "toppings"}).ToLower();
        Assert.True(str.Contains("pepperoni")
        && str.Contains("sausage")
        && str.Contains("bacon")
        && str.Contains("chicken")
        && str.Contains("ham")
        && str.Contains("beef")
        && str.Contains("steak")
        && str.Contains("salami")
        && str.Contains("onion")
        && str.Contains("mushrooms")
        && str.Contains("peppers")
        && str.Contains("olives")
        && str.Contains("tomatoes")
        && str.Contains("spinach")
        && str.Contains("jalapenos")
        && str.Contains("provolone")
        && str.Contains("cheddar"));
      }

      [Fact(DisplayName = "Pizza Help")]
      public void PizzaHelp()
      {
        string str = HelpManager.Help(new String[] {"help", "pizzas"}).ToLower();
        Assert.True(str.Contains("deluxe")
        && str.Contains("hawaiian")
        && str.Contains("veggie")
        && str.Contains("canadian")
        && str.Contains("meat"));
      }

      [Fact(DisplayName = "Sauce Help")]
      public void SauceHelp()
      {
        string str = HelpManager.Help(new String[] {"help", "sauce"}).ToLower();
        Assert.True(str.Contains("tomato")
        && str.Contains("marinara")
        && str.Contains("white")
        && str.Contains("alfredo"));
      }
    }
}