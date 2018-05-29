using log4net;
using System.Reflection;

namespace ScaleVoting.Extensions
{
    public static class Logger
    {
        public static ILog Log { get; private set; }

        public static void Init()
        {
            Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }
    }
}
