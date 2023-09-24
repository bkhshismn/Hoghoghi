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
    public partial class frmVarizi : Form
    {
        public frmVarizi()
        {
            InitializeComponent();
        }
        string path = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        System.Globalization.PersianCalendar dt = new System.Globalization.PersianCalendar();
        clsGetDataSource gds = new clsGetDataSource();
        int bankID = -1;
        bool load = false;
        bool BankNo = false;
        bool shobe = false;
        void ADDTodmnYears()
        {
            int years = dt.GetYear(DateTime.Now);
            for (int i = years; i > years - 5; i--)
            {
                dmnYears.Items.Add(i);
            }
            dmnYears.SelectedIndex = 0;
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
                cmbBankNo.SelectedIndex = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش نام بانک رخ داده است");
            }
        }
        void DisplayShobe(int nobank)
        {
            cmdShobe.Text = "";
            try
            {
                con.Close();
                string query = "SELECT  ShobeName FROM [tblShobe] where BankID=" + nobank;
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
                cmdShobe.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmdShobe.Items.Add(dt.Rows[i]["ShobeName"]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش نام شعبه رخ داده است");
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
        Int64 GetBedehkari()
        {
            Int64 bed = 0;
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter();
                DataTable dt = new DataTable();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from tblParvande where ParvandeID=";
                adp.Fill(dt);
                int cunt = dt.Rows.Count;
                bed = Convert.ToInt64(dt.Rows[0]["Bed"]);
            }
            catch (Exception)
            {
                MessageBox.Show("مشکلی در دریافت اطلاعات بدهکاری رخ داده است");
            }

            return bed;
        }
        void DisplayPardakht(int years)
        {
            Int64 pardakhti = 0;
            Int64 sahmsherkat = 0;
            DataSet ds = new DataSet();
            SqlDataAdapter adb = new SqlDataAdapter();
            adb.SelectCommand = new SqlCommand();
            DataTable dtbl = new DataTable();
            adb.SelectCommand.Connection = con;
            adb.SelectCommand.CommandText = "select * from View_ParvandeToPardakht where Years=" + years + " ORDER BY VarizDate desc";
            adb.Fill(ds, "View_ParvandeToPardakht");
            adb.Fill(dtbl);
            int cunt = dtbl.Rows.Count;
            dgvPardakht.DataSource = ds;
            dgvPardakht.DataMember = "View_ParvandeToPardakht";
            dgvPardakht.Columns["ParvandeID"].HeaderText = "شماره پرونده";
            dgvPardakht.Columns["Expr1"].Visible = false;
            dgvPardakht.Columns["NameVarizKonande"].Visible = false;
            dgvPardakht.Columns["ShomareSanad"].Visible = false;
            dgvPardakht.Columns["Mablagh"].HeaderText = "مبلغ واریزی";
            dgvPardakht.Columns["Mablagh"].Width = 150;
            dgvPardakht.Columns["NoPardakht"].Visible = false;
            dgvPardakht.Columns["ShobeBank"].Visible = false;
            dgvPardakht.Columns["Tozihat"].Visible = false;
            dgvPardakht.Columns["VarizDate"].HeaderText = "تاریخ واریز ";
            dgvPardakht.Columns["VarizDate"].Width = 80;
            dgvPardakht.Columns["DateTimeSabt"].Visible = false;
            dgvPardakht.Columns["Years"].Visible = false;
            dgvPardakht.Columns["Month"].Visible = false;
            //------------------------------------------------------------
            dgvPardakht.Columns["ParvandeID"].HeaderText = "شماره پرونده";
            dgvPardakht.Columns["BankName"].HeaderText = "نام بانک";
            dgvPardakht.Columns["BankName"].Width = 100;
            dgvPardakht.Columns["Shobe"].HeaderText = "شعبه";
            dgvPardakht.Columns["Shobe"].Width = 100;
            dgvPardakht.Columns["Name"].HeaderText = "نام مدیون";
            dgvPardakht.Columns["Name"].Width = 100;
            dgvPardakht.Columns["FatherName"].Visible = false;
            dgvPardakht.Columns["Melli"].Visible = false;
            dgvPardakht.Columns["Shoghl"].Visible = false;
            dgvPardakht.Columns["TelSabet"].Visible = false;
            dgvPardakht.Columns["TelHamrah"].Visible = false;
            dgvPardakht.Columns["ShTashilat"].Visible = false;
            dgvPardakht.Columns["Date"].Visible = false;
            dgvPardakht.Columns["Bed"].HeaderText = "مبلغ بدهی";
            dgvPardakht.Columns["Bed"].Width = 100;
            dgvPardakht.Columns["Avarde"].HeaderText = "سهم شرکت";
            dgvPardakht.Columns["Avarde"].Width = 100;
            dgvPardakht.Columns["Address"].Visible = false;
            dgvPardakht.Columns["DateSabt"].Visible = false;
            //pardakhti= Int64.Parse(dtbl.Rows[0]["Mablagh"].ToString());
            for (int i = 0; i <= cunt - 1; i++)
            {
                pardakhti += Int64.Parse(dtbl.Rows[i]["Mablagh"].ToString());
                sahmsherkat += Int64.Parse(dtbl.Rows[i]["Avarde"].ToString());
            }
            lblPardakhtBank.Text = pardakhti.ToString("N0");
            lblAvarde.Text = sahmsherkat.ToString("N0");

        }
        void DisplayPardakht(int years, string bank)
        {
            Int64 pardakhti = 0;
            Int64 sahmsherkat = 0;
            DataSet ds = new DataSet();
            SqlDataAdapter adb = new SqlDataAdapter();
            adb.SelectCommand = new SqlCommand();
            DataTable dtbl = new DataTable();
            adb.SelectCommand.Connection = con;
            adb.SelectCommand.CommandText = "select * from View_ParvandeToPardakht where Years=" + years + " and BankName=N'" + bank + "' ORDER BY VarizDate desc";
            adb.Fill(ds, "View_ParvandeToPardakht");
            adb.Fill(dtbl);
            int cunt = dtbl.Rows.Count;
            dgvPardakht.DataSource = ds;
            dgvPardakht.DataMember = "View_ParvandeToPardakht";
            dgvPardakht.Columns["ParvandeID"].HeaderText = "شماره پرونده";
            dgvPardakht.Columns["Expr1"].Visible = false;
            dgvPardakht.Columns["NameVarizKonande"].Visible = false;
            dgvPardakht.Columns["ShomareSanad"].Visible = false;
            dgvPardakht.Columns["Mablagh"].HeaderText = "مبلغ";
            dgvPardakht.Columns["Mablagh"].Width = 150;
            dgvPardakht.Columns["NoPardakht"].Visible = false;
            dgvPardakht.Columns["ShobeBank"].Visible = false;
            dgvPardakht.Columns["Tozihat"].Visible = false;
            dgvPardakht.Columns["VarizDate"].HeaderText = "تاریخ واریز ";
            dgvPardakht.Columns["VarizDate"].Width = 80;
            dgvPardakht.Columns["DateTimeSabt"].Visible = false;
            dgvPardakht.Columns["Years"].Visible = false;
            dgvPardakht.Columns["Month"].Visible = false;
            //------------------------------------------------------------
            dgvPardakht.Columns["ParvandeID"].HeaderText = "شماره پرونده";
            dgvPardakht.Columns["BankName"].HeaderText = "نام بانک";
            dgvPardakht.Columns["BankName"].Width = 100;
            dgvPardakht.Columns["Shobe"].HeaderText = "شعبه";
            dgvPardakht.Columns["Shobe"].Width = 100;
            dgvPardakht.Columns["Name"].HeaderText = "نام مدیون";
            dgvPardakht.Columns["Name"].Width = 100;
            dgvPardakht.Columns["FatherName"].Visible = false;
            dgvPardakht.Columns["Melli"].Visible = false;
            dgvPardakht.Columns["Shoghl"].Visible = false;
            dgvPardakht.Columns["TelSabet"].Visible = false;
            dgvPardakht.Columns["TelHamrah"].Visible = false;
            dgvPardakht.Columns["ShTashilat"].Visible = false;
            dgvPardakht.Columns["Date"].Visible = false;
            dgvPardakht.Columns["Bed"].HeaderText = "مبلغ بدهی";
            dgvPardakht.Columns["Bed"].Width = 100;
            dgvPardakht.Columns["Avarde"].HeaderText = "سهم شرکت";
            dgvPardakht.Columns["Avarde"].Width = 100;
            dgvPardakht.Columns["Address"].Visible = false;
            dgvPardakht.Columns["DateSabt"].Visible = false;
            for (int i = 0; i <= cunt - 1; i++)
            {
                pardakhti += Int64.Parse(dtbl.Rows[i]["Mablagh"].ToString());
                sahmsherkat += Int64.Parse(dtbl.Rows[i]["Avarde"].ToString());
            }
            lblPardakhtBank.Text = pardakhti.ToString("N0");
            lblAvarde.Text = sahmsherkat.ToString("N0");
        }
        void DisplayPardakht(int years, string bank,string shobe)
        {
            Int64 pardakhti = 0;
            Int64 sahmsherkat = 0;

            DataSet ds = new DataSet();
            SqlDataAdapter adb = new SqlDataAdapter();
            adb.SelectCommand = new SqlCommand();
            DataTable dtbl = new DataTable();
            adb.SelectCommand.Connection = con;
            adb.SelectCommand.CommandText = "select * from View_ParvandeToPardakht where Years=" + years + " and BankName=N'" + bank + "' and Shobe=N'" + shobe + "' ORDER BY VarizDate desc";
            adb.Fill(ds, "View_ParvandeToPardakht");
            dgvPardakht.DataSource = ds;
            adb.Fill(dtbl);
            int cunt = dtbl.Rows.Count;
            dgvPardakht.DataMember = "View_ParvandeToPardakht";
            dgvPardakht.Columns["ParvandeID"].HeaderText = "شماره پرونده";
            dgvPardakht.Columns["Expr1"].Visible = false;
            dgvPardakht.Columns["NameVarizKonande"].Visible = false;
            dgvPardakht.Columns["ShomareSanad"].Visible = false;
            dgvPardakht.Columns["Mablagh"].HeaderText = "مبلغ";
            dgvPardakht.Columns["Mablagh"].Width = 150;
            dgvPardakht.Columns["NoPardakht"].Visible = false;
            dgvPardakht.Columns["ShobeBank"].Visible = false;
            dgvPardakht.Columns["Tozihat"].Visible = false;
            dgvPardakht.Columns["VarizDate"].HeaderText = "تاریخ واریز ";
            dgvPardakht.Columns["VarizDate"].Width = 80;
            dgvPardakht.Columns["DateTimeSabt"].Visible = false;
            dgvPardakht.Columns["Years"].Visible = false;
            dgvPardakht.Columns["Month"].Visible = false;
            //------------------------------------------------------------
            dgvPardakht.Columns["ParvandeID"].HeaderText = "شماره پرونده";
            dgvPardakht.Columns["BankName"].HeaderText = "نام بانک";
            dgvPardakht.Columns["BankName"].Width = 100;
            dgvPardakht.Columns["Shobe"].HeaderText = "شعبه";
            dgvPardakht.Columns["Shobe"].Width = 100;
            dgvPardakht.Columns["Name"].HeaderText = "نام مدیون";
            dgvPardakht.Columns["Name"].Width = 100;
            dgvPardakht.Columns["FatherName"].Visible = false;
            dgvPardakht.Columns["Melli"].Visible = false;
            dgvPardakht.Columns["Shoghl"].Visible = false;
            dgvPardakht.Columns["TelSabet"].Visible = false;
            dgvPardakht.Columns["TelHamrah"].Visible = false;
            dgvPardakht.Columns["ShTashilat"].Visible = false;
            dgvPardakht.Columns["Date"].Visible = false;
            dgvPardakht.Columns["Bed"].HeaderText = "مبلغ بدهی";
            dgvPardakht.Columns["Bed"].Width = 100;
            dgvPardakht.Columns["Avarde"].HeaderText = "سهم شرکت";
            dgvPardakht.Columns["Avarde"].Width = 100;
            dgvPardakht.Columns["Address"].Visible = false;
            dgvPardakht.Columns["DateSabt"].Visible = false;
            for (int i = 0; i <= cunt - 1; i++)
            {
                pardakhti += Int64.Parse(dtbl.Rows[i]["Mablagh"].ToString());
                sahmsherkat += Int64.Parse(dtbl.Rows[i]["Avarde"].ToString());
            }
            lblPardakhtBank.Text = pardakhti.ToString("N0");
            lblAvarde.Text = sahmsherkat.ToString("N0");
        }
        void DisplayPardakht(int years, int month, string bank, string shobe)
        {
            Int64 pardakhti = 0;
            Int64 sahmsherkat = 0;
            DataSet ds = new DataSet();
            SqlDataAdapter adb = new SqlDataAdapter();
            adb.SelectCommand = new SqlCommand();
            DataTable dtbl = new DataTable();
            adb.SelectCommand.Connection = con;
            adb.SelectCommand.CommandText = "select * from View_ParvandeToPardakht where Years=" + years + " and BankName=N'" + bank + "' and Shobe=N'" + shobe + "' and Month=N'"+month+"' ORDER BY VarizDate desc";
            adb.Fill(ds, "View_ParvandeToPardakht");
            dgvPardakht.DataSource = ds;
            adb.Fill(dtbl);
            int cunt = dtbl.Rows.Count;
            dgvPardakht.DataMember = "View_ParvandeToPardakht";
            dgvPardakht.Columns["ParvandeID"].HeaderText = "شماره پرونده";
            dgvPardakht.Columns["Expr1"].Visible = false;
            dgvPardakht.Columns["NameVarizKonande"].Visible = false;
            dgvPardakht.Columns["ShomareSanad"].Visible = false;
            dgvPardakht.Columns["Mablagh"].HeaderText = "مبلغ";
            dgvPardakht.Columns["Mablagh"].Width = 150;
            dgvPardakht.Columns["NoPardakht"].Visible = false;
            dgvPardakht.Columns["ShobeBank"].Visible = false;
            dgvPardakht.Columns["Tozihat"].Visible = false;
            dgvPardakht.Columns["VarizDate"].HeaderText = "تاریخ واریز ";
            dgvPardakht.Columns["VarizDate"].Width = 80;
            dgvPardakht.Columns["DateTimeSabt"].Visible = false;
            dgvPardakht.Columns["Years"].Visible = false;
            dgvPardakht.Columns["Month"].Visible = false;
            //------------------------------------------------------------
            dgvPardakht.Columns["ParvandeID"].HeaderText = "شماره پرونده";
            dgvPardakht.Columns["BankName"].HeaderText = "نام بانک";
            dgvPardakht.Columns["BankName"].Width = 100;
            dgvPardakht.Columns["Shobe"].HeaderText = "شعبه";
            dgvPardakht.Columns["Shobe"].Width = 100;
            dgvPardakht.Columns["Name"].HeaderText = "نام مدیون";
            dgvPardakht.Columns["Name"].Width = 100;
            dgvPardakht.Columns["FatherName"].Visible = false;
            dgvPardakht.Columns["Melli"].Visible = false;
            dgvPardakht.Columns["Shoghl"].Visible = false;
            dgvPardakht.Columns["TelSabet"].Visible = false;
            dgvPardakht.Columns["TelHamrah"].Visible = false;
            dgvPardakht.Columns["ShTashilat"].Visible = false;
            dgvPardakht.Columns["Date"].Visible = false;
            dgvPardakht.Columns["Bed"].HeaderText = "مبلغ بدهی";
            dgvPardakht.Columns["Bed"].Width = 100;
            dgvPardakht.Columns["Avarde"].HeaderText = "سهم شرکت";
            dgvPardakht.Columns["Avarde"].Width = 100;
            dgvPardakht.Columns["Address"].Visible = false;
            dgvPardakht.Columns["DateSabt"].Visible = false;
            for (int i = 0; i <= cunt - 1; i++)
            {
                pardakhti += Int64.Parse(dtbl.Rows[i]["Mablagh"].ToString());
                sahmsherkat += Int64.Parse(dtbl.Rows[i]["Avarde"].ToString());
            }
            lblPardakhtBank.Text = pardakhti.ToString("N0");
            lblAvarde.Text = sahmsherkat.ToString("N0");
        }
        private void frmVarizi_Load(object sender, EventArgs e)
        {
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
            ADDTodmnYears();
            DisplayComboNoBank();
            DisplayPardakht(dt.GetYear(DateTime.Now));
            int month = dt.GetMonth(DateTime.Now);
            cmbPMonth.SelectedIndex = month-1;
            load = true;
        }
        private void cmbBankNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            bankID = GetNoBankID(cmbBankNo.Text);
            DisplayShobe(bankID);
            if (load == true)
            {
                
                DisplayPardakht(int.Parse(dmnYears.Text), cmbBankNo.Text);
                BankNo = true;
                
            }
        }

        private void cmdShobe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (load == true && BankNo==true)
            {
                DisplayPardakht(int.Parse(dmnYears.Text), cmbBankNo.Text,cmdShobe.Text);
                shobe = true;
            }
        }

        private void cmbPMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (load == true && BankNo == true && shobe == true)
            {
                DisplayPardakht(int.Parse(dmnYears.Text), cmbPMonth.SelectedIndex+1, cmbBankNo.Text, cmdShobe.Text);
            }
        }

        private void dgvPardakht_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == this.dgvPardakht.Columns["Mablagh"].Index && e.RowIndex != this.dgvPardakht.NewRowIndex)
            {
                double d = double.Parse(e.Value.ToString());
                e.Value = d.ToString("#,##0.##");
            }
            if (e.ColumnIndex == this.dgvPardakht.Columns["Bed"].Index && e.RowIndex != this.dgvPardakht.NewRowIndex)
            {
                double d = double.Parse(e.Value.ToString());
                e.Value = d.ToString("#,##0.##");
            }
            if (e.ColumnIndex == this.dgvPardakht.Columns["Avarde"].Index && e.RowIndex != this.dgvPardakht.NewRowIndex)
            {
                double d = double.Parse(e.Value.ToString());
                e.Value = d.ToString("#,##0.##");
            }
            //{

            //    double s = 0;
            //    for (int i = 0; i <= dataGridViewX1.Rows.Count - 2; i++)
            //    {
            //        s += Convert.ToDouble(dataGridViewX1.Rows[i].Cells["kol"].Value.ToString());
            //    }
            //    //نوشتن عبارت جمع کل در ستون یک
            //    dataGridViewX1.Rows[dataGridViewX1.Rows.Count - 1].Cells["col1"].Value = " ";
            //    //نوشتن جمع کل در ستون kol
            //    dataGridViewX1.Rows[dataGridViewX1.Rows.Count - 1].Cells["kol"].Value = s.ToString();
            //}
        }
    }
}
