using System;
using System.Collections.Generic;
using System.Text;

namespace Timelogger.Api.Tests.Lib
{
    public static class DateTimeExtensions
    {
        public static bool IsWithinLastMinute(this DateTime dateTime, int toleranceMinutes = 1)
        {
            DateTime now = DateTime.Now;
            return now.AddMinutes(-toleranceMinutes) <= dateTime && dateTime <= now;
        }
    }
}
