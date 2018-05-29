using System;

namespace ScaleVoting.Extensions
{
    public class TimeStamp
    {
        public static string Get(DateTime value)
        {
            return value.ToString("yyyy'-'MM'-'dd HH':'mm':'ss'Z'");
        }
    }
}
