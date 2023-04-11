using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteCopier;

namespace GearTools.Util
{
    public class Functions 
    {
        public static IHubContext hubContext { get; set; }

        public static void SendProgress(string progressMessage, int progressCount, int totalItems)
        {
            //IN ORDER TO INVOKE SIGNALR FUNCTIONALITY DIRECTLY FROM SERVER SIDE WE MUST USE THIS

            //CALCULATING PERCENTAGE BASED ON THE PARAMETERS SENT
            var percentage = (progressCount * 100) / totalItems;

            //PUSHING DATA TO ALL CLIENTS
            hubContext.Clients.All.AddProgress(progressMessage, percentage + "%");
        }
        public static void SendMessage(string progressMessage)
        {
            //IN ORDER TO INVOKE SIGNALR FUNCTIONALITY DIRECTLY FROM SERVER SIDE WE MUST USE THIS
            //PUSHING DATA TO ALL CLIENTS
            hubContext.Clients.All.SendMessage(progressMessage);
        }

        public static void SendLogMessage(string message)
        {
            hubContext.Clients.All.SendLogMessage(message);
        }

    }
}