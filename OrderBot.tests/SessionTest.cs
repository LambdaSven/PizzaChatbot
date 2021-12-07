using System;
using Xunit;
using OrderBot;
using PizzaBot.Orders;
using System.Collections.Generic;
using System.Linq;
using PizzaBot.Sessions;

namespace OrderBot.tests
{
    [Collection("Tests of the Session Class")]
    public class SessionTests
    {
      [Fact(DisplayName = "Walk Through of the FSM")]
      public void Walkthrough()
      {
        Session s = new Session("tester");

        Assert.True(s.State == SessionState.GREETING);

        string res = s.Input("tester", "hello");
        Assert.True(res.Contains("Hello"));
        Assert.True(s.State == SessionState.ORDERING);

        res = s.Input("tester", "1 large pepperoni pizza");
        Assert.True(res.Contains("PEPPERONI") && res.Contains("confirm"));
        Assert.True(s.State == SessionState.ORDER_CONFIRM);

        res = s.Input("tester", "yes");
        Assert.True(res.Contains("Wonderful!"));
        Assert.True(s.State == SessionState.PAYMENT_CONFIRM);

        res = s.Input("tester", "yes");
        Assert.True(res.Contains("in the oven"));
        Assert.True(s.State == SessionState.COMPLETE);
      }

      [Fact(DisplayName = "Walk Through of the FSM with backups")]
      public void WalkthroughWBackups()
      {
        Session s = new Session("tester");

        Assert.True(s.State == SessionState.GREETING);

        string res = s.Input("tester", "hello");
        Assert.True(res.Contains("Hello"));
        Assert.True(s.State == SessionState.ORDERING);

        res = s.Input("tester", "1 large pepperoni pizza");
        Assert.True(res.Contains("PEPPERONI") && res.Contains("confirm"));
        Assert.True(s.State == SessionState.ORDER_CONFIRM);

        res = s.Input("tester", "no");
        Assert.True(res.Contains("sorry"));
        Assert.True(s.State == SessionState.ORDERING);

        res = s.Input("tester", "1 large pepperoni pizza with mushrooms on half");
        Assert.True(res.Contains("PEPPERONI") && res.Contains("confirm"));
        Assert.True(s.State == SessionState.ORDER_CONFIRM);

        res = s.Input("tester", "");
        Assert.True(res.Contains("Please type"));
        Assert.True(s.State == SessionState.ORDER_CONFIRM);

        res = s.Input("tester", "yes");
        Assert.True(res.Contains("Wonderful!"));
        Assert.True(s.State == SessionState.PAYMENT_CONFIRM);

        res = s.Input("tester", "");
        Assert.True(res.Contains("Please type"));
        Assert.True(s.State == SessionState.PAYMENT_CONFIRM);

        res = s.Input("tester", "no");
        Assert.True(res.Contains("We're sorry."));
        Assert.True(s.State == SessionState.ORDERING);
      }
    }
}