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
    public partial class frmKarmozd : Form
    {
        public frmKarmozd()
        {
            InitializeComponent();
        }
        string path = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        System.Globalization.PersianCalendar dt = new System.Globalization.PersianCalendar();
        clsGetDataSource gds = new clsGetDataSource();
        int ID = -1;
        public void DisplayKarmozd()
        {
            //try
            //{
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = new SqlCommand();
                sda.SelectCommand.Connection = con;
                sda.SelectCommand.CommandText = "select * from tblKarmozd where Year= "+dt.GetYear(DateTime.Now) +"order by Saghf";
                sda.Fill(ds, "tblKarmozd");
                dgvBank.DataSource = ds;
                dgvBank.DataMember = "tblKarmozd";
                dgvBank.Columns["KarmozdID"].Visible = false;
                dgvBank.Columns["Saghf"].Width = 250;
               dgvBank.Columns["Year"].Visible = false;
                dgvBank.Columns["Darsad"].HeaderText = "درصد";
                dgvBank.Columns["Saghf"].HeaderText = "سقف";
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("خطایی در نمایش اطلاعات قرارداد رخ داده است");
            //}
        }
        private void frmKarmozd_Load(object sender, EventArgs e)
        {
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
            DisplayKarmozd();
            txtYears.Text = dt.GetYear(DateTime.Now).ToString() ;
        }
        private void btnPygiriEdit_Click(object sender, EventArgs e)
        {
            
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDarsad.Text != "" && txtTa.Text!="")
            {
                try
                {
                    cmd.Parameters.Clear();
                    cmd.Connection = con;
                    cmd.CommandText = "insert into tblKarmozd(Saghf,Darsad,Year) values(@Saghf,@Darsad,@Year)";
                    cmd.Parameters.AddWithValue("@Saghf", float.Parse(txtTa.Text.Replace(",", "")));
                    cmd.Parameters.AddWithValue("@Darsad", float.Parse(txtDarsad.Text.Replace(",", "")));
                    cmd.Parameters.AddWithValue("@Year", int.Parse(txtYears.Text));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    DisplayKarmozd();
                    MessageBox.Show("ثبت با موفقیت انجام شد");
                    txtTa.Text = "";
                    txtDarsad.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("خطایی در ثبت رخ داده است");
                }
            }
            else
            {
                MessageBox.Show("لطفا فیلد های خالی را پر کنید");
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtTa.Text == "" || txtDarsad.Text == "" )
            {
                MessageBox.Show("فیلد های خالی را پر کنید...");
            }
            else
            {
                try
                {
                    var result = MessageBox.Show("آیا مایل به ویرایش رکورد هستید؟", "هشدار", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        cmd.Parameters.Clear();
                        cmd.Connection = con;
                        cmd.CommandText = "update tblKarmozd set  Saghf=N'" + Convert.ToDouble(txtTa.Text.Replace(",", "")) + "', Darsad=N'" + Convert.ToDouble(txtDarsad.Text) + "' where KarmozdID=" + ID;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DisplayKarmozd();
                        ID = -1;
                        MessageBox.Show("ویرایش با موفقیت انجام شد");
                        txtTa.Text = "";
                        txtDarsad.Text = "";

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("خطا در ویرایش اطلاعات رخ داده است");

                }

            }
        }
        private void txtTa_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTa.Text != string.Empty)
                {
                    txtTa.Text = string.Format("{0:N0}", double.Parse(txtTa.Text.Replace(",", "")));
                    txtTa.Select(txtTa.TextLength, 0);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("خطایی در درج اطلاعات رخ داده است.");
            }
        }
        private void dgvBank_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ID = (int)dgvBank.Rows[e.RowIndex].Cells["KarmozdID"].Value;
                cmd.Parameters.Clear();
                DataTable dt = new DataTable();
                SqlDataAdapter sdp = new SqlDataAdapter();
                sdp.SelectCommand = new SqlCommand();
                sdp.SelectCommand.Connection = con;
                sdp.SelectCommand.CommandText = "select * from tblKarmozd where KarmozdID=" + ID;
                con.Open();
                sdp.Fill(dt);
                con.Close();
                this.txtDarsad.Text = dt.Rows[0]["Darsad"].ToString();
                this.txtTa.Text = dt.Rows[0]["Saghf"].ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در انتخاب رکورد رخ داده است");
            }
        }
        private void dgvBank_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvBank.Columns["Saghf"].Index && e.RowIndex != this.dgvBank.NewRowIndex)
            {
                double d = double.Parse(e.Value.ToString());
                e.Value = d.ToString("#,##0.##");
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
                        cmd.CommandText = "delete from tblKarmozd where KarmozdID=" + ID;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DisplayKarmozd();
                        MessageBox.Show("حذف اطلاعات انجام شد.");
                        txtTa.Text = "";
                        txtDarsad.Text = "";
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

        private void txtYears_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = new SqlCommand();
                sda.SelectCommand.Connection = con;
                sda.SelectCommand.CommandText = "select * from tblKarmozd where Year= " + int.Parse(txtYears.Text) + "order by Saghf";
                sda.Fill(ds, "tblKarmozd");
                dgvBank.DataSource = ds;
                dgvBank.DataMember = "tblKarmozd";
                dgvBank.Columns["KarmozdID"].Visible = false;
                dgvBank.Columns["Saghf"].Width = 250;
                dgvBank.Columns["Year"].Visible = false;
                dgvBank.Columns["Darsad"].HeaderText = "درصد";
                dgvBank.Columns["Saghf"].HeaderText = "سقف";
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش اطلاعات قرارداد رخ داده است");
            }
        }
    }
}
