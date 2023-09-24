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
    public partial class frmShobe : Form
    {
        public frmShobe()
        {
            InitializeComponent();
        }
        string path = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        clsGetDataSource gds = new clsGetDataSource();
        int ID = -1;
        int bankID = -1;
        void Display()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adb = new SqlDataAdapter();
                adb.SelectCommand = new SqlCommand();
                adb.SelectCommand.Connection = con;
                adb.SelectCommand.CommandText = "select * from View_BankToShobe";
                adb.Fill(ds, "tblShobe");
                dgvBank.DataSource = ds;
                dgvBank.DataMember = "tblShobe";
                dgvBank.Columns["ShobeID"].HeaderText = "کد";
                dgvBank.Columns["ShobeID"].Width = 50;
                dgvBank.Columns["BankName"].HeaderText = " نام بانک";
                dgvBank.Columns["BankName"].Width = 150;
                dgvBank.Columns["ShobeName"].HeaderText = "شعبه";
                dgvBank.Columns["ShobeName"].Width = 150;
                dgvBank.Columns["ShobeCoding"].HeaderText = "کدینگ";
            }
            catch (Exception)
            {

                MessageBox.Show("خطایی در نمایش  رخ داده است");
            }
           



        }
        void Display(int nobank)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adb = new SqlDataAdapter();
                adb.SelectCommand = new SqlCommand();
                adb.SelectCommand.Connection = con;
                adb.SelectCommand.CommandText = "select * from View_BankToShobe where BankID=" + nobank;
                adb.Fill(ds, "tblShobe");
                dgvBank.DataSource = ds;
                dgvBank.DataMember = "tblShobe";
                dgvBank.Columns["ShobeID"].HeaderText = "کد";
                dgvBank.Columns["ShobeID"].Width = 50;
                dgvBank.Columns["BankName"].HeaderText = " نام بانک";
                dgvBank.Columns["BankName"].Width = 150;
                dgvBank.Columns["ShobeName"].HeaderText = "شعبه";
                dgvBank.Columns["ShobeName"].Width = 150;
                dgvBank.Columns["ShobeName"].Width = 150;
                dgvBank.Columns["ShobeCoding"].HeaderText = "کدینگ";
                dgvBank.Columns["BankID"].Visible = false;
                dgvBank.Columns["ShobeID"].Visible = false;
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش  رخ داده است");
            }
        }
        public void DisplayComboNoBank()
        {
            try
            {
                con.Close();
                string query = "SELECT  BankName FROM [tblBank]";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
                cmbBankNo.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmbBankNo.Items.Add(dt.Rows[i]["BankName"]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش نام بانک رخ داده است");
            }
        }
        public int GetNoBankID(string bankName)
        {
            int id = -1;
            try
            {
                con.Close();
                string query = "SELECT  BankID FROM [tblBank]n where BankName=N'" + bankName + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
                id = (int)dt.Rows[0]["BankID"];
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش نام بانک رخ داده است");
            }
            return id;
        }
        private void frmShobe_Load(object sender, EventArgs e)
        {
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
            DisplayComboNoBank();
        }
        private void cmbBankNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            bankID = GetNoBankID(cmbBankNo.Text);
            Display(bankID);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbBankNo.Text == "" || txtShobeName.Text == "")
            {
                MessageBox.Show("فیلد های خالی را پر کنید...");
            }
            else
            {
                try
                {
                    cmd.Parameters.Clear();
                    cmd.Connection = con;
                    cmd.CommandText = "insert into tblShobe(BankID,ShobeName,ShobeCoding) values(@BankID , @ShobeName,@ShobeCoding)";
                    cmd.Parameters.AddWithValue("@BankID", bankID);
                    cmd.Parameters.AddWithValue("@ShobeName", "شعبه "+txtShobeName.Text);
                    cmd.Parameters.AddWithValue("@ShobeCoding", Convert.ToInt32(txtCoding.Text));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Display(bankID);
                    MessageBox.Show("ثبت با موفقیت انجام شد");
                    txtShobeName.Text = "";
                    txtCoding.Text = "";
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
                ID = (int)dgvBank.Rows[e.RowIndex].Cells["ShobeID"].Value;
                cmd.Parameters.Clear();
                DataTable dt = new DataTable();
                SqlDataAdapter sdp = new SqlDataAdapter();
                sdp.SelectCommand = new SqlCommand();
                sdp.SelectCommand.Connection = con;
                sdp.SelectCommand.CommandText = "select * from View_BankToShobe where ShobeID=" + ID;
                con.Open();
                sdp.Fill(dt);
                con.Close();

                this.cmbBankNo.SelectedIndex = GetNoBankID(dt.Rows[0]["BankName"].ToString()) - 1;
                this.txtShobeName.Text = dt.Rows[0]["ShobeName"].ToString();
                this.txtCoding.Text = dt.Rows[0]["ShobeCoding"].ToString();

            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در انتخاب رکورد رخ داده است");
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (ID == -1)
            {
                MessageBox.Show("لطفا روی یک رکورد کلیک کنید");
            }
            else
            {
                if (txtShobeName.Text == "" || cmbBankNo.Text == "")
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
                        cmd.CommandText = "update tblShobe set ShobeName=N'" + txtShobeName.Text + "', BankID=N'" + GetNoBankID(cmbBankNo.Text) + "', ShobeCoding='" + Convert.ToInt32(txtCoding.Text)+ "' where ShobeID=" + ID;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        Display(bankID);
                        MessageBox.Show("ویرایش با موفقیت انجام شد");
                        ID = -1;
                        txtShobeName.Text = "";
                        txtCoding.Text = "";
                    }
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("آیا مایل به حذف رکورد هستید؟", "هشدار", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (ID != -1)
                {
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.Connection = con;
                        cmd.CommandText = "delete from tblShobe where ShobeID=" + ID;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        Display();
                        MessageBox.Show("حذف اطلاعات انجام شد.");
                        txtShobeName.Text = "";
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
