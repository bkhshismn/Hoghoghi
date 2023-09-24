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
    public partial class frmGharardadBank : Form
    {
        public frmGharardadBank()
        {
            InitializeComponent();
        }
        string path = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        System.Globalization.PersianCalendar dt = new System.Globalization.PersianCalendar();
        clsGetDataSource gds = new clsGetDataSource();
        int ID = -1;
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
        void Display()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adb = new SqlDataAdapter();
                adb.SelectCommand = new SqlCommand();
                adb.SelectCommand.Connection = con;
                adb.SelectCommand.CommandText = "select * from tblGhardad where BankName=N'" + cmbBankNo.Text + "'";
                adb.Fill(ds, "tblGhardad");
                dgvBank.DataSource = ds;
                dgvBank.DataMember = "tblGhardad";
                dgvBank.Columns["GharardadID"].HeaderText = "کد";
                dgvBank.Columns["GharardadID"].Width = 50;
                dgvBank.Columns["BankName"].HeaderText = " نام بانک";
                dgvBank.Columns["BankName"].Width = 150;
                dgvBank.Columns["AzM"].HeaderText = "از مبلغ";
                dgvBank.Columns["AzM"].Width = 150;
                dgvBank.Columns["TaM"].HeaderText = "تا مبلغ";
                dgvBank.Columns["TaM"].Width = 150;
                dgvBank.Columns["AzDate"].HeaderText = "از ماه";
                dgvBank.Columns["AzDate"].Width = 50;
                dgvBank.Columns["TaDate"].HeaderText = "تا ماه";
                dgvBank.Columns["TaDate"].Width = 50;
                dgvBank.Columns["Darsad"].HeaderText = "درصد";
                dgvBank.Columns["Darsad"].Width = 50;
            }
            catch (Exception)
            {

                MessageBox.Show("خطایی در نمایش رخ داده است");
            }
           
        }
        private void frmGharardadBank_Load(object sender, EventArgs e)
        {
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
            DisplayComboNoBank();
        }
        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbBankNo.Text == "" || txtAzM.Text == "" || txtTaM.Text == "" || txtAzDate.Text == "" || txtTaDate.Text == "" || txtDarsad.Text == "")
            {
                MessageBox.Show("لطفا فیلد های ستاره دار را پر کنید");
            }
            else
            {
                try
                {
                    cmd.Parameters.Clear();
                    cmd.Connection = con;
                    cmd.CommandText = "insert into tblGhardad(BankName,AzM,TaM,AzDate,TaDate,Darsad)values(@BankName,@AzM,@TaM,@AzDate,@TaDate,@Darsad)";
                    cmd.Parameters.AddWithValue("@BankName", cmbBankNo.Text);
                    cmd.Parameters.AddWithValue("@AzM", Convert.ToInt64(txtAzM.Text.Replace(",", "")));
                    cmd.Parameters.AddWithValue("@TaM", Convert.ToInt64(txtTaM.Text.Replace(",", "")));
                    cmd.Parameters.AddWithValue("@AzDate", Convert.ToInt32(txtAzDate.Text.Replace(",", "")));
                    cmd.Parameters.AddWithValue("@TaDate", Convert.ToInt32(txtTaDate.Text.Replace(",", "")));
                    cmd.Parameters.AddWithValue("@Darsad", Convert.ToDouble(txtDarsad.Text.Replace(",", "")));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("ثبت  با موفقیت انجام شد");
                    Display();
                    txtAzDate.Text = "";
                    txtAzM.Text = "";
                    txtDarsad.Text = "";
                    txtTaDate.Text = "";
                    txtTaM.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("مشکلی در ثبت اطلاعات وجود دارد!");
                }
            }
        }
        private void cmbBankNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Display();
        }
        private void dgvBank_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ID = (int)dgvBank.Rows[e.RowIndex].Cells["GharardadID"].Value;
                cmd.Parameters.Clear();
                DataTable dt = new DataTable();
                SqlDataAdapter sdp = new SqlDataAdapter();
                sdp.SelectCommand = new SqlCommand();
                sdp.SelectCommand.Connection = con;
                sdp.SelectCommand.CommandText = "select * from tblGhardad where GharardadID=" + ID;
                con.Open();
                sdp.Fill(dt);
                con.Close();
                this.txtDarsad.Text = dt.Rows[0]["Darsad"].ToString();
                this.cmbBankNo.Text = dt.Rows[0]["BankName"].ToString();
                this.txtAzM.Text = dt.Rows[0]["AzM"].ToString();
                this.txtTaM.Text = dt.Rows[0]["TaM"].ToString();
                this.txtAzDate.Text = dt.Rows[0]["AzDate"].ToString();
                this.txtTaDate.Text = dt.Rows[0]["TaDate"].ToString();
                ID = (int)dt.Rows[0]["GharardadID"];

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
                    if (cmbBankNo.Text == "" || txtAzM.Text == "" || txtTaM.Text == "" || txtAzDate.Text == "" || txtTaDate.Text == "" || txtDarsad.Text == "")
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
                            cmd.CommandText = "update tblGhardad set BankName=N'" + cmbBankNo.Text +
                                "', Darsad='" + Convert.ToDouble(txtDarsad.Text) +
                                "', AzM='" + Convert.ToDouble(txtAzM.Text) +
                                "' , TaM='" + Convert.ToDouble(txtTaM.Text) +
                                "' , AzDate='" + Convert.ToDouble(txtAzDate.Text) +
                                "' , TaDate='" + Convert.ToDouble(txtTaDate.Text) + "'  where GharardadID=" + ID;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            MessageBox.Show("ویرایش با موفقیت انجام شد");
                            Display();
                            ID = -1;
                            txtAzDate.Text = "";
                            txtAzM.Text = "";
                            txtDarsad.Text = "";
                            txtTaDate.Text = "";
                            txtTaM.Text = "";
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
            var result = MessageBox.Show("آیا مایل به حذف رکورد هستید؟", "هشدار", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (ID != -1)
                {
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.Connection = con;
                        cmd.CommandText = "delete from tblGhardad where GharardadID=" + ID;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        Display();
                        MessageBox.Show("حذف اطلاعات انجام شد.");
                        txtAzDate.Text = "";
                        txtAzM.Text = "";
                        txtDarsad.Text = "";
                        txtTaDate.Text = "";
                        txtTaM.Text = "";
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
        private void txtAzM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtAzM.Text != string.Empty)
                {
                    txtAzM.Text = string.Format("{0:N0}", double.Parse(txtAzM.Text.Replace(",", "")));
                    txtAzM.Select(txtAzM.TextLength, 0);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("خطایی در درج اطلاعات رخ داده است.");
            }
        }
        private void txtTaM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTaM.Text != string.Empty)
                {
                    txtTaM.Text = string.Format("{0:N0}", double.Parse(txtTaM.Text.Replace(",", "")));
                    txtTaM.Select(txtTaM.TextLength, 0);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("خطایی در درج اطلاعات رخ داده است.");
            }
        }
    }
}
