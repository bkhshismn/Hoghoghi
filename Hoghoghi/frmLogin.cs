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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {

            InitializeComponent();
        }
        clsGetDataSource cl = new clsGetDataSource();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        System.Globalization.PersianCalendar dt = new System.Globalization.PersianCalendar();
        string path = "";
        int sath = 0;
        private void frmLogin_Load(object sender, EventArgs e)
        {
            path = cl.GetDataSource();
            con.ConnectionString = @"" + path + "";
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();

            f.SathDasresi = sath;
            //try
            //{

                con.Open();
                cmd = new SqlCommand("Select UserID,usrName,usrPass,Name,SathDasresi from tblUser where usrName='" + txtName.Text + "' AND usrPass='" + txtPass.Text + "'");
                DataTable dt = new DataTable();
                cmd.Connection = con;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    int cunt = dt.Rows.Count;
                    int userid = Convert.ToInt32(dt.Rows[0]["UserID"]);
                    string username = dt.Rows[0]["usrName"].ToString();
                    string userpass = dt.Rows[0]["usrPass"].ToString();
                    string name = dt.Rows[0]["Name"].ToString();
                    sath = Convert.ToInt32(dt.Rows[0]["SathDasresi"]);
                    this.Hide();
                    f.UserName = username;
                    f.UserPass = userpass;
                    f.UserID = userid;
                    f.uName = name;
                    f.SathDasresi = sath;
                    f.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("نام کاربری یا گذرواژه اشتباه است");
                }
                con.Close();
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("مشکلی در ورود اطلاعات رخ داده است.");

            //}
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
