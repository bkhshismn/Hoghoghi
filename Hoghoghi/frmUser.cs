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
    public partial class frmUser : Form
    {
        public frmUser()
        {
            InitializeComponent();
        }
        string path = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        System.Globalization.PersianCalendar dt = new System.Globalization.PersianCalendar();
        clsGetDataSource gds = new clsGetDataSource();
        int sath = -1;
        int ID = -1;
        void Display()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adb = new SqlDataAdapter();
            adb.SelectCommand = new SqlCommand();
            adb.SelectCommand.Connection = con;
            adb.SelectCommand.CommandText = "select * from tblUser ORDER BY UserID desc";
            adb.Fill(ds, "tblUser");
            dgvUser.DataSource = ds;
            dgvUser.DataMember = "tblUser";
            dgvUser.Columns[0].HeaderText = "کد";
            dgvUser.Columns[0].Width = 50;
            dgvUser.Columns[1].HeaderText = "نام";
            dgvUser.Columns[1].Width = 150;
           dgvUser.Columns[2].HeaderText = "نام کاربری";
            dgvUser.Columns[3].HeaderText = "کلمه عبور";
            dgvUser.Columns[4].HeaderText = "شماره همراه";
            dgvUser.Columns[5].HeaderText = "سطح دسرسی";
            
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtUName.Text == ""||txtPass.Text==""||txtRePass.Text=="")
            {
                MessageBox.Show( "فیلد های خالی را پر کنید...");
               
            }
            else
            {
                if (txtPass.Text != txtRePass.Text)
                {
                    MessageBox.Show("گذروازه ها همسان نیست...");
                }
                else {
                    try
                    {
                        if (chkKamel.Checked == true)
                        {
                            sath = 1;
                        }
                        else
                        {
                            sath = 0;
                        }

                        cmd.Parameters.Clear();
                        cmd.Connection = con;
                        cmd.CommandText = "insert into tblUser(Name,usrName,usrPass,usrTel,SathDasresi)values(@Name,@usrName,@usrPass,@usrTel,@SathDasresi)";
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@usrName", txtUName.Text);
                        cmd.Parameters.AddWithValue("@usrPass", txtPass.Text);
                        cmd.Parameters.AddWithValue("@usrTel", txtTell.Text);
                        cmd.Parameters.AddWithValue("@SathDasresi", sath);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        Display();
                        MessageBox.Show("ثبت با موفقیت انجام شد");
                       
                        txtName.Text = "";
                        txtPass.Text = "";
                        txtUName.Text = "";
                        txtRePass.Text = "";
                        txtTell.Text = "";
                        chkMahdod.Checked = true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("مشکلی در ثبت اطلاعات وجود دارد!");
                    }
                }

                }
            }
        private void chkMahdod_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMahdod.Checked == true)
            {
                chkKamel.Checked = false;
            }
        }
        private void chkKamel_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKamel.Checked == true)
            {
                chkMahdod.Checked = false;
            }
        }
        private void frmUser_Load(object sender, EventArgs e)
        {
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
            Display();
            chkMahdod.Checked = true;
        }
        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvUser.Rows[e.RowIndex].Selected = true;
            txtPass.PasswordChar ='\0' ;
            txtRePass.PasswordChar = '\0';
            try
            {
                ID = (int)dgvUser.Rows[e.RowIndex].Cells["UserID"].Value;
                cmd.Parameters.Clear();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from [tblUser] where UserID =" + ID;
                con.Open();
                adp.Fill(dt);
                this.txtName.Text = dt.Rows[0]["Name"].ToString();
                this.txtPass.Text = dt.Rows[0]["usrPass"].ToString();
                this.txtRePass.Text = dt.Rows[0]["usrPass"].ToString();
                this.txtUName.Text = dt.Rows[0]["usrName"].ToString();
                this.txtTell.Text = dt.Rows[0]["usrTel"].ToString();
                int sath = (int)dt.Rows[0]["SathDasresi"];
                if (sath == 1)
                {
                    chkKamel.Checked = true;
                }
                else chkMahdod.Checked = true;
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در انتخاب رکورد رخ داده است");
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            int esath = -1;
            if (ID == -1)
            {
                MessageBox.Show("لطفا یک رکورد را انتخاب کنید...");
            }
            else
            {
                if (txtName.Text == "" || txtUName.Text == "" || txtPass.Text == "" || txtRePass.Text == "")
                {
                    MessageBox.Show("فیلد های خالی را پر کنید...");

                }
                else
                {
                    if (txtPass.Text != txtRePass.Text)
                    {
                        MessageBox.Show("گذروازه ها همسان نیست...");
                    }
                    else
                    {
                        var result = MessageBox.Show("آیا مایل به ویرایش رکورد هستتید؟", "هشدار", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                if (chkKamel.Checked == true)
                                {
                                    esath = 1;
                                }
                                else
                                {
                                    esath = 0;
                                }
                                //txtName.Text = "";
                                //txtPass.Text = "";
                                //txtUName.Text = "";
                                //txtRePass.Text = "";
                                cmd.Parameters.Clear();
                                cmd.Connection = con;
                                cmd.CommandText = "UPDATE tblUser set Name=N'" + txtName.Text + "' ,usrName=N'" + txtUName.Text + "' ,usrPass=N'" + txtPass.Text + "' ,usrTel=N'" + txtTell.Text + "' ,SathDasresi='" + esath + "' where UserID=" + ID;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                                Display();
                                MessageBox.Show("ویرایش اطلاعات انجام شد.");
                                txtTell.Text = "";
                                txtPass.PasswordChar = '*';
                                txtRePass.PasswordChar = '*';
                                txtName.Text = "";
                                txtPass.Text = "";
                                txtUName.Text = "";
                                txtRePass.Text = "";
                                chkMahdod.Checked = true;
                                ID = -1;
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("خطایی در ثبت رکورد رخ داده است");
                            }
                        }
                    }
                }
            }                  
        }
        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
