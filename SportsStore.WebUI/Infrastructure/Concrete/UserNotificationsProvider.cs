using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using SportsStore.WebUI.Infrastructure.Abstract;

namespace SportsStore.WebUI.Infrastructure.Concrete
{
    public class UserNotificationsProvider : INotificationsProvider
    {


        public string Data()
        {
            return Data("message");
        }

        public string Data(string eventName)
        {
            string result = String.Empty;
            if (eventName == "message")
            {
                var jsonSerializer = new JavaScriptSerializer();
            }

            return result;
        }
    }
}