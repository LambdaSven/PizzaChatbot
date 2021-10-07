using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaBot.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Twilio;
using Twilio.TwiML;


namespace wireless.Pages
{


 [IgnoreAntiforgeryToken(Order = 1001)]
    public class IndexModel : PageModel
    {
        private TwilioInterface twilio = new TwilioInterface();
        public ActionResult OnPost()
        {
            string sFrom = Request.Form["From"];
            string sBody = Request.Form["Body"];
            var oMessage = new Twilio.TwiML.MessagingResponse();

            oMessage.Message(twilio.OnMessage(sFrom, sBody));
            return Content(oMessage.ToString(), "application/xml");
        }
    }
}
