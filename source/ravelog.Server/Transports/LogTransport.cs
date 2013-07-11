using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaveLog.Server.Http.Models;

namespace RaveLog.Server.Transports
{
    public delegate EventHandler LogEntryReceivedHandler(object sender, LogEntry e);

    
    public abstract class LogTransport
    {
        public LogEntryReceivedHandler LogEntryReceived;

        public abstract void Open();
        public abstract void Close();

        protected void OnLogEntryReceived(object sender, LogEntry e)
        {
            var handler = LogEntryReceived;
            if (handler != null)
                handler(sender, e);
        }
        
    }
}
