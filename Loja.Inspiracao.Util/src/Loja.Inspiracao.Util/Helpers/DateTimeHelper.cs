#nullable disable

using System;
using System.Runtime.InteropServices;

namespace Loja.Inspiracao.Util.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime GetTimeZoneDate()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Brazilian Standard Time"))
                : TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo"));
        }
    }
}
