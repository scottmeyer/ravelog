using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaveLog.Server.Http.Models;

namespace RaveLog.Server.Transports
{
    public delegate Task LogEntryReceivedHandler(object sender, LogEntry e);

    
    public abstract class LogTransport
    {
        public LogEntryReceivedHandler LogEntryReceived;

        public abstract void Open();
        public abstract void Close();

        public async Task OnLogEntryReceived(object sender, LogEntry e)
        {
            await Task.Run(async () =>
            {
                var handler = LogEntryReceived;
                if (handler != null)
                    await handler(sender, e);
            });
        }
    }
}
