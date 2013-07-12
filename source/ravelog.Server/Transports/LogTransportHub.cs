using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveLog.Server.Transports
{
    public class LogTransportHub
    {
        public LogTransportHub()
        {
            Transports = new List<LogTransport>();
        }

        public List<LogTransport> Transports { get; set; }

        public void InitializeTransports()
        {
            foreach (var transport in Transports)
            {
                transport.LogEntryReceived += async (sender, e) =>
                {
                    using (var session = Program.DocumentStore.OpenAsyncSession())
                    {
                        await session.StoreAsync(e);
                        await session.SaveChangesAsync();
                    }
                };

                transport.Open();
            }
        }

        public void CloseTransports()
        {
            Transports.ForEach(x => x.Close());
        }
    
    }
}
