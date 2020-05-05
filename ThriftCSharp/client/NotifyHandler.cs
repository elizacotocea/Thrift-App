using app.client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    class NotifyHandler : NotifyService.Iface
    {
        public string notify(string message)
        {
            mainPage.needsUpdate = true;
            return "hiii";
        }
    }
}
