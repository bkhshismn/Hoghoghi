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
    public partial class frmGozareshKarsenas : Form
    {
        public frmGozareshKarsenas()
        {
            InitializeComponent();
        }
        string path = "";
        int count = 0;
        int countTShobe = 0;
        int bankID = -1;
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        System.Globalization.PersianCalendar dt = new System.Globalization.PersianCalendar();
        public int UserID { get; set; }
        clsGetDataSource gds = new clsGetDataSource();
        int parvandeID = -1;
        private void LoadfrmPardakht(int outid)
        {
            frmParvandeMain frmPM = new frmParvandeMain();
            frmPM.ParvandeID = parvandeID;
            frmPM.ShowDialog();
        }
        void TitleDisplayParvande()
        {
            dgvRprtUser.Columns["UserID"].Visible = false;
            dgvRprtUser.Columns["Vaziat"].Visible = false;
            dgvRprtUser.Columns["Years"].Visible = false;
            dgvRprtUser.Columns["Month"].Visible = false;
            dgvRprtUser.Columns["NoMakhtome"].Visible = false;
            dgvRprtUser.Columns["DateMakhtome"].Visible = false;
            dgvRprtUser.Columns["ParvandeID"].HeaderText = "شماره پرونده";
            dgvRprtUser.Columns["BankName"].HeaderText = "نام بانک";
            dgvRprtUser.Columns["BankName"].Width = 100;
            dgvRprtUser.Columns["Shobe"].HeaderText = "شعبه";
            dgvRprtUser.Columns["Shobe"].Width = 100;
            dgvRprtUser.Columns["Name"].HeaderText = "نام مدیون";
            dgvRprtUser.Columns["Name"].Width = 100;
            dgvRprtUser.Columns["FatherName"].HeaderText = "نام پدر";
            dgvRprtUser.Columns["FatherName"].Width = 50;
            dgvRprtUser.Columns["Melli"].HeaderText = "شماره ملی";
            dgvRprtUser.Columns["Melli"].Width = 70;
            dgvRprtUser.Columns["Shoghl"].HeaderText = "شغل";
            dgvRprtUser.Columns["Shoghl"].Width = 50;
            dgvRprtUser.Columns["TelSabet"].HeaderText = "تلفن ثابت";
            dgvRprtUser.Columns["TelSabet"].Width = 80;
            dgvRprtUser.Columns["TelHamrah"].HeaderText = "شماره همراه";
            dgvRprtUser.Columns["TelHamrah"].Width = 80;
            dgvRprtUser.Columns["ShTashilat"].HeaderText = "شماره تسهیلات";
            dgvRprtUser.Columns["ShTashilat"].Width = 70;
            dgvRprtUser.Columns["Date"].HeaderText = "تاریخ سر رسید ";
            dgvRprtUser.Columns["Date"].Width = 70;
            dgvRprtUser.Columns["Bed"].HeaderText = "مبلغ بدهی";
            dgvRprtUser.Columns["Bed"].Width = 100;
            dgvRprtUser.Columns["Address"].HeaderText = "آدرس";
            dgvRprtUser.Columns["Address"].Width = 250;
            dgvRprtUser.Columns["DateSabt"].HeaderText = "تاریخ ثبت ";
            dgvRprtUser.Columns["DateSabt"].Width = 70;
            dgvRprtUser.Columns["Komision"].HeaderText = "کمیسیون";
        }
        public void Titr(DataGridView dgvInSearch)
        {
            dgvInSearch.Columns["UserID"].HeaderText = "کد کارشناس";
            dgvInSearch.Columns["UserID"].Width = 30;
            dgvInSearch.Columns["UserID"].Visible = false;
            dgvInSearch.Columns["Name"].HeaderText = " نام";
            dgvInSearch.Columns["Name"].Width = 200;
            dgvInSearch.Columns["SathDasresi"].Visible = false;
            dgvInSearch.Columns["usrTel"].Visible = false;
            dgvInSearch.Columns["usrName"].Visible = false;
            dgvInSearch.Columns["usrPass"].Visible = false;
        }
        void DisplayParvande()
        {

            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adb = new SqlDataAdapter();
                adb.SelectCommand = new SqlCommand();
                adb.SelectCommand.Connection = con;
                adb.SelectCommand.CommandText = "select * from tblParvande where UserID=" + UserID + " and Years=" + int.Parse(dmnYears.Text) + "  ORDER BY ParvandeID desc";
                adb.Fill(ds, "dgvRprtUser");
                dgvRprtUser.DataSource = ds;
                dgvRprtUser.DataMember = "dgvRprtUser";
                TitleDisplayParvande();
                count = dgvRprtUser.Rows.Count - 1;

            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش لیست پرونده رخ داده است");
            }
            lblCount.Text = (count).ToString();
            lblTShobe.Text = countTShobe.ToString();
        }
        void DisplayParvande(int years)
        {

            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adb = new SqlDataAdapter();
                adb.SelectCommand = new SqlCommand();
                adb.SelectCommand.Connection = con;
                adb.SelectCommand.CommandText = "select * from tblParvande where UserID=" + UserID +
                                                                            " and Years=" + years +
                                                                            " ORDER BY ParvandeID desc";
                adb.Fill(ds, "dgvRprtUser");
                dgvRprtUser.DataSource = ds;
                dgvRprtUser.DataMember = "dgvRprtUser";
                TitleDisplayParvande();
                countTShobe = dgvRprtUser.Rows.Count - 1;
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش لیست پرونده رخ داده است");
            }
            lblTShobe.Text = (countTShobe).ToString();
        }
        void DisplayParvande(int years, int month)
        {

            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adb = new SqlDataAdapter();
                adb.SelectCommand = new SqlCommand();
                adb.SelectCommand.Connection = con;
                adb.SelectCommand.CommandText = "select * from tblParvande where UserID=" + UserID +
                                                                            " and Years=" + years +
                                                                            " and Month=" + month +
                                                                            " ORDER BY ParvandeID desc";
                adb.Fill(ds, "dgvRprtUser");
                dgvRprtUser.DataSource = ds;
                dgvRprtUser.DataMember = "dgvRprtUser";
                TitleDisplayParvande();
                countTShobe = dgvRprtUser.Rows.Count - 1;
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش لیست پرونده رخ داده است");
            }
            lblTShobe.Text = (countTShobe).ToString();
        }
        void DisplayParvande(int year, string bankname)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adb = new SqlDataAdapter();
                adb.SelectCommand = new SqlCommand();
                adb.SelectCommand.Connection = con;
                adb.SelectCommand.CommandText = "select * from tblParvande where  UserID=" + UserID +
                                                                            "and Years=" + year +
                                                                            "and Years=" + year +
                                                                            "and  BankName=N'" + bankname +
                                                                            "' ORDER BY ParvandeID desc";
                adb.Fill(ds, "tblParvande");
                dgvRprtUser.DataSource = ds;
                dgvRprtUser.DataMember = "tblParvande";
                TitleDisplayParvande();
                countTShobe = dgvRprtUser.Rows.Count - 1;
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش لیست پرونده رخ داده است");
            }
            lblTShobe.Text = (countTShobe).ToString();
        }
        void DisplayParvande(int year, int month, string bankname, string shobe)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adb = new SqlDataAdapter();
                adb.SelectCommand = new SqlCommand();
                adb.SelectCommand.Connection = con;
                adb.SelectCommand.CommandText = "select * from tblParvande where  UserID=" + UserID +
                                                                            " and Years=" + year +
                                                                            "and Month=" + month +
                                                                            "and  BankName=N'" + bankname +
                                                                            "' and  Shobe=N'" + shobe +
                                                                            "'ORDER BY ParvandeID desc";
                adb.Fill(ds, "tblParvande");
                dgvRprtUser.DataSource = ds;
                dgvRprtUser.DataMember = "tblParvande";
                TitleDisplayParvande();
                countTShobe = dgvRprtUser.Rows.Count - 1;
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش لیست پرونده رخ داده است");
            }
            lblTShobe.Text = (countTShobe).ToString();
        }
        void DisplayParvande(int year, string bankname, string shobe)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adb = new SqlDataAdapter();
                adb.SelectCommand = new SqlCommand();
                adb.SelectCommand.Connection = con;
                adb.SelectCommand.CommandText = "select * from tblParvande where  UserID=" + UserID + " and Years=" + year + "and  BankName=N'" + bankname + "' and Shobe=N'" + shobe + "' ORDER BY ParvandeID desc";
                adb.Fill(ds, "tblParvande");
                dgvRprtUser.DataSource = ds;
                dgvRprtUser.DataMember = "tblParvande";
                TitleDisplayParvande();
                countTShobe = dgvRprtUser.Rows.Count - 1;
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش لیست پرونده رخ داده است");
            }
            lblTShobe.Text = (countTShobe).ToString();
        }
        void ADDTodmnYears()
        {
            int years = dt.GetYear(DateTime.Now);
            for (int i = years; i > years - 5; i--)
            {
                dmnYears.Items.Add(i);
            }
            dmnYears.SelectedIndex = 0;
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
        void DisplayShobe(int nobank)
        {
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
        private void frmGozareshKarsenas_Load(object sender, EventArgs e)
        {
            dgvInSearch.Visible = false;
            cmbMonth.Items.Add("همه ماه ها");
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
            //txtDate.Text = dt.GetYear(DateTime.Now).ToString() + dt.GetMonth(DateTime.Now).ToString("0#") + dt.GetDayOfMonth(DateTime.Now).ToString("0#");          
            cmbMonth.SelectedIndex = ((int)dt.GetMonth(DateTime.Now))-1;
            ADDTodmnYears();
            if (UserID == null)
            {
                MessageBox.Show("لطفا نام کارشناس را جستجو کنید");
            }
            else
            {
                DisplayComboNoBank();
                cmbBankNo.SelectedIndex = 1;
                cmbMonth.SelectedIndex = 1;
                DisplayShobe(bankID);
                cmdShobe.SelectedIndex = 1;

            }
            UserID = -1;
        }
        private void txtInFamSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvInSearch.Visible = true;
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from tblUser where Name Like '%' + @s + '%'";
                adp.SelectCommand.Parameters.AddWithValue("@s", txtInFamSearch.Text + "%");
                adp.Fill(ds, "tblUser");
                dgvInSearch.DataSource = ds;
                dgvInSearch.DataMember = "tblUser";
                Titr(dgvInSearch);

            }
            catch (Exception)
            {
                MessageBox.Show("لطفا نام و نام خانوادگی را وارد کنید");
            }
        }
        private void dgvInSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int indx = (int)dgvInSearch.Rows[e.RowIndex].Cells[0].Value;
                cmd.Parameters.Clear();
                DataSet ds = new DataSet();
                DataTable dt1 = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from tblUser where UserID=" + indx;
                con.Open();
                adp.Fill(dt1);
                lblName.Text = (dt1.Rows[0]["UserID"].ToString())+"-"+ (dt1.Rows[0]["Name"].ToString());
                UserID = (int)dt1.Rows[0]["UserID"];
                con.Close();
                dgvInSearch.Visible = false;
                DisplayParvande();
                count = dgvRprtUser.Rows.Count -1;
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در انتخاب رکورد رخ داده است.");
            }
        }
        private void btnParvande_Click(object sender, EventArgs e)
        {
            int y = int.Parse(dmnYears.Text);
            int m = cmbMonth.SelectedIndex + 1;
            DisplayParvande(y,m);
        }
        private void dgvRprtUser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                parvandeID = (int)dgvRprtUser.Rows[e.RowIndex].Cells["ParvandeID"].Value;
                LoadfrmPardakht(parvandeID);
                dgvRprtUser.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception)
            {
            }
        }
        private void btnHoghogh_Click(object sender, EventArgs e)
        {
            if (UserID == 0)
            {
                MessageBox.Show("لطفا نام کارشناس را جستوجو کنید");
            }
            else
            {
                frmHoghogh hgh = new frmHoghogh();
                hgh.UserID = UserID;
                hgh.chk = 1;
                hgh.ShowDialog();
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
        public void DisplayComboShobe()
        {
            try
            {
                con.Close();
                string query = "SELECT  ShobeName FROM [tblShobe] ";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmdShobe.Items.Add(dt.Rows[i]["ShobeName"]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش نام بانک رخ داده است");
            }
        }
        int select_month = -1;
        private void cmbNoPardakht_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UserID!=0)
            {
                DisplayParvande(int.Parse(dmnYears.Text), cmbMonth.SelectedIndex+1, cmbBankNo.Text, cmdShobe.Text);
            }
        }    
        private void cmbBankNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Close();
            try
            {
                
                bankID = GetNoBankID(cmbBankNo.Text);
                DisplayShobe(bankID);
                cmdShobe.SelectedIndex = 0;
                DisplayParvande(int.Parse(dmnYears.Text), cmbBankNo.Text);
            }
            catch (Exception)
            {

            }
        }

        private void dmnYears_SelectedItemChanged(object sender, EventArgs e)
        {
            if (UserID !=0)
            {
                DisplayParvande(int.Parse(dmnYears.Text));
            }  
        }

        private void cmdShobe_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayParvande(int.Parse(dmnYears.Text), cmbBankNo.Text, cmdShobe.Text);
        }

        private void groupPanel2_Click(object sender, EventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DisplayParvande(int.Parse(dmnYears.Text), cmbMonth.SelectedIndex + 1, cmbBankNo.Text, cmdShobe.Text);
        }
    }
}
