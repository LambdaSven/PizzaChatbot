using System;
using Xunit;
using OrderBot;
using PizzaBot.Interpretation;
using System.Collections.Generic;
using System.Linq;
using PizzaBot.Interface;

namespace OrderBot.tests
{
    [Collection("Tests of the Twilio Interface")]
    public class TwilioInterfaceTests
    {
      [Fact(DisplayName = "normal input")]
      public void NormalInput()
      {
        TwilioInterface test = new TwilioInterface();
        Assert.True(test.OnMessage("tester", "hello").Contains("Welcome to Jim's Pizzaria"));
      }

      [Fact(DisplayName = "history")]
      public void History()
      {
        TwilioInterface test = new TwilioInterface();
        Assert.True(test.OnMessage("test", "history").Contains("Your Last 5 "));
      }

      [Fact(DisplayName = "help test")]
      public void Help()
      {
        TwilioInterface test = new TwilioInterface();
        Assert.True(test.OnMessage("test", "help").Contains("help"));
      }
    }
}