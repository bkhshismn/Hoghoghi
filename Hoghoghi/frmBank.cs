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
    public partial class frmBank : Form
    {
        public frmBank()
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
            adb.SelectCommand.CommandText = "select * from tblBank";
            adb.Fill(ds, "tblBank");
            dgvBank.DataSource = ds;
            dgvBank.DataMember = "tblBank";
            dgvBank.Columns["BankID"].HeaderText = "کد";
            dgvBank.Columns["BankID"].Width = 50;
            dgvBank.Columns["BankName"].HeaderText = " نام بانک";
            dgvBank.Columns["BankName"].Width = 150;
            dgvBank.Columns["BankCoding"].HeaderText = "کدینگ";
            dgvBank.Columns["BankCoding"].Width = 50;
            dgvBank.Columns["Darsad"].Visible = false;
        }
        private void frmBank_Load(object sender, EventArgs e)
        {
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
            Display();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text=="" || txtCoding.Text=="")
            {
                MessageBox.Show("فیلد های خالی را پر کنید...");
            }
            else
            {
                try
                {
                    cmd.Parameters.Clear();
                    cmd.Connection = con;
                    cmd.CommandText = "insert into tblBank(BankName,BankCoding) values(@BankName , @BankCoding)";
                    cmd.Parameters.AddWithValue("@BankName", txtName.Text);
                    cmd.Parameters.AddWithValue("@BankCoding", Convert.ToInt32(txtCoding.Text));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Display();
                    MessageBox.Show("ثبت با موفقیت انجام شد");
                    txtCoding.Text = "";
                    txtName.Text = "";
                  
                }
                catch (Exception)
                {
                    MessageBox.Show("مشکلی در ثبت اطلاعات وجود دارد!");
                }
            }
        }
        private void dgvBank_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                ID = (int)dgvBank.Rows[e.RowIndex].Cells["BankID"].Value;
                cmd.Parameters.Clear();
                DataTable dt = new DataTable();
                SqlDataAdapter sdp = new SqlDataAdapter();
                sdp.SelectCommand = new SqlCommand();
                sdp.SelectCommand.Connection = con;
                sdp.SelectCommand.CommandText = "select * from tblBank where BankID=" + ID;
                con.Open();
                sdp.Fill(dt);
                con.Close();
                this.txtCoding.Text = dt.Rows[0]["BankCoding"].ToString();
                this.txtName.Text = dt.Rows[0]["BankName"].ToString();

            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در انتخاب رکورد رخ داده است");
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ID == -1)
                {
                    MessageBox.Show("لطفا روی یک رکورد کلیک کنید");
                }
                else
                {
                    if (txtName.Text == "" || txtCoding.Text == "")
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
                            cmd.CommandText = "update tblBank set BankName=N'" + txtName.Text + "', BankCoding='" + Convert.ToInt32(txtCoding.Text)
                                                              + "'where BankID=" + ID;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            Display();
                            MessageBox.Show("ویرایش با موفقیت انجام شد");
                            ID = -1;
                            txtCoding.Text = "";
                            txtName.Text = "";
                        }
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در ویرایش رکورد رخ داده است");
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("؟ایا مایل به حذف بانک هستید", "هشدار", MessageBoxButtons.YesNo);
            if (result==DialogResult.Yes)
            {
                if (ID != -1)
                {
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.Connection = con;
                        cmd.CommandText = "delete from tblBank where BankID=" + ID;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        Display();
                        MessageBox.Show("حذف اطلاعات انجام شد.");
                        txtName.Text = "";
                        txtCoding.Text = "";
                        ID = -1;
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("خطایی در حذف اطلاعات رخ داده است.");
                    }
                }
                else { MessageBox.Show("لطفا روی رکورد مورد نظر کلیک کنید"); }
            }
        }
    }
}
