using Console_Attendance_Mgt_system.login_signup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Console_Attendance_Mgt_system.Teachers
{
    internal class TeacherTask
    {
        

        public void getTeacherID(out int? Tid)
        {
            Tid = null;
            if (Global.userid != null)
            {
                SqlConnection con = new SqlConnection(Properties.Settings.Default.MyConString);
                con.Open();
                SqlCommand cmd = new SqlCommand($"select teacher_id from tblTeachers where userid={Global.userid}");
                cmd.Connection = con;
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read()) 
                {
                    Tid = rdr.GetInt32(0); 
                }
                else
                {
                    Console.WriteLine("Teacher Profile Not updated yet... \nPlease update Teacher profile");
                    Tid = null;
                    updateTeacherProfile();
                    getTeacherID(out Tid);
                }
            }
            else
            {
                Console.WriteLine("Please login ..");
                Tid = null;
            }
        }

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
            if (Global.userid != null)
            {
                SqlConnection con = new SqlConnection();
                try
                {
                    
                    con.ConnectionString = Properties.Settings.Default.MyConString;
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = $"select * from tblTeachers where userid={Global.userid}";
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        
                        Console.WriteLine("First Name :" + rdr["Fname"]);
                        Console.WriteLine($"Last Name : {rdr["Lname"].ToString()}");
                    }
                    else
                    {
                        Console.WriteLine($"Profile of {Global.userid} is not available.....");
                    }                   

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                Console.WriteLine("Please login...");
                new Login_signup().login();
            }
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

        public SqlDataReader GetStudents(int class_id)
        {

            SqlConnection con = new SqlConnection(Properties.Settings.Default.MyConString);
            con.Open();
            SqlCommand cmd = new SqlCommand($"select * from tblStudent where class_id={class_id}");
            cmd.Connection = con;
            SqlDataReader rdr = cmd.ExecuteReader();
            return rdr;
        }
        public void getSubjectDetails(int classID,out int subId,out string subName)
        {
            
            subId = -1;
            subName = "";
            SqlConnection con = new SqlConnection(Properties.Settings.Default.MyConString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from tblSubjects where class_id="+classID);
            cmd.Connection = con;
            SqlDataReader rdr = cmd.ExecuteReader();
            Console.WriteLine("Subject ID\tSubject Name");
            while (rdr.Read())
            {
                Console.WriteLine(rdr[0] + "\t" + rdr[1]);
            }
            Console.WriteLine("Enter Subject ID");
            int sid = int.Parse(Console.ReadLine());
            //-----------------------------------------------------------------------
            SqlCommand cmd1 = new SqlCommand($"select * from tblSubjects where class_id={classID} and subject_id={sid}",con);
            rdr.Close();
            SqlDataReader temprdr = cmd1.ExecuteReader();
            if (temprdr.Read())
            {
                subId = sid;
                subName = temprdr.GetString(1);
            }
            else
            {
                Console.WriteLine("Wrong Subject ID selected!!!!!!");
                getSubjectDetails(classID, out subId, out subName);
            }

            con.Close();
           

        }
        public void getClassID(out int clsid,out string clsName)
        {
            clsid = -1;
            clsName = "";
            SqlConnection con = new SqlConnection(Properties.Settings.Default.MyConString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from tblClasses");
            cmd.Connection = con;
            SqlDataReader rdr=cmd.ExecuteReader();
            
            Console.WriteLine("Class ID\tClass Name");
            while (rdr.Read()) {
                Console.WriteLine(rdr[0] + "\t" + rdr[1]);
            }            
            Console.WriteLine("Enter Class ID");
            int cid = int.Parse(Console.ReadLine());
            //-----------------------------------------------------------------------
            SqlCommand cmd1 = new SqlCommand("select *from tblClasses where class_id=" + cid,con);
            rdr.Close();
            SqlDataReader temprdr = cmd1.ExecuteReader();
            if (temprdr.Read())
            {               
                    clsid = cid;
                    clsName = temprdr.GetString(1);               
            }            
            
            con.Close();
            
        }
    
        public void TakeAttendance()
        {
            //----------- collect class id, class Name and respective student list 
            int classId;
            string className;
            getClassID(out classId,out className);

            SqlDataReader studsRdr = GetStudents(classId);
            
            //----------------- collect system date
            DateTime dt = DateTime.Now;
            
            //----------------- collect subject ID and subject Name
            string subName = "";
            int subId = 0;
            getSubjectDetails(classId, out subId, out subName);
            //--------show all students and add attendance in attendance table--------------
            Console.Clear();
            Console.WriteLine("Date :"+dt.ToShortDateString());
            Console.WriteLine("Class:"+className);
            Console.WriteLine("Subject :"+subName);
            Console.WriteLine("-------Student list--------");
            int studId;
            int ?TeacherID = null;
            getTeacherID(out TeacherID);
            while (studsRdr.Read())
            {
                studId= studsRdr.GetInt32(0);
                Console.WriteLine( studsRdr[2] + "\t" + studsRdr[3]);
                Console.WriteLine("1 : Present\n0:Absent\n Please enter number:");
                int attendanceMark = int.Parse(Console.ReadLine());
                markAttendance(TeacherID, studId, subId, dt, attendanceMark);
            }
            Console.WriteLine("----Attendance Completed Successfully---------------..");
        }
        public void markAttendance(int? tid,int stid,int subid,DateTime dt,int status)
        {
          
            //SqlCommand cmd = new SqlCommand($"insert into tblAttendance(teacher_id, student_id, subject_id, Attendance_Date, status)values({tid},{stid}, {subid}, '{dt}', {status})");

            //SqlCommand cmd = new SqlCommand($"insert into tblAttendance values({tid},{stid}, {subid}, '{DateTime.Parse(dt.ToShortDateString())}', {status})");
            //cmd.Connection = con;
           // int n=cmd.ExecuteNonQuery();
            //if (n > 0) {
            //    Console.WriteLine("Attendance marked successfully..");
            //}
            ///---------------------------------------------
            SqlConnection con = new SqlConnection(Properties.Settings.Default.MyConString);

            SqlDataAdapter adp = new SqlDataAdapter("Select * from tblAttendance", con);
            DataSet ds = new DataSet();
            adp.Fill(ds, "tblAttendance");
            SqlCommandBuilder cmdb = new SqlCommandBuilder(adp);
            DataRow dr = ds.Tables["tblAttendance"].NewRow();
            //,,,
            dr["teacher_id"] = tid;
            dr["student_id"] = stid;
            dr["subject_id"] = subid;
            dr["Attendance_Date"] = dt;
            dr["status"] = status;
            

            ds.Tables["tblAttendance"].Rows.Add(dr);

           int n= adp.Update(ds, "tblAttendance");
            if (n > 0)
              {
                   Console.WriteLine("Attendance marked successfully..");
              }
            }
        }
}
