using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console_Attendance_Mgt_system.login_signup;
namespace Console_Attendance_Mgt_system
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int ch;
            do
            {
            
            Console.WriteLine("1: signup\n2:login");
                Console.WriteLine("3:Sign Out\n Enter Choice");
                ch = int.Parse(Console.ReadLine());

            switch (ch)
            {
                case 1: Login_signup ls = new Login_signup();
                        ls.signup();
                         break;
                case 2: new Login_signup().login();                       
                        break;
                    case 3:Global.userid = null;
                        Global.userRole = null;
                        break;
                default:

                    break;
            }
                if (Global.userid==null)
                {
                    Console.WriteLine("Do you want to Signup (Y/N)");
                    char ch2 = Console.ReadLine()[0];
                    if (ch2=='N'||ch2=='n')
                    {
                        break;
                    } 
                }
                
            } while (true);

            Console.WriteLine("this is last statements");
            Console.ReadKey();
        }
    }
}
