using System;
using Xunit;
using OrderBot;
using PizzaBot.Interpretation;
using System.Collections.Generic;
using System.Linq;
using PizzaBot.Interface;
using PizzaBot.Database;
using PizzaBot.Orders;

namespace OrderBot.tests
{
    [Collection("Tests of the DBAccessor")]
    public class DBAccessorTests
    {
      [Fact(DisplayName = "Commit Order")]
      public void CommitOrder()
      {
        DBAccessor db = new DBAccessor();
        Order o = new Order("tester", "1 pepperoni pizza");
        Pizza p = new Pizza(
          new List<Topping>() {Topping.CHEESE, Topping.PEPPERONI}
        );
        o.AddPizza(p);
        Assert.True(db.SaveOrder(o));
      }

      [Fact(DisplayName = "View History")]
      public void ViewHistory()
      {
        DBAccessor db = new DBAccessor();
        String s = System.Guid.NewGuid().ToString("N");
        Order o = new Order(s, "1 pepperoni pizza");
        Pizza p = new Pizza(
          new List<Topping>() {Topping.CHEESE, Topping.PEPPERONI}
        );
        o.AddPizza(p);
        Assert.True(db.SaveOrder(o));

        Assert.True(db.GetHistory(s).Count == 1);
      }
    }
}