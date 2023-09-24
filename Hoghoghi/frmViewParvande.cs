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
    public partial class frmViewParvande : Form
    {
        public frmViewParvande()
        {
            InitializeComponent();
        }
        string path = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        System.Globalization.PersianCalendar dt = new System.Globalization.PersianCalendar();
        clsGetDataSource gds = new clsGetDataSource();
        int parvandeID = -1;
        public int UserID { get; set; }
        public int Sath { get; set; }
        void sath()
        {
            if (Sath == 0)
            {
                DisplayParvande();
            }
            else
            {
                DisplayParvande(dt.GetYear(DateTime.Now));
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
        void DisplayTitleParvande()
        {
            try
            {
                dgvParvande.Columns["UserID"].Visible = false;
                dgvParvande.Columns["Vaziat"].Visible = false;
                dgvParvande.Columns["Years"].Visible = false;
                dgvParvande.Columns["Month"].Visible = false;
                dgvParvande.Columns["NoMakhtome"].Visible = false;
                dgvParvande.Columns["DateMakhtome"].Visible = false;
                dgvParvande.Columns["ParvandeID"].HeaderText = "شماره پرونده";
                dgvParvande.Columns["BankName"].HeaderText = "نام بانک";
                dgvParvande.Columns["BankName"].Width = 100;
                dgvParvande.Columns["Shobe"].HeaderText = "شعبه";
                dgvParvande.Columns["Shobe"].Width = 100;
                dgvParvande.Columns["Name"].HeaderText = "نام مدیون";
                dgvParvande.Columns["Name"].Width = 100;
                dgvParvande.Columns["FatherName"].HeaderText = "نام پدر";
                dgvParvande.Columns["FatherName"].Width = 50;
                dgvParvande.Columns["Melli"].HeaderText = "شماره ملی";
                dgvParvande.Columns["Melli"].Width = 70;
                dgvParvande.Columns["Shoghl"].HeaderText = "شغل";
                dgvParvande.Columns["Shoghl"].Width = 50;
                dgvParvande.Columns["TelSabet"].HeaderText = "تلفن ثابت";
                dgvParvande.Columns["TelSabet"].Width = 80;
                dgvParvande.Columns["TelHamrah"].HeaderText = "شماره همراه";
                dgvParvande.Columns["TelHamrah"].Width = 80;
                dgvParvande.Columns["ShTashilat"].HeaderText = "شماره تسهیلات";
                dgvParvande.Columns["ShTashilat"].Width = 80;
                dgvParvande.Columns["Date"].HeaderText = "تاریخ دریافت ";
                dgvParvande.Columns["Date"].Width = 70;
                dgvParvande.Columns["Bed"].HeaderText = "مبلغ بدهی";
                dgvParvande.Columns["Bed"].Width = 100;
                dgvParvande.Columns["Address"].HeaderText = "آدرس";
                dgvParvande.Columns["Address"].Width = 250;
                dgvParvande.Columns["DateSabt"].HeaderText = "تاریخ ثبت ";
                dgvParvande.Columns["DateSabt"].Width = 70;
                dgvParvande.Columns["Komision"].HeaderText = "کمیسیون";

            }
            catch (Exception)
            {
            }
           
        }
        void DisplayParvande()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adb = new SqlDataAdapter();
                adb.SelectCommand = new SqlCommand();
                adb.SelectCommand.Connection = con;
                adb.SelectCommand.CommandText = "select * from tblParvande where UserID=" + UserID + "and Vaziat=" + 1 + " ORDER BY ParvandeID desc";
                adb.Fill(ds, "tblParvande");
                dgvParvande.DataSource = ds;
                dgvParvande.DataMember = "tblParvande";
                DisplayTitleParvande();
            }
            catch (Exception)
            {

            }
           
        }
        void DisplayParvande(int y )
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adb = new SqlDataAdapter();
                adb.SelectCommand = new SqlCommand();
                adb.SelectCommand.Connection = con;
                adb.SelectCommand.CommandText = "select * from View_UserToParvande where UserID=" + UserID + " and Years=" + y + " and Vaziat=" + 1 + " ORDER BY ParvandeID desc";
                adb.Fill(ds, "View_UserToParvande");
                dgvParvande.DataSource = ds;
                dgvParvande.DataMember = "View_UserToParvande";
                DisplayTitleParvande();
            }
            catch (Exception)
            {
            }
        
        }
        private void LoadfrmPardakht(int outid)
        {
            frmParvandeMain frmPM = new frmParvandeMain();
            frmPM.ParvandeID=parvandeID;          
            frmPM.ShowDialog();
        }
        public void frmViewParvande_Load(object sender, EventArgs e)
        {
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
            DisplayComboNoBank();
            DisplayParvande(dt.GetYear(DateTime.Now));
            txtDate.Text = dt.GetYear(DateTime.Now).ToString();
        }
        private void buttonX4_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from View_UserToParvande where  Name Like '%' + @Name + '%' and BankName Like '%' + @BankName + '%'and Melli Like '%' + @Melli + '%' and  ShTashilat Like '%' + @ShTashilat + '%' and UserID=" + UserID + " and  Years =" + int.Parse(txtDate.Text) + " and Vaziat=" + 1 + "";
                adp.SelectCommand.Parameters.AddWithValue("@Name", txtName.Text + "%");
                adp.SelectCommand.Parameters.AddWithValue("@BankName", cmbBankNo.Text + "%");
                adp.SelectCommand.Parameters.AddWithValue("@Melli", txtMelli.Text + "%");
                adp.SelectCommand.Parameters.AddWithValue("@ShTashilat", txtShTashilat.Text + "%");
                adp.Fill(ds, "View_UserToParvande");
                dgvParvande.DataSource = ds;
                dgvParvande.DataMember = "View_UserToParvande";
                DisplayTitleParvande();
            }

            catch (Exception)
            {
                MessageBox.Show("مشکلی در جستوجو با  کلید جستوجو رخ داده است.");
            }
                 
        }
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from View_UserToParvande where  Name Like '%' + @Name + '%' and UserID=" + UserID + " and  Years =" + int.Parse(txtDate.Text) + " and Vaziat=" + 1 + "";
                adp.SelectCommand.Parameters.AddWithValue("@Name", txtName.Text + "%");
                adp.Fill(ds, "View_UserToParvande");
                dgvParvande.DataSource = ds;
                dgvParvande.DataMember = "View_UserToParvande";
                DisplayTitleParvande();
            }
            catch (Exception)
            {
                MessageBox.Show("مشکلی در جستوجو با فید نام رخ داده است.");
            }
           
        }
        private void txtMelli_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from View_UserToParvande where  Melli Like '%' + @Melli + '%'and UserID=" + UserID + " and  Years =" + int.Parse(txtDate.Text) + " and Vaziat=" + 1 + "";
                adp.SelectCommand.Parameters.AddWithValue("@Melli", txtMelli.Text + "%");
                adp.Fill(ds, "View_UserToParvande");
                dgvParvande.DataSource = ds;
                dgvParvande.DataMember = "View_UserToParvande";
                DisplayTitleParvande();
            }
            catch (Exception)
            {
                MessageBox.Show("مشکلی در جستوجو با فیلد شماره ملی رخ داده است.");
            }
           
        }
        private void cmbBankNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from View_UserToParvande where  BankName Like '%' + @BankName + '%'and UserID=" + UserID + " and  Years =" + int.Parse(txtDate.Text) + " and Vaziat=" + 1 + "";
                adp.SelectCommand.Parameters.AddWithValue("@BankName", cmbBankNo.Text + "%");
                adp.Fill(ds, "View_UserToParvande");
                dgvParvande.DataSource = ds;
                dgvParvande.DataMember = "View_UserToParvande";
                DisplayTitleParvande();
            }
            catch (Exception)
            {
                MessageBox.Show("مشکلی در جستوجو با  نام بانک رخ داده است.");
            }

        }
        private void txtShTashilat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from View_UserToParvande where  ShTashilat Like '%' + @ShTashilat + '%' and UserID=" + UserID + " and  Years =" + int.Parse(txtDate.Text) + "and Vaziat=" + 1 + "";
                adp.SelectCommand.Parameters.AddWithValue("@ShTashilat", txtShTashilat.Text + "%");
                adp.Fill(ds, "View_UserToParvande");
                dgvParvande.DataSource = ds;
                dgvParvande.DataMember = "View_UserToParvande";
                DisplayTitleParvande();
            }
            catch (Exception)
            {
                MessageBox.Show("مشکلی در جستوجو با شماره تسهیلات رخ داده است.");
            }
          
        }
        private void dgvParvande_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
                parvandeID = (int)dgvParvande.Rows[e.RowIndex].Cells["ParvandeID"].Value;
                LoadfrmPardakht(parvandeID);
                dgvParvande.Rows[e.RowIndex].Selected = true;
            //}
            //catch (Exception)
            //{
            //}

        }
        private void dgvParvande_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgvParvande.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception)
            {
            }
          
        }
        private void dgvParvande_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 11 && e.RowIndex != this.dgvParvande.NewRowIndex)
            {
                double d = double.Parse(e.Value.ToString());
                e.Value = d.ToString("#,##0.##");
            }
            if (e.ColumnIndex == 16 && e.RowIndex != this.dgvParvande.NewRowIndex)
            {
                double d = double.Parse(e.Value.ToString());
                e.Value = d.ToString("#,##0.##");
            }
        }

        public void btnRefresh_Click(object sender, EventArgs e)
        {
            sath();
        }
    }
}
