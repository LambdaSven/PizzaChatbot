using System;
using Xunit;
using PizzaBot.Orders;
using System.Collections.Generic;

namespace OrderBot.tests
{
  [Collection("Tests of the Pizza Class")]
  public class PizzaTests
  {
    [Fact(DisplayName = "Verify Adding Topping is Successful")]
    public void AddToppingTest()
    {
      Pizza p = new Pizza();
      p.AddTopping(Topping.PINEAPPLE);

      Assert.Equal(p, new Pizza(new List<Topping> {Topping.CHEESE, Topping.PINEAPPLE}));
    }
    [Fact(DisplayName = "Verify Adding Half Topping is Successful")]
    public void AddHalfToppingTest()
    {
      Pizza p = new Pizza();
      p.AddHalfTopping(Topping.PEPPERONI);
      Assert.Contains(Topping.PEPPERONI, p.HalfToppings);
      Assert.DoesNotContain(Topping.PEPPERONI, p.FullToppings);
    }
    [Fact(DisplayName = "Test Remove Full Topping")]
    public void RemoveFullToppingTest()
    {
      Pizza p = new Pizza(new List<Topping> {Topping.BACON, Topping.BEEF});

      p.RemoveTopping(Topping.BACON);
      Assert.DoesNotContain(Topping.BACON, p.FullToppings);
    }
    [Fact(DisplayName = "Verify removing half topping is successful")]
    public void RemoveHalfToppingTest()
    {
      Pizza p = new Pizza();
      p.AddHalfTopping(Topping.CHICKEN);
      p.AddHalfTopping(Topping.HAM);
      p.AddTopping(Topping.SAUSAGE);

      p.RemoveTopping(Topping.CHICKEN);
      Assert.Contains(Topping.HAM, p.HalfToppings);
      Assert.DoesNotContain(Topping.CHICKEN, p.HalfToppings);
      Assert.Contains(Topping.SAUSAGE, p.FullToppings);
    }
  }
}