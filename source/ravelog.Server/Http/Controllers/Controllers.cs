using RaveLog.Server.Transports;
using RaveLog.Server.Http.Models;

namespace RaveLog.Server.Http.Controllers
{
    public class InformationLogController : LogControllerBase
    {
        public InformationLogController(LogTransport transport)
            : base(transport){}

        public override Severity Severity
        {
            get { return Severity.Information; }
        }
    }

    public class WarningLogController : LogControllerBase
    {
        public WarningLogController(LogTransport transport)
            : base(transport){}

        public override Severity Severity
        {
            get { return Severity.Warning; }
        }
    }
    public class ErrorLogController : LogControllerBase
    {
        public ErrorLogController(LogTransport transport)
            : base(transport){}

        public override Severity Severity
        {
            get { return Severity.Error; }
        }
    }

    public class TraceLogController : LogControllerBase
    {
        public TraceLogController(LogTransport transport)
            : base(transport){}

        public override Severity Severity
        {
            get { return Severity.Trace; }
        }
    }
}
