using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hoghoghi
{
    public partial class frmSortKarshenas : Form
    {
        public frmSortKarshenas()
        {
            InitializeComponent();
        }
        
        string path = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        System.Globalization.PersianCalendar dt = new System.Globalization.PersianCalendar();
        clsGetDataSource gds = new clsGetDataSource();
        public struct User
        {
            public string Name { get; set; }
            public Int64 Daramad { get; set; }
            public void AvardeKarmozd(string name, int years)
            {
                Int64 hoghogh = 0;
                try
                {
                    SqlConnection con = new SqlConnection();
                    string path = "";
                    clsGetDataSource gds = new clsGetDataSource();
                    path = gds.GetDataSource();
                    con.ConnectionString = @"" + path + "";
                    string query = "SELECT * from View_UserToPardakht where  Name='" + name + "' and Years=" + years;
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    con.Open();
                    cmd.ExecuteScalar();
                    con.Close();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        hoghogh += Convert.ToInt64(dt.Rows[i]["Mablagh"]);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("خطایی در نمایش اطلاعات تابع DisplayAvardeKarmozd رخ داده است");
                }
                Name = name;
                Daramad = hoghogh;
            }
        }
        //public string[] GetUserName()
        //{
        //    string[] nm;
        //    try
        //    {
        //        con.Close();
        //        string query = "SELECT * from tblUser ";
        //        SqlCommand cmd = new SqlCommand(query, con);
        //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        sda.Fill(dt);
        //        string[] name = new string[dt.Rows.Count];
        //        con.Open();
        //        cmd.ExecuteScalar();
        //        con.Close();
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            name[i] = dt.Rows[i]["Name"].ToString();
        //        }
        //        nm = name;
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("خطایی در  تابع  رخ داده است");
        //    }
            //return nm;
        //}
        public Int64 DisplayAvardeKarmozd(string name,int years)
        {
            Int64 hoghogh = 0;
            try
            {
                con.Close();
                string query = "SELECT * from View_UserToPardakht where  Name='" + name + "' and Years=" + years;
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    hoghogh += Convert.ToInt64(dt.Rows[i]["Mablagh"]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش اطلاعات تابع DisplayAvardeKarmozd رخ داده است");
            }
            return hoghogh;
        }
        private void frmSortKarshenas_Load(object sender, EventArgs e)
        {
            //path = gds.GetDataSource();
            //con.ConnectionString = @"" + path + "";
            //string[] name = GetUserName();
            //User[] user = new User[name.Length];
            //Int64[] karkard = new Int64[name.Length];
            //for (int i = 0; i < name.Length ; i++)
            //{
            //    user[i].AvardeKarmozd(name[i], dt.GetYear(DateTime.Now));
            //}
            //for (int i = 0; i < name.Length ; i++)
            //{
            //    listBox1.Items.Add("  نام:  " + user[i].Name);
            //    listBox1.Items.Add(" میزان درامد:   " + user[i].Daramad.ToString("N0") + "\n\n\n\n\n\n");
            //}
        }
    }
}
