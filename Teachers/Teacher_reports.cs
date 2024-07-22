using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Attendance_Mgt_system.Teachers
{
    internal class Teacher_reports
    {
        public static void Show_Attendance_Class_subjectWise(int? teacher_id)
        {
            int classid=-1;
            string className="";
            TeacherTask.getClassID(out classid,out className);
            DateTime dt;
            Console.WriteLine("Enter Date to Show Attendance in DD-MM-YY format");
            dt = DateTime.Parse(Console.ReadLine());

            int subId;
            string subName;
            TeacherTask.getSubjectDetails(classid,out subId,out subName);
            Console.WriteLine("1: Present students\n2:Absent Student:\n3:Present & Absent Students\nEnter choice");
            int ch=int.Parse(Console.ReadLine());

            //--- fetch records using join

            SqlConnection con = new SqlConnection(Properties.Settings.Default.MyConString);
            con.Open();
            Console.WriteLine("########3date is:"+dt);
            SqlCommand cmd = new SqlCommand($"select ta.student_id,status,Fname,Lname from tblAttendance ta inner join tblStudent ts on ta.student_id=ts.student_id where subject_id={subId} AND  Attendance_Date=@attDate AND teacher_id={teacher_id} order by status,student_id ", con);
            cmd.Parameters.Add("@attDate", SqlDbType.Date).Value = dt;
            cmd.CommandType = CommandType.Text;
            SqlDataReader rdr= cmd.ExecuteReader();
            Console.WriteLine("Student ID\tFirst Name\t Last Name");
            while (rdr.Read())
            {

                if (ch == 1)  //for present student only
                {
                    if (rdr.GetBoolean(1))
                    {
                        Console.WriteLine($"{rdr[0]}\t\t{rdr[2]}\t\t{rdr[3]}");
                    }
                }
                else if (ch==2)//only absent
                {
                    if (rdr.GetBoolean(1)==false)
                    {
                        Console.WriteLine($"{rdr[0]}\t\t{rdr[2]}\t\t{rdr[3]}");
                    }
                }
                else  // all present and absent
                {                    
                    
                        Console.WriteLine($"{rdr[0]}\t\t{rdr[2]}\t\t{rdr[3]}");
                    
                }
            }
            con.Close();


        }
    }
}
