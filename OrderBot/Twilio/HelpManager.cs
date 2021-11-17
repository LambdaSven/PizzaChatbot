namespace PizzaBot.Interface
{
  static internal class HelpManager
  {
    public static string Help(string[] args)
    {
      //step 1 - parse help args
      if(args.Length == 1)
      {
        return "This is a pizza ordering service. You can" +
               "Order pizza by typing in an order such as " +
               "\"1 large hawaiian pizza without bacon\". " +
               "If you would like the list of toppings, or " +
               "of pizzas, please type \"help toppings\" or \"help pizzas\"";
      }
      else if (args[1] == "toppings")
      {
        return "The list of toppings is: \n\t" +
               "Pepperoni\n\tSausage\n\tBacon\n\tChicken\n\tHam\n\t" +
               "Beef\n\tSteak\n\tSalami\n\tOnion\n\tMushrooms\n\t" +
               "Peppers\n\tOlives\n\tTomatoes\n\tSpinach\n\t" +
               "Jalapenos\n\tProvolone\n\tCheese\n\tCheddar";
      }
      else if (args[1] == "pizza" || args[1] == "pizzas")
      {
        return "The list of Pizzas is:\n\t" +
        "Deluxe\n\tHawaiian\n\tVeggie\n\tCanadian\n\tMeat";
      }
      else
      {
         return "This is a pizza ordering service. You can" +
               "Order pizza by typing in an order such as " +
               "\"one large hawaiian pizza without bacon\". " +
               "If you would like the list of toppings, or " +
               "of pizzas, please type \"help toppings\" or \"help pizzas\"";
      }
    }
  }
}
