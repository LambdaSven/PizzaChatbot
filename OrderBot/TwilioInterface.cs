using System;

namespace PizzaBot.Interface
{
    public class TwilioInterface
    {
      /*
      Handle the twilio interface, will need a dictionary of currently active
      sessions.

      dict<string, session>
      */
      public string OnMessage(string from, string body)
      {
        return "";
      }
    }
}