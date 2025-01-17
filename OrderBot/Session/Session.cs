using System;
using PizzaBot.Database;
using PizzaBot.Interpretation;
using PizzaBot.Orders;

namespace PizzaBot.Sessions
{
  public class Session
  {
    private string id;
    private DBAccessor DB;
    private Order order;
    private SessionState state;
    internal SessionState State
    {
        get { return state; }
        set { state = value; }
    } 
    public Session(string id)
    {
      this.id = id;
      State = SessionState.GREETING;
      order = null;
      DB = new DBAccessor();
    }

    /*
      This function reads the state and progresses to the relevant operation
      of our FSM
    */
    public string Input(string from, string input)
    {
      return State switch 
        {
          SessionState.GREETING => Greet(),
          SessionState.ORDERING => Order(input, from),
          SessionState.ORDER_CONFIRM => OrderConfirm(input),
          SessionState.PAYMENT => Payment(),
          SessionState.PAYMENT_CONFIRM => PaymentConfirm(input),
          SessionState.COMPLETE => Complete(),
          _ => throw new Exception("Error: SessionState invalid")
        };
    }

    private string Order(string input, string from)
    {
      order = Interpreter.Interpret(input, from);
      this.State = SessionState.ORDER_CONFIRM;
      string ret = "Order recieved please confirm order:";

      ret += TextManager.FormatPizza(order);

      return ret + "\n\n Please Type \"yes\" or \"no\" to confirm your order now.";
    }
    private string OrderConfirm(string input)
    {
      if(input.Contains("yes"))
      {
        this.State = SessionState.PAYMENT;
        return "Wonderful! Please proceed with payment: \n" + Input(order.Customer, ""); // this is a hack to get around the fsm needing to be changed last minute
      }
      else if (input.Contains("no"))
      {
        this.State = SessionState.ORDERING;
        return "We're sorry, please try to order again. If you need help, type \"help\".";
      } 
      else 
      {
        return "Please type \"yes\" or \"no\" to confirm your order.";
      }
    }
    private string Payment()
    {
      this.State = SessionState.PAYMENT_CONFIRM;
      return $"The total for your order will be {order.CalculatePrice()}\nPlease Provide final verification on the order with [yes] or [no]";  
    }
    private string PaymentConfirm(string input)
    {
      if(input.Contains("yes"))
      {
        this.State = SessionState.COMPLETE;
        DBAccessor db = new DBAccessor();
        db.SaveOrder(order);
        return "The pizza is in the oven, and the driver will be there in 40 minutes.\nThank you for shopping with us!";
      }
      else if(input.Contains("no"))
      {
        this.State = SessionState.ORDERING;
        return "We're sorry. If you would like to try ordering something else, please feel free.";
      }
      else
      {
        return "Please type [yes] or [no] to confirm the order.";
      }
    }

    private string Complete()
    {
      return "Order Complete!";
    }

    private string Greet()
    {
      this.State = SessionState.ORDERING;
      return TextManager.Greet();
    }
  }
}