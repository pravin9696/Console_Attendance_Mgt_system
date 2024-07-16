using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Attendance_Mgt_system.login_signup
{
    internal class Login_signup
    {
        public void signup()
        {
            string uname,password,role;
            int r;
            do
            {
            Console.WriteLine("Enter User Name");
            uname=Console.ReadLine();
            Console.WriteLine("Enter password");
            password=Console.ReadLine();
           
            
            Console.WriteLine("select Role..");
            Console.WriteLine("1:Student\n 2: Teacher\nEnter Choice");
            r = int.Parse(Console.ReadLine());
            if (r==1)
            {
                role = "Student";
                    break;
            }
            else if (r==2)
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
            SqlConnection con = new SqlConnection(Global.conString);
           SqlCommand cmd = new SqlCommand($"insert into tblUsers values('{uname}','{password}','{role}')", con);

            // SqlCommand cmd = new SqlCommand();
            // cmd.Connection = con;
            // cmd.CommandText = $"insert into tblUsers values('{uname}','{password}','{role}')";
            con.Open();
            int n = cmd.ExecuteNonQuery();
            if (n>0)
            {
                Console.WriteLine("Sign up Completed Sucessfully");

            }


        }
        public void login() {
            Console.WriteLine("This is login logic..");
            Global.userid = 123;
            Global.userRole = "Student";
        }
        public void logout()
        {
            Global.userid = null;
            Global.userRole = null;
        }

    }
}
