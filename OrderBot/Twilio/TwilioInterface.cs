using System;
using System.Collections.Generic;
using System.Linq;
using PizzaBot.Database;
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
          return HelpManager.Help(body.ToLower().Split(" "));
        }
        else if (body.ToLower().Equals("history"))
        {
          DBAccessor db = new DBAccessor();
          return db.GetHistory(from)
                .Take(5)
                .Aggregate((s: "Your Last 5 Ordered Pizzas were;", i: 1),  
                  (acc, e) => (acc.s + "\n" + acc.i + ") " + e, acc.i+1))
                .Item1; // fold string together
        }
        else
        {
          return s.Input(from, body);
        }
      }
    }
}