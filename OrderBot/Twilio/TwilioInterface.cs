using System;
using System.Collections.Generic;
using PizzaBot.Sessions;

namespace PizzaBot.Interface
{
    public class TwilioInterface
    {
      /*
      Handle the twilio interface, will need a dictionary of currently active
      sessions.

      dict<string, session>
      */
      Dictionary<string, Session> Sessions = new Dictionary<string, Session>();
      public string OnMessage(string from, string body)
      {
        if (!Sessions.ContainsKey(from))
        {
          Sessions.Add(from, new Session(from));
        }

        Session s = Sessions[from];
        if (body.ToLower().Split(" ")[0].Equals("help"))
        {
          return HelpManager.Help(from);
        }
        else
        {
          return s.Input(from);
        }
      }
    }
}