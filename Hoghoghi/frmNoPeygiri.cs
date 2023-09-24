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
    public partial class frmNoPeygiri : Form
    {
        public frmNoPeygiri()
        {
            InitializeComponent();
        }
        string path = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        clsGetDataSource gds = new clsGetDataSource();

        int ID = -1;
        void Display()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adb = new SqlDataAdapter();
            adb.SelectCommand = new SqlCommand();
            adb.SelectCommand.Connection = con;
            adb.SelectCommand.CommandText = "select * from tblNoPeygiri";
            adb.Fill(ds, "tblNoPeygiri");
            dgvBank.DataSource = ds;
            dgvBank.DataMember = "tblNoPeygiri";
            dgvBank.Columns["NoPeygiriID"].HeaderText = "کد";
            dgvBank.Columns["NoPeygiriID"].Width = 50;
            dgvBank.Columns["NoPeygiri"].HeaderText = " عنوان";
            dgvBank.Columns["NoPeygiri"].Width = 400;



        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" )
            {
                MessageBox.Show("فیلد های خالی را پر کنید...");
            }
            else
            {
                try
                {
                    cmd.Parameters.Clear();
                    cmd.Connection = con;
                    cmd.CommandText = "insert into tblNoPeygiri(NoPeygiri) values(@BankName )";
                    cmd.Parameters.AddWithValue("@BankName", txtName.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Display();
                    MessageBox.Show("ثبت با موفقیت انجام شد");
                    txtName.Text = "";

                }
                catch (Exception)
                {
                    MessageBox.Show("مشکلی در ثبت اطلاعات وجود دارد!");
                }
            }
        }
        private void frmNoPeygiri_Load(object sender, EventArgs e)
        {
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
            Display();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (ID == -1)
            {
                MessageBox.Show("لطفا روی یک رکورد کلیک کنید");
            }
            else
            {
                if (txtName.Text == "" )
                {
                    MessageBox.Show("فیلد های خالی را پر کنید...");
                }
                else
                {
                    var result = MessageBox.Show("آیا مایل به ویرایش رکورد هستید؟", "هشدار", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        cmd.Parameters.Clear();
                        cmd.Connection = con;
                        cmd.CommandText = "update tblNoPeygiri set NoPeygiri=N'" + txtName.Text + "' where NoPeygiriID=" + ID;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        Display();
                        MessageBox.Show("ویرایش با موفقیت انجام شد");
                        ID = -1;
                        txtName.Text = "";

                    }
                }
            }
        }
        private void dgvBank_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ID = (int)dgvBank.Rows[e.RowIndex].Cells["NoPeygiriID"].Value;
                cmd.Parameters.Clear();
                DataTable dt = new DataTable();
                SqlDataAdapter sdp = new SqlDataAdapter();
                sdp.SelectCommand = new SqlCommand();
                sdp.SelectCommand.Connection = con;
                sdp.SelectCommand.CommandText = "select * from tblNoPeygiri where NoPeygiriID=" + ID;
                con.Open();
                sdp.Fill(dt);
                con.Close();
                this.txtName.Text = dt.Rows[0]["NoPeygiri"].ToString();

            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در انتخاب رکورد رخ داده است");
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("آیا مایل به حذف رکورد هستتید؟", "هشدار", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {                   
                    cmd.Parameters.Clear();
                    cmd.Connection = con;
                    cmd.CommandText = "Delete from tblNoPeygiri where NoPeygiriID=@n";
                    cmd.Parameters.AddWithValue("@n", ID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Display();
                    MessageBox.Show("عملیات حذف با موفقیت انجام شد.");
                    txtName.Text = "";
                }
                catch (Exception)
                {

                    MessageBox.Show("مشکلی در حذف کاربر رخ داده است.");
                }
            }
        }
    }
}
