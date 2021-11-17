using System;
using PizzaBot.Interpretation;
using PizzaBot.Orders;

namespace PizzaBot.Sessions
{
  public class Session
  {
    private string id;

    private Order order;
    private SessionState state;
    private SessionState State
    {
        get { return state; }
        set { state = value; }
    } 
    public Session(string id)
    {
      this.id = id;
      State = SessionState.GREETING;
      order = null;
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
          SessionState.PAYMENT_CONFIRM => PaymentConfirm(),
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
        return "Wonderful! Please proceed with payment: \n" + Input(order.Customer, "");
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
      return "[Payment] Not Yet Implemented";
    }
    private string PaymentConfirm()
    {
      this.State = SessionState.COMPLETE;
      return "[Payment Confirm] Not Yet Implemented";
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