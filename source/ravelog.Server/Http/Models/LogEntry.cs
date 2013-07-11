using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveLog.Server.Http.Models
{
    public class LogEntry
    {
        public DateTime Date { get; set; }
        public string Host { get; set; }
        public string Application { get; set; }
        public string Identity { get; set; }
        public string Assembly { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public object Data { get; set; }
        public string Severity { get; set; }
    }

    public enum Severity
    {
        Information,
        Warning,
        Error,
        Trace
    }
}
