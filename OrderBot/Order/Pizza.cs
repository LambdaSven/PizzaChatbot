using System.Collections.Generic;
using System.Linq;

namespace PizzaBot.Orders
{
  internal class Pizza
  {
    public List<Topping> FullToppings;
    public List<Topping> HalfToppings;
    public Base Base;
    public Size Size;
    public Pizza()
    {
      FullToppings = new List<Topping>() {Topping.CHEESE};
      HalfToppings = new List<Topping>();
      Base = Base.TOMATO;
      Size = Size.MEDIUM;   
    }

    public Pizza(List<Topping> toppings, Base Base = Base.TOMATO, Size Size = Size.MEDIUM)
    {
      this.FullToppings = toppings;
      this.HalfToppings = new List<Topping>();
      this.Base = Base;
      this.Size = Size;
    }

    public void AddTopping(Topping t) 
    {
        FullToppings.Add(t);
    }

    public void AddHalfTopping(Topping t)
    {
      if(FullToppings.Contains(t))
      {
        FullToppings.Remove(t);
      }
      HalfToppings.Add(t);
    }

    public void RemoveTopping(Topping t) 
    {
      if(FullToppings.Contains(t))
      {
        FullToppings.Remove(t);
      }
      if(HalfToppings.Contains(t))
      {
        HalfToppings.Remove(t);
      }
    }  

    public void SetBase(Base b)
    {
      this.Base = b;
    }

    public void SetSize(Size s)
    {
      this.Size = s;
    }

    public override bool Equals(object obj)
    {
      if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
      {
         return false;
      }
      else
      {
        Pizza p = (Pizza) obj;
        return p.Base == this.Base 
            && p.Size == this.Size 
            && p.FullToppings.SequenceEqual(this.FullToppings)
            && p.HalfToppings.SequenceEqual(this.HalfToppings);
        
      }
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}