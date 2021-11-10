using PizzaBot.Orders;

namespace PizzaBot.Interpretation
{
  public class Interpreter
  {
    public static Order Interpret(string input, string from)
    {
      Order o = new Order(from);
      Parser.Parse(o, Lexer.scan(input));
      return o;
    } 
  }
}