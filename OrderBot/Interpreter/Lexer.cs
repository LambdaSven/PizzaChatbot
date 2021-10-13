using System.Collections.Generic;
using System.Linq;

namespace PizzaBot.Interpretation
{
  internal class Lexer
  {
    static string[] toppings = 
    {
      "pepperoni", "sausage", "bacon", "chicken", "ham", "beef", "steak", "salami",
      "onion", "mushrooms", "peppers", "pineapple", "olives", "tomatoes", "spinach",
      "jalapenos", "provolone", "cheese", "cheddar"
    };
    static string[] bases = 
    {
      "tomato", "marinara", "white", "alfredo"
    };
    static string[] pizzas =
    {
     "cheese", "pepperoni", "deluxe", "hawaiian", "veggie", "canadian",
      "meat", 
    };

    static string[] grammar = 
    {
      "a", "an", "with", "without", "and", "or", "no", "pizza", "half"
    };
    public static List<Token> scan(string src)
    {
      List<Token> ret = new List<Token>();

      foreach (string s in src.Split(" "))
      {
        ret.Add(Tokenise(s.ToLower()));
      }
      
      return ret;
    }

    /*
      Converts a string into a Token based on the table at the top of the file.
    */
    private static Token Tokenise(string input) => input switch
    {
      _ when toppings.Contains(input) => new Token(TokenType.TOPPING, input),
      _ when bases.Contains(input) => new Token(TokenType.BASE, input),
      _ when pizzas.Contains(input) => new Token(TokenType.PIZZA, input),
      _ when grammar.Contains(input) => new Token(TokenType.GRAMMAR, input),
      _ when isInt(input) => new Token(TokenType.NUMBER, input),
      _ => new Token(TokenType.UNKNOWN, input)
     };

     private static bool isInt(string s)
     {
       int x;
       return (int.TryParse(s, out x));
     }
  }
}