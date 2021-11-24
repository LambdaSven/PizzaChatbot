namespace PizzaBot.Orders
{
  public enum Size
  {
    SMALL, MEDIUM, LARGE
  }

  internal static class SizeExtension
  {
    public static decimal Factor(this Size size) => size switch
    {
      Size.SMALL => 0.9m,
      Size.MEDIUM => 1.0m,
      Size.LARGE => 1.1m,
      _ => 0m // this should never be reached
    };
  }
}