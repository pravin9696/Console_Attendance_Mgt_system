using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Attendance_Mgt_system
{
    public static class Global
    {
        public static string conString = "Data Source=.\\sqlexpress;Initial Catalog=DBAttendanceMgtSystem;Integrated Security=True;TrustServerCertificate=True";
        public static int? userid=null;
        public static String userRole=null;
    }
}
