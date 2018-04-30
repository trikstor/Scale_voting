using System;

namespace ScaleVoting.Extensions
{
    public class TimeStamp
    {
        public static string Get(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }
}
