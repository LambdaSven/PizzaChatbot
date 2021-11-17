
namespace PizzaBot.Interpretation 
{
  /*
    A token is the fundemental unit of our grammar. A token has a type,
    and some underlying value.
  */
  internal class Token
  {
    internal TokenType type;
    internal string value;
    
    public Token(TokenType type, string value)
    {
      this.type = type; 
      this.value = value;
    }

    // this override is primarily for testing purposes. the parser will never 
    // need to do string comparisons.
    public override bool Equals(object obj)
    {
      if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
      {
         return false;
      }
      else
      {
        Token t = (Token) obj;
        return this.type == t.type && this.value == t.value;
      }
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}