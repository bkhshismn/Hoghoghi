using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO.MemoryMappedFiles;
using System.IO;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Hoghoghi
{
    public partial class frmHazine : Form
    {
        public frmHazine()
        {
            InitializeComponent();
        }
        string path = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        System.Globalization.PersianCalendar dt = new System.Globalization.PersianCalendar();
        clsGetDataSource gds = new clsGetDataSource();
        int Hazineid = -1;
        void DisplayHazine()
        {
            int years = dt.GetYear(DateTime.Now);
            int month = dt.GetMonth(DateTime.Now);
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter();
                DataSet ds = new DataSet();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from [tblHazine] where Years=" + years + " and Month=" + month;
                adp.Fill(ds, "tblHazine");
                dgvHazine.DataSource = ds;
                dgvHazine.DataMember = "tblHazine";
                dgvHazine.Columns["Years"].Visible = false;
                dgvHazine.Columns["Month"].Visible = false;
                dgvHazine.Columns["HazineID"].HeaderText = "کد";
                dgvHazine.Columns["HazineID"].Width = 70;
                dgvHazine.Columns["Mablagh"].HeaderText = " مبلغ";
                dgvHazine.Columns["Mablagh"].Width = 70;
                dgvHazine.Columns["Sharh"].HeaderText = "شرح هزینه";
                dgvHazine.Columns["Sharh"].Width = 300;
                dgvHazine.Columns["Date"].HeaderText = "تاریخ هزینه";
                dgvHazine.Columns["Date"].Width = 70;
                dgvHazine.Columns["Discription"].HeaderText = "توضیحات";
                dgvHazine.Columns["Discription"].Width = 300;
            }
            catch (Exception)
            {

                MessageBox.Show("مشکلی در نمایش اطلاعات رخ داده است");
            }
        }
        void DisplayHazine(int years, int month)
        {
            //int years = dt.GetYear(DateTime.Now);
            //int month = dt.GetMonth(DateTime.Now);
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter();
                DataSet ds = new DataSet();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from [tblHazine] where Years=" + years + " and Month=" + month;
                adp.Fill(ds, "tblHazine");
                dgvHazine.DataSource = ds;
                dgvHazine.DataMember = "tblHazine";
                dgvHazine.Columns["Years"].Visible = false;
                dgvHazine.Columns["Month"].Visible = false;
                dgvHazine.Columns["HazineID"].HeaderText = "کد";
                dgvHazine.Columns["HazineID"].Width = 70;
                dgvHazine.Columns["Mablagh"].HeaderText = " مبلغ";
                dgvHazine.Columns["Mablagh"].Width = 70;
                dgvHazine.Columns["Sharh"].HeaderText = "شرح هزینه";
                dgvHazine.Columns["Sharh"].Width = 300;
                dgvHazine.Columns["Date"].HeaderText = "تاریخ هزینه";
                dgvHazine.Columns["Date"].Width = 70;
                dgvHazine.Columns["Discription"].HeaderText = "توضیحات";
                dgvHazine.Columns["Discription"].Width = 300;
            }
            catch (Exception)
            {

                MessageBox.Show("مشکلی در نمایش اطلاعات رخ داده است");
            }
        }
        void Search(string text,int years, int month)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from tblHazine where Sharh Like '%' + @s + '%' and Years=" + years + " and Month=" + month;
            adp.SelectCommand.Parameters.AddWithValue("@s", text + "%");
            adp.Fill(ds, "tblHazine");
            dgvHazine.DataSource = ds;
            dgvHazine.DataMember = "tblHazine";
            dgvHazine.Columns["Years"].Visible = false;
            dgvHazine.Columns["Month"].Visible = false;
            dgvHazine.Columns["HazineID"].HeaderText = "کد";
            dgvHazine.Columns["HazineID"].Width = 70;
            dgvHazine.Columns["Mablagh"].HeaderText = " مبلغ";
            dgvHazine.Columns["Mablagh"].Width = 70;
            dgvHazine.Columns["Sharh"].HeaderText = "شرح هزینه";
            dgvHazine.Columns["Sharh"].Width = 300;
            dgvHazine.Columns["Date"].HeaderText = "تاریخ هزینه";
            dgvHazine.Columns["Date"].Width = 70;
            dgvHazine.Columns["Discription"].HeaderText = "توضیحات";
            dgvHazine.Columns["Discription"].Width = 300;

        }
        private void btnPygiriSave_Click(object sender, EventArgs e)
        {
            string s = txtHazineDate.Text;
            string[] y = s.Split('/');
            int years = Convert.ToInt16(y[0]);
            int month = Convert.ToInt16(y[1]);
            if (txtMablagh.Text == "" && txtSharh.Text=="")
            {
                MessageBox.Show(".لطفا فیلد شرح و مبلغ را پر کنید");
            }
            else
            {
                try
                {
                    con.Close();
                    cmd.Parameters.Clear();
                    cmd.Connection = con;
                    cmd.CommandText = "INSERT into [tblHazine](Mablagh,Sharh,Date,Discription,Years,Month)values(@Mablagh,@Sharh,@Date,@Discription,@Years,@Month)";
                    cmd.Parameters.AddWithValue("@Mablagh", Convert.ToInt64(txtMablagh.Text.Replace(",", "")));
                    cmd.Parameters.AddWithValue("@Sharh", txtSharh.Text);
                    cmd.Parameters.AddWithValue("@Date", txtHazineDate.Text);
                    cmd.Parameters.AddWithValue("@Discription", txtCostDis.Text);
                    cmd.Parameters.AddWithValue("@Years", years);
                    cmd.Parameters.AddWithValue("@Month", month);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    DisplayHazine();
                    MessageBox.Show("ثبت با موفقیت انجام شد");
                    txtMablagh.Text = "0";
                    txtSharh.Text = "";
                    txtCostDis.Text = "";

                }
                catch (Exception)
                {
                    MessageBox.Show("مشکلی در ثبت پرداخت نقدی وجود دارد");
                }
                txtSharh.Focus();
            }
        }
        private void frmHazine_Load(object sender, EventArgs e)
        {
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
            txtHazineDate.Text = dt.GetYear(DateTime.Now).ToString() + dt.GetMonth(DateTime.Now).ToString("0#") + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
            txtYears.Text = dt.GetYear(DateTime.Now).ToString();
            cmbNoPardakht.SelectedIndex = (int)dt.GetMonth(DateTime.Now)-1;
            DisplayHazine();
        }
        private void txtMablagh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMablagh.Text != string.Empty || txtMablagh.Text != "0")
                {
                    txtMablagh.Text = string.Format("{0:N0}", double.Parse(txtMablagh.Text.Replace(",", "")));
                    txtMablagh.Select(txtMablagh.TextLength, 0);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("خطایی در درج اطلاعات مبلغ رخ داده است.");
            }
        }
        private void btnPygiriEdit_Click(object sender, EventArgs e)
        {
            string s = txtHazineDate.Text;
            string[] y = s.Split('/');
            int years = Convert.ToInt16(y[0]);
            int month = Convert.ToInt16(y[1]);
            var result = MessageBox.Show("آیا مایل به ویرایش رکورد هستید؟", "هشدار", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    cmd.Parameters.Clear();
                    cmd.Connection = con;
                    cmd.CommandText = "Update tblHazine Set Mablagh=N'" + Convert.ToInt32(txtMablagh.Text.Replace(",", "")) +
                        "',Sharh=N'" + txtSharh.Text +
                        "',Date=N'" + txtHazineDate.Text +
                        "',Discription=N'" + txtCostDis.Text +
                         "',Years=N'" + years +
                          "',Month=N'" + month +
                        "' where HazineID=" + Hazineid;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    DisplayHazine();
                    MessageBox.Show("ویرایش اطلاعات انجام شد.");
                    Hazineid = -1;
                    txtMablagh.Text = "0";
                    txtSharh.Text = "";
                    txtCostDis.Text = "";
                }
                catch (Exception)
                {

                    MessageBox.Show("خطایی در ویرایش اطلاعات رخ داده است.");
                }
            }
        }
        private void dgvHazine_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
                try
                {
                    Hazineid = (int)dgvHazine.Rows[e.RowIndex].Cells[0].Value;
                    cmd.Parameters.Clear();
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.SelectCommand = new SqlCommand();
                    adp.SelectCommand.Connection = con;
                    adp.SelectCommand.CommandText = "select * from tblHazine where HazineID=" + Hazineid;
                    con.Open();
                    adp.Fill(dt);
                    this.txtMablagh.Text = dt.Rows[0]["Mablagh"].ToString();
                    this.txtSharh.Text = dt.Rows[0]["Sharh"].ToString();
                    this.txtHazineDate.Text = dt.Rows[0]["Date"].ToString();
                    this.txtCostDis.Text = dt.Rows[0]["Discription"].ToString();
                    con.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("لطفا روی رکورد سال مورد نظر کلیک کنید");
                }
        }
        private void btnPygiriDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("آیا مایل به حذف رکورد هستید؟", "هشدار", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (Hazineid != -1)
                {
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.Connection = con;
                        cmd.CommandText = "delete from tblHazine where HazineID=" + Hazineid;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DisplayHazine();
                        MessageBox.Show("حذف اطلاعات انجام شد.");
                        txtMablagh.Text = "0";
                        txtSharh.Text = "";
                        txtCostDis.Text = "";
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("خطایی در حذف اطلاعات رخ داده است.");
                    }
                }
                else { MessageBox.Show("لطفا روی رکورد سال مورد نظر کلیک کنید"); }
            }
        }
        private void buttonX22_Click(object sender, EventArgs e)
        {
            txtMablagh.Text = "0";
            txtSharh.Text = "";
            txtCostDis.Text = "";
            Hazineid = -1;

        }
        private void cmbNoPardakht_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayHazine(int.Parse(txtYears.Text), cmbNoPardakht.SelectedIndex + 1);
        }
        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            Search(txtSearch.Text, int.Parse(txtYears.Text), cmbNoPardakht.SelectedIndex + 1);
        }

        private void dgvHazine_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex ==2 && e.RowIndex != this.dgvHazine.NewRowIndex)
            {
                double d = double.Parse(e.Value.ToString());
                e.Value = d.ToString("#,##0.##");
            }
        }

        private void btnPygiriPrint_Click(object sender, EventArgs e)
        {
            //try
            //{
            frmReport frmReportInput = new frmReport();
            SqlDataAdapter da = new SqlDataAdapter("select * from tblHazine ", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "tblHazine");
            dgvHazine.DataSource = ds.Tables["tblHazine"].DefaultView;
            ReporHazine rptInput = new ReporHazine();
            rptInput.SetDataSource(ds);
            frmReportInput.crystalReportViewer1.ReportSource = rptInput;
            frmReportInput.ShowDialog();
            //}
            //catch (Exception)
            //{


        }
    }
}
