using System.Collections.Generic;
using PizzaBot.Orders;

namespace PizzaBot.Interpretation  
{
  internal class Parser
  {
    internal static void Parse(Order o, List<Token> input)
    {
      List<Pizza> pizzas = new List<Pizza>();
      int pizzaPointer = -1;
      int pizzaCount = 0;
      bool AddState = true;
      int repeatCount = 0;
      // parse every line
      for(int i = 0; i < input.Count; i++)
      {
        Token t = input[i];
        switch (t.type) {
          case TokenType.NUMBER:
            pizzaCount += int.Parse(t.value);
            if(pizzaCount > pizzas.Count)
            {
              //resize
              int PizzaCountRaw = pizzas.Count;
              for(int j = 0; j < pizzaCount - PizzaCountRaw; j++)
              {
                pizzas.Add(new Pizza());
              }
            }
            if(input[i+1].type == TokenType.PIZZA || input[i+1].type == TokenType.SIZE)
            {
              repeatCount = int.Parse(t.value) - 1;
              pizzaPointer++;
            }
            else if(input[i+1].value != "pizzas" && input[i+1].value != "pizza")
            {
              pizzaPointer += int.Parse(t.value);
            }
            break;
          case TokenType.BASE:
            pizzas[pizzaPointer].SetBase(parseBase(t.value));
            break;
          case TokenType.PIZZA:
            pizzas[pizzaPointer] = parsePizza(t.value);
            break;
          case TokenType.SIZE:  
            Size s = parseSize(t.value);
            if(input[i+1].type == TokenType.PIZZA || input[i+1].type == TokenType.TOPPING) {
              Pizza p = parsePizza(input[++i].value, Base.TOMATO, s);
              pizzas[pizzaPointer] = p;
              for(; repeatCount > 0; repeatCount--)
              {
                pizzas[++pizzaPointer] = p;
              }
            } else {
              pizzas[pizzaPointer].SetSize(s);
            }
            break;
          case TokenType.TOPPING:
            if(AddState)
            {
              if(i < input.Count-1 && input[i+1].value == "half"
              || i < input.Count-2 && input[i+2].value == "half")
              {
                pizzas[pizzaPointer].AddHalfTopping(parseTopping(t.value));
                i += input[i+1].value == "half" ? 1 : 2;
              }
              else
              {
                pizzas[pizzaPointer].AddTopping(parseTopping(t.value));
              }
            }
            else
            {
              pizzas[pizzaPointer].RemoveTopping(parseTopping(t.value));
            }
            break;
          case TokenType.GRAMMAR:
            switch(t.value)
            {
              case "a" or "an":
                input[i--] = new Token(TokenType.NUMBER, "1"); // back up
                break;
              case "with":
                AddState = true;
                break;
              case "without" or "no":
                AddState = false;
                break;
              case "and":
                // and will be seen either in add state, or will be to leave remove state
                AddState = true;
                if(input[i+1].type != TokenType.TOPPING)
                {
                  //pizzaPointer++;
                }
                break;
              case "or":
                // or should only be seen in remove state
                break;
              case "on":
                if(input[i+1].value != "half")
                  goto case "half";
                break;
              case "half":
                if(input[i+1].type == TokenType.TOPPING)
                {
                  pizzas[pizzaPointer].AddHalfTopping(parseTopping(input[i+1].value));
                }
                break;
              default:
                break;
            }
            break; 
        }
      }

      o.Pizzas = pizzas;
    }


    private static Topping parseTopping(string s) => s switch
    {
      "pepperoni" => Topping.PEPPERONI,
      "sausage" => Topping.SAUSAGE,
      "bacon" => Topping.BACON, 
      "chicken" => Topping.CHICKEN, 
      "ham" => Topping.HAM, 
      "beef" => Topping.BEEF, 
      "steak" => Topping.STEAK, 
      "salami" => Topping.SALAMI,
      "onion" => Topping.ONION, 
      "mushrooms" => Topping.MUSHROOMS,
      "peppers" => Topping.PEPPERS,  
      "pineapple" => Topping.PINEAPPLE,
      "olives" => Topping.OLIVES, 
      "tomatoes" => Topping.TOMATOES, 
      "spinach" => Topping.SPINACH,
      "jalapenos" => Topping.JALAPENOS, 
      "provolone" => Topping.PROVOLONE, 
      "cheese" => Topping.CHEESE, 
      "cheddar" => Topping.CHEDDAR,
      _ => throw new System.Exception($"Unknown Topping {s}")
    };
    private static Base parseBase(string s) => s switch
    {
      "tomato" => Base.TOMATO,
      "marinara" => Base.MARINARA,
      "white" => Base.WHITE,
      "alfredo" => Base.ALFREDO,
      _ => throw new System.Exception($"Unknnown Base {s}")
    };

    private static Size parseSize(string s) => s switch
    {
      "small" or "s" => Size.SMALL,
      "medium" or "m" => Size.MEDIUM,
      "large" or "l" => Size.LARGE,
      _ => throw new System.Exception($"Unknown size {s}"),
    };

    private static Pizza parsePizza(string String, Base b = Base.TOMATO, Size s = Size.MEDIUM) => String switch
    {
      "cheese" => new Pizza(new List<Topping>(){Topping.CHEESE}, b, s),
      "pepperoni" => new Pizza(new List<Topping>(){Topping.CHEESE, 
                                                   Topping.PEPPERONI},b, s),
      "deluxe" => new Pizza(new List<Topping>(){Topping.CHEESE, 
                                                Topping.PEPPERONI, 
                                                Topping.HAM, 
                                                Topping.PEPPERS, 
                                                Topping.MUSHROOMS}, b, s),
      "hawaiian" => new Pizza(new List<Topping>(){Topping.CHEESE,
                                                  Topping.PINEAPPLE,
                                                  Topping.HAM,
                                                  Topping.BACON}, b, s),
      "veggie" => new Pizza(new List<Topping>(){Topping.CHEESE,
                                                Topping.PEPPERS,
                                                Topping.SPINACH,
                                                Topping.ONION,
                                                Topping.MUSHROOMS,
                                                Topping.TOMATOES,
                                                Topping.OLIVES}, b, s),
      "canadian" => new Pizza(new List<Topping>(){Topping.CHEESE,
                                                  Topping.BACON,
                                                  Topping.PEPPERONI,
                                                  Topping.MUSHROOMS}, b, s),
      "meat" => new Pizza(new List<Topping>(){Topping.CHEESE,
                                              Topping.BACON,
                                              Topping.PEPPERONI,
                                              Topping.SAUSAGE}, b, s),
      _ => throw new System.Exception($"")
    }; 
  }
}