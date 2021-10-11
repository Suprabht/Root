using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyVisitors.EnumConstants
{
    public class Settings
    {
        public static string ConnectionString { get; set; }
        public static string SmtpHost { get; set; }
        public static int SmtpPort { get; set; }
        public static string SmtpPass { get; set; }
        public static string SmtpUser { get; set; }
        public static string From { get; set; }
    }
}
