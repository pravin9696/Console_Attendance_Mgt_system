using Console_Attendance_Mgt_system.students;
using Console_Attendance_Mgt_system.Teachers;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Console_Attendance_Mgt_system.login_signup
{
    internal class Login_signup
    {
        public void signup()
        {
            string uname, password, role;
            int r;
            do
            {
                Console.WriteLine("Enter User Name");
                uname = Console.ReadLine();
                Console.WriteLine("Enter password");
                password = Console.ReadLine();


                Console.WriteLine("select Role..");
                Console.WriteLine("1:Student\n 2: Teacher\nEnter Choice");
                r = int.Parse(Console.ReadLine());
                if (r == 1)
                {
                    role = "Student";
                    break;
                }
                else if (r == 2)
                {
                    role = "Teacher";
                    break;
                }
                else
                {
                    Console.WriteLine("wrong choice....");
                    Console.WriteLine("Select 1 or 2 for role");
                }
            } while (true);
            ///////////////////////////////// Insert logic////////////////////////
            //SqlConnection con = new SqlConnection(Global.conString);
            SqlConnection con = new SqlConnection(Properties.Settings.Default.MyConString);
            SqlCommand cmd = new SqlCommand($"insert into tblUsers values('{uname}','{password}','{role}')", con);

            // SqlCommand cmd = new SqlCommand();
            // cmd.Connection = con;
            // cmd.CommandText = $"insert into tblUsers values('{uname}','{password}','{role}')";
            con.Open();
            int n = cmd.ExecuteNonQuery();
            if (n > 0)
            {
                Console.WriteLine("Sign up Completed Sucessfully");

            }
            con.Close();

        }
        public void login()
        {
            string uname, pass;
            Console.WriteLine("Enter USer name and password");
            uname = Console.ReadLine();
            pass = Console.ReadLine();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = Properties.Settings.Default.MyConString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = $"select user_id,role from tblUsers where userName='{uname}' AND password='{pass}'";
            cmd.CommandType = CommandType.Text;
             SqlDataReader rdr= cmd.ExecuteReader();
            if (rdr.Read()) {

                Global.userid = int.Parse(rdr[0].ToString());
                Global.userRole = rdr["role"].ToString();
                Console.WriteLine("Login Successful...");
                   if (Global.userRole == "Teacher")
                      {
                        TeacherTask tt = new TeacherTask();
                        tt.TeacherDashboard();
                      }

                // if (string.Compare(Global.userRole.ToLower(),"StuDent".ToLower())==0)
                if (string.Equals(Global.userRole,"Student",StringComparison.OrdinalIgnoreCase))                
                {
                    new StudentTask().StudentDashBoard();
                }
            }
            else
            {
                Console.WriteLine("Invalid user Name or password");
            }
            con.Close();
            Console.WriteLine("Do you want to continue (Y/N)");
            char ch2 = Console.ReadLine()[0];
            if (ch2 == 'Y' || ch2 == 'y')
            {
                login();
            }

            //Console.WriteLine("This is login logic..");
            //Global.userid = 123;
            //Global.userRole = "Teacher";


        }
        public void logout()
        {
            Global.userid = null;
            Global.userRole = null;
        }

    }
}
