using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hoghoghi
{
    public partial class frmHoghogh : Form
    {
        public frmHoghogh()
        {
            InitializeComponent();
        }
        string path = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        System.Globalization.PersianCalendar dt = new System.Globalization.PersianCalendar();
        clsGetDataSource gds = new clsGetDataSource();
        DataTable dt_MablaghGharadad = new DataTable();
        public int UserID { get; set; }
        public int chk { get; set; }
        public int Sath { get; set; }
        void sath()
        {
            if (Sath == 0)
            {
                txtInFamSearch.Enabled = false;
            }
            else
            {
                // DisplayParvande(dt.GetYear(DateTime.Now));
            }
        }
        int hoghoghID = -1;
        void Search()
        {
            try
            {
                cmd.Parameters.Clear();
                DataSet ds = new DataSet();
                DataTable dt1 = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from tblUser where UserID=" + UserID;
                con.Open();
                adp.Fill(dt1);
                lblName.Text = (dt1.Rows[0]["Name"].ToString());
                UserID = (int)dt1.Rows[0]["UserID"];
                con.Close();
                dgvInSearch.Visible = false; ;
                lblHoghogh.Text = DisplayHoghoghDaryafti(dt.GetYear(DateTime.Now)).ToString("N0");
                DisplayHoghogh();
                lblAvardeKol.Text = DisplayAvardeKarmozdKol(UserID, txtYears.Text).ToString("N0");
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در انتخاب رکورد رخ داده است.");
            }
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
        public Int64 DisplayHoghoghDaryafti(int year)
        {
            Int64 hoghogh = 0;
            try
            {
                con.Close();
                string query = "SELECT * from tblHoghogh where UserID=" + UserID + " and Years=" + year;
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    hoghogh += Convert.ToInt64(dt.Rows[i]["Mablagh"]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش اطلاعات تابع DisplayHoghoghDaryafti رخ داده است");
            }
            return hoghogh;
        }
        public Int64 DisplayHoghoghDaryafti(int year, int month)
        {
            Int64 hoghogh = 0;
            try
            {
                con.Close();
                string query = "SELECT * from tblHoghogh where UserID=" + UserID + " and Years=" + year + "and Month=" + month;
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    hoghogh += Convert.ToInt64(dt.Rows[i]["Mablagh"]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش اطلاعات تابع DisplayHoghoghDaryafti رخ داده است");
            }
            lblHoghogh.Text = hoghogh.ToString("N0");
            return hoghogh;
        }
        void DisplayHoghogh()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adb = new SqlDataAdapter();
            adb.SelectCommand = new SqlCommand();
            adb.SelectCommand.Connection = con;
            adb.SelectCommand.CommandText = "select * from tblHoghogh where UserID=" + UserID + " ORDER BY HoghoghID desc ";
            adb.Fill(ds, "tblHoghogh");
            dgvHoghogh.DataSource = ds;
            dgvHoghogh.DataMember = "tblHoghogh";
            dgvHoghogh.Columns["HoghoghID"].Visible = false;
            dgvHoghogh.Columns["UserID"].Visible = false;
            dgvHoghogh.Columns["Mablagh"].HeaderText = "مبلغ";
            dgvHoghogh.Columns["Mablagh"].Width = 150;
            dgvHoghogh.Columns["Years"].HeaderText = "سال";
            dgvHoghogh.Columns["Years"].Width = 70;
            dgvHoghogh.Columns["Month"].HeaderText = "ماه";
            dgvHoghogh.Columns["Month"].Width = 30;
            dgvHoghogh.Columns["Tozihat"].HeaderText = "توضیحات";
            dgvHoghogh.Columns["Tozihat"].Width = 300;
            dgvHoghogh.Columns["DateSabt"].HeaderText = "تاریخ ثبت ";
            dgvHoghogh.Columns["DateSabt"].Width = 90;
            dgvHoghogh.Columns["Avarde"].HeaderText = "آورده";
            dgvHoghogh.Columns["Avarde"].Width = 150;
            dgvHoghogh.Columns["Karmozd"].HeaderText = "کارمزد";
            dgvHoghogh.Columns["Karmozd"].Width = 150;
            con.Close();
        }
        void DisplayHoghogh(int userid)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adb = new SqlDataAdapter();
            adb.SelectCommand = new SqlCommand();
            adb.SelectCommand.Connection = con;
            adb.SelectCommand.CommandText = "select * from tblHoghogh where UserID=" + userid + " ORDER BY HoghoghID desc ";
            adb.Fill(ds, "tblHoghogh");
            dgvHoghogh.DataSource = ds;
            dgvHoghogh.DataMember = "tblHoghogh";
            dgvHoghogh.Columns["HoghoghID"].Visible = false;
            dgvHoghogh.Columns["UserID"].Visible = false;
            dgvHoghogh.Columns["Mablagh"].HeaderText = "مبلغ";
            dgvHoghogh.Columns["Mablagh"].Width = 150;
            dgvHoghogh.Columns["Years"].HeaderText = "سال";
            dgvHoghogh.Columns["Years"].Width = 70;
            dgvHoghogh.Columns["Month"].HeaderText = "ماه";
            dgvHoghogh.Columns["Month"].Width = 30;
            dgvHoghogh.Columns["Tozihat"].HeaderText = "توضیحات";
            dgvHoghogh.Columns["Tozihat"].Width = 300;
            dgvHoghogh.Columns["DateSabt"].HeaderText = "تاریخ ثبت ";
            dgvHoghogh.Columns["DateSabt"].Width = 90;
            con.Close();
        }
       public DataTable DisplayKarmozd(int years)
        {
            DataTable dt = new DataTable();
            //dt = null;
            try
            {
                con.Close();
                string query = "SELECT * from tblKarmozd where Year= " + years;
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش اطلاعات تابع Karmozd رخ داده است");
            }
            return dt;
        }
        public DataTable Karmozr()
        {
            DataTable dtKarmozd = new DataTable();
            con.Close();
            string query = "SELECT * from tblKarmozd ";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Open();
            cmd.ExecuteScalar();
            con.Close();
            dtKarmozd = dt;
            return dtKarmozd;
        }
        public void MohasebeKarmozd( double avarde)
        {
            double krmzd = 0;
            for (int i = 0; i < dt_MablaghGharadad.Rows.Count; i++)
            {
                if (Convert.ToDouble(dt_MablaghGharadad.Rows[i]["Saghf"]) >= avarde)
                {
                    krmzd = avarde * Convert.ToDouble(dt_MablaghGharadad.Rows[i]["Darsad"]) / 100;
                }
                break;
            }
            lblKarmozd.Text = krmzd.ToString("N0");
            txtKarmozd.Text = krmzd.ToString("N0");

        }
        public double DisplayAvardeKarmozd(int id, int years, int month)
        {
            double hoghogh = 0;
            try
            {
                con.Close();
                string query = "SELECT * from View_UserFaliat where UserID=" + id + " and Month='" + month + "' and Years=" + years;
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    hoghogh += Convert.ToDouble(dt.Rows[i]["Avarde"]);
                }
                lblavarde.Text = hoghogh.ToString("N0");
                btnAvarde.Text = hoghogh.ToString("N0");
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش اطلاعات تابع DisplayAvardeKarmozd رخ داده است");
            }
            return hoghogh;
        }
        public double DisplayAvardeKarmozd(int id, string AzDate, string TaDate)
        {
            double hoghogh = 0;
            try
            {
                con.Close();
                string query = "SELECT * from View_UserFaliat where UserID=" + id + " and PardakhtDate between '" + AzDate + "' and '" + TaDate + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    hoghogh += Convert.ToDouble(dt.Rows[i]["Avarde"]);
                }
                lblavarde.Text = hoghogh.ToString("N0");
                btnAvarde.Text = hoghogh.ToString("N0");
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش اطلاعات تابع DisplayAvardeKarmozd رخ داده است");
            }
            return hoghogh;
        }
        public double DisplayAvardeKarmozdKol(int id, string years)
        {
            double hoghogh = 0;

            try
            {
                con.Close();
                string query = "SELECT * from View_UserFaliat where UserID=" + id + " and Years=" + years;
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    hoghogh += Convert.ToDouble(dt.Rows[i]["Avarde"]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش اطلاعات تابع DisplayAvardeKarmozd رخ داده است");
            }
            return hoghogh;
        }
        public void txts(string year, string az, string ta)
        {
            txtDate.Text = year + "/" + az;
            txtDate2.Text = year + "/" + ta;
        }
        public int Mah(int month)
        {
            int m = 0;
            switch (month)
            {
                case 1:
                    m = 0;
                    break;
                case 2:
                    m = 1;
                    break;
                case 3:
                    m = 2;
                    break;
                case 4:
                    m = 3;
                    break;
                case 5:
                    m = 4;
                    break;
                case 6:
                    m = 5;
                    break;
                case 7:
                    m = 6;
                    break;
                case 8:
                    m = 7; ;
                    break;
                case 9:
                    m = 8;
                    break;
                case 10:
                    m = 9;
                    break;
                case 11:
                    m = 10;
                    break;
                case 12:
                    m = 11;
                    break;
            }
            return m;
        }
        private void frmHoghogh_Load(object sender, EventArgs e)
        {
            dgvInSearch.Visible = false;
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
            txtDate.Text = dt.GetYear(DateTime.Now).ToString() + dt.GetMonth(DateTime.Now).ToString("0#") + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
            txtDate2.Text = txtDate.Text;
            txtYears.Text = dt.GetYear(DateTime.Now).ToString("0#");
            //dtKarmozd=  Karmozr();
            dt_MablaghGharadad = DisplayKarmozd(dt.GetYear(DateTime.Now));
            if (Sath == 1)
            {
                txtInFamSearch.Enabled = true;
            }
            else
            {
                txtInFamSearch.Enabled = true;
                try
                {
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.SelectCommand = new SqlCommand("select * from tblUser where UserID=" + UserID, con);
                    DataTable dtbl = new DataTable(); adp.Fill(dtbl);
                    con.Open();
                    txtInFamSearch.Text = dtbl.Rows[0]["Name"].ToString();
                    con.Close();
                    txtInFamSearch.Enabled = false;
                    lblHoghogh.Text = DisplayHoghoghDaryafti(dt.GetYear(DateTime.Now)).ToString("N0");
                    DisplayHoghogh();
                    lblAvardeKol.Text = DisplayAvardeKarmozdKol(UserID, txtYears.Text).ToString("N0");
                }
                catch (Exception)
                {
                    MessageBox.Show("لطفا نام و نام خانوادگی را وارد کنید");
                }

            }
            if (chk == 1)
            {
                DisplayHoghogh(UserID);
                Search();
            }
        }
        private void txtMablaghVariz_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMablaghVariz.Text != string.Empty)
                {
                    txtMablaghVariz.Text = string.Format("{0:N0}", double.Parse(txtMablaghVariz.Text.Replace(",", "")));
                    txtMablaghVariz.Select(txtMablaghVariz.TextLength, 0);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("خطایی در درج اطلاعات رخ داده است.");
            }
        }
        private void txtInFamSearch_TextChanged(object sender, EventArgs e)
        {
            if (Sath == 1)
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
                lblName.Text = (dt1.Rows[0]["Name"].ToString());
                UserID = (int)dt1.Rows[0]["UserID"];
                con.Close();
                dgvInSearch.Visible = false; ;
                lblHoghogh.Text = DisplayHoghoghDaryafti(dt.GetYear(DateTime.Now)).ToString("N0");
                DisplayHoghogh();
                lblAvardeKol.Text = DisplayAvardeKarmozdKol(UserID, txtYears.Text).ToString("N0");
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در انتخاب رکورد رخ داده است.");
            }
            cmbNoPardakht.Focus();
        }
        private void btnPygiriSave_Click(object sender, EventArgs e)
        {
            string s = txtDate.Text;
            string[] y = s.Split('/');
            //int years = Convert.ToInt16(y[0]);
            //int month = Convert.ToInt16(y[1]);
            int years = int.Parse(txtYears.Text);
            int month = cmbNoPardakht.SelectedIndex + 1;
            if (txtMablaghVariz.Text == "")
            {
                MessageBox.Show("لطفا فیلد های ستاره دار را پر کنید");
            }
            else
            {
                try
                {
                    Double avarde = Convert.ToDouble(btnAvarde.Text.Replace(",", ""));
                    double karmozd = double.Parse(lblKarmozd.Text);
                    var date = dt.GetYear(DateTime.Now).ToString() + "/" + dt.GetMonth(DateTime.Now).ToString("0#") + "/" + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
                    cmd.Parameters.Clear();
                    cmd.Connection = con;
                    cmd.CommandText = "insert into tblHoghogh(UserID,Mablagh,Years,Month,DateSabt,Tozihat,Avarde,Karmozd)values                                        (@UserID,@Mablagh,@Years,@Month,@DateSabt,@Tozihat,@Avarde,@Karmozd)";
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@Mablagh", Convert.ToInt64(txtMablaghVariz.Text.Replace(",", "")));
                    cmd.Parameters.AddWithValue("@Years", years);
                    cmd.Parameters.AddWithValue("@Month", month);
                    cmd.Parameters.AddWithValue("@DateSabt", date);
                    cmd.Parameters.AddWithValue("@Tozihat", txtTozihat.Text);
                    cmd.Parameters.AddWithValue("@Avarde", avarde);
                    cmd.Parameters.AddWithValue("@Karmozd", karmozd);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    DisplayHoghogh();
                    MessageBox.Show("ثبت با موفقیت انجام شد");
                }
                catch (Exception)
                {
                    MessageBox.Show("مشکلی در ثبت اطلاعات وجود دارد!");
                }
            }
        }
        private void cmbNoPardakht_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                switch (cmbNoPardakht.SelectedIndex)
                {
                    case 0:
                        txts(txtYears.Text, "/01/01", "/01/31");
                        //MohasebeKarmozd(DisplayAvardeKarmozd(UserID, txtDate.Text,txtDate2.Text));
                        //DisplayHoghoghDaryafti(int.Parse(txtYears.Text), 1);

                        break;
                    case 1:
                        txts(txtYears.Text, "/02/01", "/02/31");
                        //MohasebeKarmozd(DisplayAvardeKarmozd(UserID, txtDate.Text, txtDate2.Text));
                        //DisplayHoghoghDaryafti(int.Parse(txtYears.Text), 2);
                        break;
                    case 2:
                        txts(txtYears.Text, "/03/01", "/03/31");
                        //MohasebeKarmozd(DisplayAvardeKarmozd(UserID, txtDate.Text, txtDate2.Text));
                        //DisplayHoghoghDaryafti(int.Parse(txtYears.Text), 3);
                        break;
                    case 3:
                        txts(txtYears.Text, "/04/01", "/04/31");
                        //MohasebeKarmozd(DisplayAvardeKarmozd(UserID, txtDate.Text, txtDate2.Text));
                        //DisplayHoghoghDaryafti(int.Parse(txtYears.Text), 4);
                        break;
                    case 4:
                        txts(txtYears.Text, "/05/01", "/05/31");
                        //MohasebeKarmozd(DisplayAvardeKarmozd(UserID, txtDate.Text, txtDate2.Text));
                        //DisplayHoghoghDaryafti(int.Parse(txtYears.Text), 5);
                        break;
                    case 5:
                        txts(txtYears.Text, "/06/01", "/06/31");
                        //MohasebeKarmozd(DisplayAvardeKarmozd(UserID, txtDate.Text, txtDate2.Text));
                        //DisplayHoghoghDaryafti(int.Parse(txtYears.Text), 6);
                        break;
                    case 6:
                        txts(txtYears.Text, "/07/01", "/07/31");
                        //MohasebeKarmozd(DisplayAvardeKarmozd(UserID, txtDate.Text, txtDate2.Text));
                        //DisplayHoghoghDaryafti(int.Parse(txtYears.Text), 7);
                        break;
                    case 7:
                        txts(txtYears.Text, "/08/01", "/08/31");
                        //MohasebeKarmozd(DisplayAvardeKarmozd(UserID, txtDate.Text, txtDate2.Text));
                        //DisplayHoghoghDaryafti(int.Parse(txtYears.Text), 8);
                        break;
                    case 8:
                        txts(txtYears.Text, "/09/01", "/09/31");
                        //MohasebeKarmozd(DisplayAvardeKarmozd(UserID, txtDate.Text, txtDate2.Text));
                        //DisplayHoghoghDaryafti(int.Parse(txtYears.Text), 9);
                        break;
                    case 9:
                        txts(txtYears.Text, "/10/01", "/10/31");
                        //MohasebeKarmozd(DisplayAvardeKarmozd(UserID, txtDate.Text, txtDate2.Text));
                        //DisplayHoghoghDaryafti(int.Parse(txtYears.Text), 10);
                        break;
                    case 10:
                        txts(txtYears.Text, "/11/01", "/11/31");
                        //MohasebeKarmozd(DisplayAvardeKarmozd(UserID, txtDate.Text, txtDate2.Text));
                        //DisplayHoghoghDaryafti(int.Parse(txtYears.Text), 11);
                        break;
                    case 11:
                        txts(txtYears.Text, "/12/01", "/12/31");
                        //MohasebeKarmozd(DisplayAvardeKarmozd(UserID, int.Parse(txtYears.Text), 12));
                        //DisplayHoghoghDaryafti(int.Parse(txtYears.Text), 12);
                        break;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("خطایی در نمایش اطلاعات SwitchCase رخ داده است");
            }
        }
        private void lblavarde_Click(object sender, EventArgs e)
        {
            frmListFaaliat frm = new frmListFaaliat();
            frm.UserID = UserID;
            if (UserID != 0)
            {
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("لطفا یک نام را جستوجو کنید");
            }
        }
        private void btnAvarde_Click(object sender, EventArgs e)
        {
            frmListFaaliat frm = new frmListFaaliat();
            frm.UserID = UserID;
            frm.Year = txtYears.Text;
            frm.AzDate = txtDate.Text;
            frm.TaDate = txtDate2.Text;
            frm.Month = (int.Parse(cmbNoPardakht.SelectedIndex.ToString()) + 1).ToString();
            if (UserID != 0)
            {
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("لطفا یک نام را جستوجو کنید");
            }
        }
        private void dgvHoghogh_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex != this.dgvHoghogh.NewRowIndex)
            {
                double d = double.Parse(e.Value.ToString());
                e.Value = d.ToString("#,##0.##");
            }
            if (e.ColumnIndex == 5 && e.RowIndex != this.dgvHoghogh.NewRowIndex)
            {
                double d = double.Parse(e.Value.ToString());
                e.Value = d.ToString("#,##0.##");
            }
            if (e.ColumnIndex == 6 && e.RowIndex != this.dgvHoghogh.NewRowIndex)
            {
                double d = double.Parse(e.Value.ToString());
                e.Value = d.ToString("#,##0.##");
            }
        }
        private void btnPygiriEdit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("آیا مایل به ویرایش رکورد هستتید؟", "هشدار", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    int years = int.Parse(txtYears.Text);
                    int month = cmbNoPardakht.SelectedIndex + 1;
                    Double avarde = DisplayAvardeKarmozd(UserID, int.Parse(txtYears.Text), (cmbNoPardakht.SelectedIndex + 1));
                    double karmozd = double.Parse(lblKarmozd.Text);
                    cmd.Parameters.Clear();
                    cmd.Connection = con;
                    cmd.CommandText = "UPDATE tblHoghogh set Mablagh=N'" + Convert.ToInt64(txtMablaghVariz.Text.Replace(",", "")) +
                    "' ,Years=N'" + years +
                    "' ,Month=N'" + month +
                    "' ,Avarde=N'" + avarde +
                    "' ,Karmozd=N'" + karmozd +
                    "' ,Tozihat=N'" + txtTozihat.Text + "' where HoghoghID=" + hoghoghID;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    DisplayHoghogh();
                    MessageBox.Show("ویرایش اطلاعات انجام شد.");
                    txtMablaghVariz.Text = "";
                    txtTozihat.Text = "";
                    hoghoghID = -1;
                }
                catch (Exception)
                {
                    MessageBox.Show("خطایی در ثبت رکورد رخ داده است");
                }
            }
        }
        private void dgvHoghogh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvHoghogh.Rows[e.RowIndex].Selected = true;
            try
            {
                hoghoghID = (int)dgvHoghogh.Rows[e.RowIndex].Cells["HoghoghID"].Value;
                cmd.Parameters.Clear();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from [tblHoghogh] where HoghoghID =" + hoghoghID;
                con.Open();
                adp.Fill(dt);
                this.txtMablaghVariz.Text = dt.Rows[0]["Mablagh"].ToString();
                this.txtTozihat.Text = dt.Rows[0]["Tozihat"].ToString();
                this.cmbNoPardakht.SelectedIndex = Mah((int)dt.Rows[0]["Month"]);
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در انتخاب رکورد رخ داده است");
            }
        }
        private void btnPygiriDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("آیا مایل به حذف رکورد هستتید؟", "هشدار", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    cmd.Parameters.Clear();
                    cmd.Connection = con;
                    cmd.CommandText = "DELETE from tblHoghogh  where HoghoghID=" + hoghoghID;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    DisplayHoghogh();
                    MessageBox.Show("حذف اطلاعات  با موفقیت انجام شد.");
                    txtMablaghVariz.Text = "";
                    txtTozihat.Text = "";
                    hoghoghID = -1;
                }
                catch (Exception)
                {
                    MessageBox.Show("خطایی در ثبت رکورد رخ داده است");
                }
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            MohasebeKarmozd(DisplayAvardeKarmozd(UserID, txtDate.Text, txtDate2.Text));
            DisplayHoghoghDaryafti(int.Parse(txtYears.Text), cmbNoPardakht.SelectedIndex + 1);
        }
    }
}
