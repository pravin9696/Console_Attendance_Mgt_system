using Console_Attendance_Mgt_system.login_signup;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Attendance_Mgt_system.Teachers
{
    internal class TeacherTask
    {
        public void TeacherDashboard()
        {
            
            Console.WriteLine("--------Teacher DashBoard-------------");
            int Tchoice;
            do
            {

            
            Console.WriteLine("1: Update Profile\n2: Show Profile\n3:Take Attendance");
            Console.WriteLine("Enter Choice");
            Tchoice = Convert.ToInt32(Console.ReadLine());
            switch (Tchoice)
            {
                case 1: updateTeacherProfile();break;
                case 2: showTeacherProfile();break;
                case 3: TakeAttendance();break;
                default:
                    break;
            }
                Console.WriteLine("Do you want to continue (Y/N)");
                char ch2 = Console.ReadLine()[0];
                if (ch2 == 'N' || ch2 == 'n')
                {
                    break;
                }
            } while (true);
        }
        public void showTeacherProfile()
        {
            Console.WriteLine("Teacher Profile Show Successfully...");
        }

        public bool checkUserIDinTeacherTable(int? uid)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.MyConString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"select * from tblTeachers where userid={uid}");
                cmd.Connection = con;
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
            finally
            {
                con.Close();
            }
            
           

        }
        public void updateTeacherProfile()
        {
            if (Global.userid!=null)
            {
            Console.WriteLine("Enter First Name and Last Name");
            string fname = Console.ReadLine();
            string lname = Console.ReadLine();
                SqlConnection con = new SqlConnection(Properties.Settings.Default.MyConString);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                bool temp = checkUserIDinTeacherTable(Global.userid);
                if (temp == true)
                {
                    cmd.CommandText = $"update tblTeachers set Fname='{fname}',Lname='{lname}' where userid="+Global.userid;
                }
                else
                {
                    cmd.CommandText = $"insert into tblTeachers values({Global.userid},'{fname}','{lname}')";
                }

                int n = cmd.ExecuteNonQuery();
                if (n>0)
                {
                    Console.WriteLine("Record inserted / updated Successfully....");
                }
                con.Close();



            }
            else
            {
                Console.WriteLine("Please login First");                
                new Login_signup().login();
            }
        }
        public void TakeAttendance()
        {
            Console.WriteLine("Attendance taken by **** teacher successfully..");
        }
    }
}
