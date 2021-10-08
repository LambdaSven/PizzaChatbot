namespace PizzaBot.Sessions
{
  internal enum SessionState
  {
    GREETING,
    ORDERING,
    ORDER_CONFIRM,
    PAYMENT,
    PAYMENT_CONFIRM,
    COMPLETE
  }
}