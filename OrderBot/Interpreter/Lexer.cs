using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
    static string[] sizes = 
    {
      "small", "medium", "large", "s", "m", "l"
    };
    static string[] pizzas =
    {
     "cheese", "pepperoni", "deluxe", "hawaiian", "veggie", "canadian",
      "meat", 
    };

    static string[] grammar = 
    {
      "a", "an", "with", "without", "and", "or", "no", "pizza", "pizzas", "half"
    };
    public static List<Token> scan(string src)
    {
      List<Token> ret = new List<Token>();

      // remove trim whitespace, and split on any set of one or more whitespace characters
      foreach (string s in Regex.Split(src.Trim(), @"\s+"))
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
      _ when sizes.Contains(input) => new Token(TokenType.SIZE, input),
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