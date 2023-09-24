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
    public partial class frmParvande : Form
    {
        public frmParvande()
        {
            InitializeComponent();
        }
        public int UserID { get; set; }
        public int Sath { get; set; }
        string path = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        System.Globalization.PersianCalendar dt = new System.Globalization.PersianCalendar();
        clsGetDataSource gds = new clsGetDataSource();
        int ParvandeID = -1;
        int ZamenID = -1;
        int bankID = -1;
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
        void TitleDisplayParvande()
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
            dgvParvande.Columns["ShTashilat"].Width = 70;
            dgvParvande.Columns["Date"].HeaderText = "تاریخ سر رسید ";
            dgvParvande.Columns["Date"].Width = 70;
            dgvParvande.Columns["Bed"].HeaderText = "مبلغ بدهی";
            dgvParvande.Columns["Bed"].Width = 100;
            dgvParvande.Columns["Address"].HeaderText = "آدرس";
            dgvParvande.Columns["Address"].Width = 250;
            dgvParvande.Columns["DateSabt"].HeaderText = "تاریخ ثبت ";
            dgvParvande.Columns["DateSabt"].Width = 70;
            dgvParvande.Columns["Komision"].HeaderText = "کمیسیون";
        }
        void DisplayShobe(int nobank)
        {
            try
            {
                con.Close();
                string query = "SELECT  ShobeName FROM [tblShobe] where BankID="+ nobank;
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
        void DisplayParvande()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adb = new SqlDataAdapter();
                adb.SelectCommand = new SqlCommand();
                adb.SelectCommand.Connection = con;
                adb.SelectCommand.CommandText = "select * from tblParvande where UserID=" + UserID + " ORDER BY ParvandeID desc";
                adb.Fill(ds, "tblParvande");
                dgvParvande.DataSource = ds;
                dgvParvande.DataMember = "tblParvande";
                TitleDisplayParvande();
            }
            catch (Exception)
            {
            }
            

        }    
        void DisplayParvande(int year)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adb = new SqlDataAdapter();
                adb.SelectCommand = new SqlCommand();
                adb.SelectCommand.Connection = con;
                adb.SelectCommand.CommandText = "select * from tblParvande where Years=" + year + " ORDER BY ParvandeID desc";
                adb.Fill(ds, "tblParvande");
                dgvParvande.DataSource = ds;
                dgvParvande.DataMember = "tblParvande";
                TitleDisplayParvande();
            }
            catch (Exception)
            {
            }
            
        }
        void DisplayZamen()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adb = new SqlDataAdapter();
                adb.SelectCommand = new SqlCommand();
                adb.SelectCommand.Connection = con;
                adb.SelectCommand.CommandText = "select * from tblZamen where ParvandeID=" + ParvandeID + " ORDER BY ZamenID desc";
                adb.Fill(ds, "tblZamen");
                dgvZamen.DataSource = ds;
                dgvZamen.DataMember = "tblZamen";
                dgvZamen.Columns["ZamenID"].HeaderText = "کد ضامن";
                dgvZamen.Columns["ZamenID"].Width = 100;
                dgvZamen.Columns["ParvandeID"].HeaderText = "شماره پرونده";
                dgvZamen.Columns["Name"].HeaderText = "نام ضامن";
                dgvZamen.Columns["Name"].Width = 100;
                dgvZamen.Columns["FatherName"].HeaderText = "نام پدر";
                dgvZamen.Columns["FatherName"].Width = 100;
                dgvZamen.Columns["Melli"].HeaderText = "شماره ملی";
                dgvZamen.Columns["Melli"].Width = 70;
                dgvZamen.Columns["Shoghl"].HeaderText = "شغل";
                dgvZamen.Columns["Shoghl"].Width = 100;
                dgvZamen.Columns["TelSabet"].HeaderText = "تلفن ثابت";
                dgvZamen.Columns["TelSabet"].Width = 70;
                dgvZamen.Columns["TelHamrah"].HeaderText = "شماره همراه";
                dgvZamen.Columns["TelHamrah"].Width = 70;
                dgvZamen.Columns["DateSabt"].HeaderText = "تاریخ ثبت ";
                dgvZamen.Columns["DateSabt"].Width = 70;
                dgvZamen.Columns["Address"].HeaderText = "آدرس";
                dgvZamen.Columns["Address"].Width = 250;
            }
            catch (Exception)
            {
            }
           
        }
        void DisplayParvande(int year, string bankname)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adb = new SqlDataAdapter();
                adb.SelectCommand = new SqlCommand();
                adb.SelectCommand.Connection = con;
                adb.SelectCommand.CommandText = "select * from UserID=" + UserID + " and tblParvande where Years=" + year + "and  BankName=N'" + bankname + "' ORDER BY ParvandeID desc";
                adb.Fill(ds, "tblParvande");
                dgvParvande.DataSource = ds;
                dgvParvande.DataMember = "tblParvande";
                TitleDisplayParvande();
            }
            catch (Exception)
            {
            }
           
        }
        void DisplayParvande(int year, string bankname,string shobe)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adb = new SqlDataAdapter();
                adb.SelectCommand = new SqlCommand();
                adb.SelectCommand.Connection = con;
                adb.SelectCommand.CommandText = "select * from  UserID=" + UserID + " and tblParvande where Years=" + year + "and  BankName=N'" + bankname + "' and Shobe=N'" + shobe + "' ORDER BY ParvandeID desc";
                adb.Fill(ds, "tblParvande");
                dgvParvande.DataSource = ds;
                dgvParvande.DataMember = "tblParvande";
                TitleDisplayParvande();
            }
            catch (Exception)
            {
            }
            
        }
        int GetReferID()
        {
            int Parvandeid = -1;
            try
            {
               
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                DataTable dt = new DataTable();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from tblParvande ";
                adp.Fill(dt);
                int cunt = dt.Rows.Count;
                Parvandeid = (int)dt.Rows[cunt - 1]["ParvandeID"];
                con.Close();              
            }
            catch (Exception)
            {
                MessageBox.Show("مشکلی در دریافت ای دی پرونده وجود دارد!");
            }
            return Parvandeid;
        }
       /* double GetDarsad(string bank)
        {
            bank = "'" + bank + "'";
            double darsad = 0;
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                DataTable dt = new DataTable();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from tblBank  where BankName=N" + ban;
                adp.Fill(dt);
                int cunt = dt.Rows.Count;
                darsad = Convert.ToDouble((dt.Rows[0]["Darsad"]));
            }
            catch (Exception)
            {
            }
            return darsad;
        }*/
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
                string query = "SELECT  ShobeName FROM [tblShobe]";
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
        private void InsertIntotblMaliParvande(int parvandeId)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "insert into tblMaliParvande(Bed,ParvandeID)values(@Bed,@ParvandeID)";
                cmd.Parameters.AddWithValue("@Bed", Convert.ToInt64(txtMablaghBed.Text.Replace(",", "")));
                cmd.Parameters.AddWithValue("@ParvandeID", parvandeId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در ثبت اطلاعات مالی رخ داده است");
            }       
        }
        private void UpdatetblMaliParvande()
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE tblMaliParvande set Bed=N'" + Convert.ToInt64(txtMablaghBed.Text.Replace(",", "")) + "' where ParvandeID=" + ParvandeID;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
            }
           
        }
        private void DisplayDarsadGhardad(string bankname)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adb = new SqlDataAdapter();
                adb.SelectCommand = new SqlCommand();
                adb.SelectCommand.Connection = con;
                adb.SelectCommand.CommandText = "select * from tblGhardad where BankName=N'" + bankname + "'  ORDER BY Darsad desc";
                adb.Fill(ds, "tblGhardad");
                dgvDarsad.DataSource = ds;
                dgvDarsad.DataMember = "tblGhardad";
                dgvDarsad.Columns["GharardadID"].Visible = false;
                dgvDarsad.Columns["BankName"].Visible = false;
                dgvDarsad.Columns["AzM"].HeaderText = "از مبلغ";
                dgvDarsad.Columns["AzM"].Width = 100;
                dgvDarsad.Columns["TaM"].HeaderText = "تا مبلغ";
                dgvDarsad.Columns["TaM"].Width = 100;
                dgvDarsad.Columns["AzDate"].HeaderText = "از ماه";
                dgvDarsad.Columns["AzDate"].Width = 70;
                dgvDarsad.Columns["TaDate"].HeaderText = "تا ماه";
                dgvDarsad.Columns["TaDate"].Width = 70;
                dgvDarsad.Columns["Darsad"].HeaderText = "درصد";
                dgvDarsad.Columns["Darsad"].Width = 70;
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش نام بانک رخ داده است");
            }
        }
        private void frmParvande_Load(object sender, EventArgs e)
        {           
            txtDate.Text = dt.GetYear(DateTime.Now).ToString() + dt.GetMonth(DateTime.Now).ToString("0#") + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
            //if (Sath==0)
            //{
                DisplayParvande();
                DisplayComboNoBank();
            // DisplayComboShobe();
            //}
            //else
            //{
            //    DisplayParvande(dt.GetYear(DateTime.Now));
            //    DisplayComboNoBank();
            //}
            dgvDarsad.Visible = false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            con.Close();
            #region transaction
            double darsad = Convert.ToDouble(txtDarsad.Text); /*GetDarsad(cmbBankNo.Text);*/
            int years = dt.GetYear(DateTime.Now);
            int month = dt.GetMonth(DateTime.Now);
            if (cmbBankNo.Text == "" || txtName.Text == "" || txtFatherName.Text == "" || txtMablaghBed.Text == "" || txtShTashilat.Text == "" || txtMelli.Text == ""||cmdShobe.Text=="")
            {
                MessageBox.Show("لطفا فیلد های ستاره دار را پر کنید");
            }
            else
            {
                try
                {
                    using (var ts = new System.Transactions.TransactionScope())
                    {
                        double komision = 0;
                        var date = dt.GetYear(DateTime.Now).ToString() + "/" + dt.GetMonth(DateTime.Now).ToString("0#") + "/" + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
                        cmd.Parameters.Clear();
                        cmd.Connection = con;
                        cmd.CommandText = "insert into tblParvande(Shobe,BankName,Name,FatherName,Melli,Shoghl,TelSabet,TelHamrah,ShTashilat,Date,Bed,Address,UserID,DateSabt,Komision,Years,Month,Darsad)values(@Shobe,@BankName,@Name,@FatherName,@Melli,@Shoghl,@TelSabet,@TelHamrah,@ShTashilat,@Date,@Bed,@Address,@UserID,@DateSabt,@komision,@Years,@Month,@Darsad)";
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@BankName", cmbBankNo.Text);
                        cmd.Parameters.AddWithValue("@Shobe", cmdShobe.Text);
                        cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                        cmd.Parameters.AddWithValue("@Melli", txtMelli.Text);
                        cmd.Parameters.AddWithValue("@Shoghl", txtShoghl.Text);
                        cmd.Parameters.AddWithValue("@TelSabet", txtTelSabet.Text);
                        cmd.Parameters.AddWithValue("@TelHamrah", txtTelHamrah.Text);
                        cmd.Parameters.AddWithValue("@ShTashilat", txtShTashilat.Text);
                        cmd.Parameters.AddWithValue("@Date", txtDate.Text);
                        cmd.Parameters.AddWithValue("@Bed", Convert.ToDouble(txtMablaghBed.Text.Replace(",", "")));
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@UserID", UserID);
                        cmd.Parameters.AddWithValue("@DateSabt", date);
                        komision = Convert.ToDouble(((Convert.ToDouble(txtMablaghBed.Text.Replace(",", ""))) * darsad) / 100);
                        cmd.Parameters.AddWithValue("@Komision", komision);
                        cmd.Parameters.AddWithValue("@Years", years);
                        cmd.Parameters.AddWithValue("@Month", month);
                        cmd.Parameters.AddWithValue("@Darsad", darsad);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DisplayParvande();
                        ParvandeID = GetReferID();
                        ////////////////////////////////////////////////////////////////////////
                        InsertIntotblMaliParvande(ParvandeID);
                        ////////////////////////////////////////////////////////////////////////
                        ts.Complete();
                    }
                    MessageBox.Show("ثبت پرونده با موفقیت انجام شد");
                    txtName.Text = "";
                    txtFatherName.Text = "";
                    txtMelli.Text = "";
                    txtShoghl.Text = "";
                    txtTelSabet.Text = "";
                    txtTelHamrah.Text = "";
                    txtMablaghBed.Text = "";
                    txtShTashilat.Text = "";
                    txtAddress.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("مشکلی در ثبت اطلاعات وجود دارد!");
                }
            }
            #endregion
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("آیا مایل به ویرایش رکورد هستتید؟", "هشدار", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                double darsad = Convert.ToDouble(txtDarsad.Text); //GetDarsad(cmbBankNo.Text);

                try
                {
                    using (var ts = new System.Transactions.TransactionScope())
                    {
                        Int64 komision = 0;
                        komision = (Int64)(((Convert.ToInt64(txtMablaghBed.Text.Replace(",", ""))) * darsad) / 100);
                        cmd.Parameters.Clear();
                        cmd.Connection = con;
                        cmd.CommandText = "UPDATE tblParvande set Name=N'" + txtName.Text +
                            "' ,FatherName=N'" + txtFatherName.Text +
                            "' ,BankName=N'" + cmbBankNo.Text +
                            "' ,Melli=N'" + txtMelli.Text +
                            "' ,Shoghl=N'" + txtShoghl.Text +
                            "' ,TelSabet=N'" + txtTelSabet.Text +
                            "' ,TelHamrah=N'" + txtTelHamrah.Text +
                            "' ,ShTashilat=N'" + txtShTashilat.Text +
                            "' ,Date=N'" + txtDate.Text +
                            "' ,Bed=N'" + txtMablaghBed.Text.Replace(",","") +
                            "' ,Address=N'" + txtAddress.Text +
                            "' ,Komision=N'" + komision +
                            "' ,Shobe=N'" + cmdShobe.Text +
                            "' where ParvandeID=" + ParvandeID;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ///////////////////////////////////////////////
                        UpdatetblMaliParvande();
                        ///////////////////////////////////////////////
                        DisplayParvande();
                        ts.Complete();
                    }                   
                        MessageBox.Show("ویرایش اطلاعات پرونده با موفقیت انجام شد.");
                        txtDate.Text = dt.GetYear(DateTime.Now).ToString() + dt.GetMonth(DateTime.Now).ToString("0#") + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
                        txtName.Text = "";
                        txtFatherName.Text = "";
                        txtMelli.Text = "";
                        txtShoghl.Text = "";
                        txtTelSabet.Text = "";
                        txtTelHamrah.Text = "";
                        txtMablaghBed.Text = "";
                        txtShTashilat.Text = "";
                        txtAddress.Text = "";
                        ParvandeID = -1;
                }
                catch (Exception)
                {
                    MessageBox.Show("خطایی در ویرایش پرونده رخ داده است");
                }
            }
        }
        private void dgvParvande_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            con.Close();
            try
            {
                dgvParvande.Rows[e.RowIndex].Selected = true;
                ParvandeID = (int)dgvParvande.Rows[e.RowIndex].Cells["ParvandeID"].Value;
                cmd.Parameters.Clear();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from [tblParvande] where ParvandeID =" + ParvandeID;
                con.Open();
                adp.Fill(dt);
                this.cmbBankNo.Text = dt.Rows[0]["BankName"].ToString();
                this.cmdShobe.Text = dt.Rows[0]["Shobe"].ToString();
                this.txtName.Text = dt.Rows[0]["Name"].ToString();
                this.txtFatherName.Text = dt.Rows[0]["FatherName"].ToString();
                this.txtMelli.Text = dt.Rows[0]["Melli"].ToString();
                this.txtShoghl.Text = dt.Rows[0]["Shoghl"].ToString();
                this.txtTelHamrah.Text = dt.Rows[0]["TelHamrah"].ToString();
                this.txtTelSabet.Text = dt.Rows[0]["TelSabet"].ToString();
                this.txtShTashilat.Text = dt.Rows[0]["ShTashilat"].ToString();
                this.txtAddress.Text = dt.Rows[0]["Address"].ToString();
                this.txtMablaghBed.Text = dt.Rows[0]["Bed"].ToString();
                this.txtDate.Text = dt.Rows[0]["Date"].ToString();
                this.txtDarsad.Text = dt.Rows[0]["Darsad"].ToString();
                DisplayZamen();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در انتخاب رکورد رخ داده است");
            }
        }
        private void buttonX4_Click(object sender, EventArgs e)
        {
            ParvandeID = -1;
            ZamenID = -1;
            txtDate.Text = dt.GetYear(DateTime.Now).ToString() + dt.GetMonth(DateTime.Now).ToString("0#") + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
            txtName.Text = "";
            txtFatherName.Text = "";
            txtMelli.Text = "";
            txtShoghl.Text = "";
            txtTelSabet.Text = "";
            txtTelHamrah.Text = "";
            txtMablaghBed.Text = "";
            txtShTashilat.Text = "";
            txtAddress.Text = "";
        }
        private void btnZSave_Click(object sender, EventArgs e)
        {
            con.Close();
            if (ParvandeID == -1)
            {
                MessageBox.Show("لطفا پرونده ای را ثبت یا از لیست یکی را انتخاب کنید");
            }
            else
            {
                if (txZName.Text == "" || txtZFName.Text == "" || txtZMelli.Text == "")
                {
                    MessageBox.Show("لطفا فیلد های ستاره دار را پر کنید");
                }
                else
                {
                    try
                    {
                        var date = dt.GetYear(DateTime.Now).ToString() + "/" + dt.GetMonth(DateTime.Now).ToString("0#") + "/" + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
                        cmd.Parameters.Clear();
                        cmd.Connection = con;
                        cmd.CommandText = "insert into tblZamen(Name,FatherName,Melli,Shoghl,TelSabet,TelHamrah,DateSabt,Address,ParvandeID)values                                        (@Name,@FatherName,@Melli,@Shoghl,@TelSabet,@TelHamrah,@DateSabt,@Address,@ParvandeID)";
                        cmd.Parameters.AddWithValue("@Name", txZName.Text);
                        cmd.Parameters.AddWithValue("@FatherName", txtZFName.Text);
                        cmd.Parameters.AddWithValue("@Melli", txtZMelli.Text);
                        cmd.Parameters.AddWithValue("@Shoghl", txtZShoghl.Text);
                        cmd.Parameters.AddWithValue("@TelSabet", txtZTSabet.Text);
                        cmd.Parameters.AddWithValue("@TelHamrah", txtZtHamrah.Text);
                        cmd.Parameters.AddWithValue("@DateSabt", date);
                        cmd.Parameters.AddWithValue("@Address", txtZAddress.Text);
                        cmd.Parameters.AddWithValue("@ParvandeID", ParvandeID);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DisplayZamen();
                        MessageBox.Show("ثبت ضامن با موفقیت انجام شد");
                        txZName.Text = "";
                        txtZFName.Text = "";
                        txtZMelli.Text = "";
                        txtZShoghl.Text = "";
                        txtZTSabet.Text = "";
                        txtZtHamrah.Text = "";
                        txtZAddress.Text = "";
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("مشکلی در ثبت اطلاعات وجود دارد!");
                    }
                }
            }
        }
        private void buttonX5_Click(object sender, EventArgs e)
        {
            txZName.Text = "";
            txtZFName.Text = "";
            txtZMelli.Text = "";
            txtZShoghl.Text = "";
            txtZTSabet.Text = "";
            txtZtHamrah.Text = "";
            txtZAddress.Text = "";
        }
        private void btnZEdit_Click(object sender, EventArgs e)
        {
            if (txZName.Text == "" || txtZFName.Text == "" || txtZMelli.Text == "")
            {
                MessageBox.Show("لطفا فیلد های ستاره دار را پر کنید");
            }
            else
            {
                var result = MessageBox.Show("آیا مایل به ویرایش رکورد هستتید؟", "هشدار", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.Connection = con;
                        cmd.CommandText = "UPDATE tblZamen set Name=N'" + txZName.Text +
                            "' ,FatherName=N'" + txtZFName.Text +
                            "' ,Melli=N'" + txtZMelli.Text +
                            "' ,Shoghl=N'" + txtZShoghl.Text +
                            "' ,TelSabet=N'" + txtZTSabet.Text +
                            "' ,TelHamrah=N'" + txtZtHamrah.Text +
                             "' ,Address=N'" + txtZAddress.Text +
                            "' where ZamenID=" + ZamenID;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DisplayZamen();
                        MessageBox.Show("ویرایش اطلاعات ضامن باموفقیت انجام شد.");
                        txtDate.Text = dt.GetYear(DateTime.Now).ToString() +"/"+ dt.GetMonth(DateTime.Now).ToString("0#") +"/"+ dt.GetDayOfMonth(DateTime.Now).ToString("0#");
                        txZName.Text = "";
                        txtZFName.Text = "";
                        txtZMelli.Text = "";
                        txtZShoghl.Text = "";
                        txtZTSabet.Text = "";
                        txtZtHamrah.Text = "";
                        txtZAddress.Text = "";
                        ZamenID = -1;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("خطایی در ثبت رکورد رخ داده است");
                    }
                }
            }             
        }
        private void dgvZamen_CellClick(object sender, DataGridViewCellEventArgs e)
        {        
            try
            {
                dgvZamen.Rows[e.RowIndex].Selected = true;
                ZamenID = (int)dgvZamen.Rows[e.RowIndex].Cells["ZamenID"].Value;
                cmd.Parameters.Clear();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from [tblZamen] where ZamenID =" + ZamenID;
                con.Open();
                adp.Fill(dt);
                this.txZName.Text = dt.Rows[0]["Name"].ToString();
                this.txtZFName.Text = dt.Rows[0]["FatherName"].ToString();
                this.txtZMelli.Text = dt.Rows[0]["Melli"].ToString();
                this.txtZShoghl.Text = dt.Rows[0]["Shoghl"].ToString();
                this.txtZtHamrah.Text = dt.Rows[0]["TelHamrah"].ToString();
                this.txtZTSabet.Text = dt.Rows[0]["TelSabet"].ToString();
                this.txtZAddress.Text = dt.Rows[0]["Address"].ToString();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در انتخاب رکورد رخ داده است");
            }
        }
        private void btnZDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("آیا مایل به حذف ضامن هستتید؟", "هشدار", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                con.Close();
                try
                {
                    cmd.Parameters.Clear();
                    cmd.Connection = con;
                    cmd.CommandText = "Delete from [tblZamen] where ZamenID=@n";
                    cmd.Parameters.AddWithValue("@n", ZamenID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    DisplayZamen();
                    //////////////////////////////////////////////////////////////////
                    MessageBox.Show(" حذف ضامن با موفقیت انجام شد.");
                    txZName.Text = "";
                    txtZFName.Text = "";
                    txtZMelli.Text = "";
                    txtZShoghl.Text = "";
                    txtZTSabet.Text = "";
                    txtZtHamrah.Text = "";
                    txtZAddress.Text = "";
                    ZamenID = -1;
                }
                catch (Exception)
                {
                    MessageBox.Show("مشکلی در حذف اطلاعات فروش رخ دارد!");
                }
            }
        }
        public void btnZDelete_Click(int parvandeId)
        {
                con.Close();
            try
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "Delete from [tblZamen] where ParvandeID=@n";
                cmd.Parameters.AddWithValue("@n", parvandeId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                DisplayZamen();
                txZName.Text = "";
                txtZFName.Text = "";
                txtZMelli.Text = "";
                txtZShoghl.Text = "";
                txtZTSabet.Text = "";
                txtZtHamrah.Text = "";
                txtZAddress.Text = "";
                ZamenID = -1;
            }
            catch (Exception)
            {
                MessageBox.Show("مشکلی در حذف اطلاعات فروش رخ دارد!");
            }
        }
        private void txtMablaghBed_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMablaghBed.Text != string.Empty)
                {
                    txtMablaghBed.Text = string.Format("{0:N0}", double.Parse(txtMablaghBed.Text.Replace(",", "")));
                    txtMablaghBed.Select(txtMablaghBed.TextLength, 0);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("خطایی در درج اطلاعات رخ داده است.");
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
        private void cmbBankNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bankID = GetNoBankID(cmbBankNo.Text);
                DisplayShobe(bankID);
                DisplayParvande((int)dt.GetYear(DateTime.Now), cmbBankNo.Text);
            }
            catch (Exception)
            {

            }
           
        }
        private void dgvDarsad_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDarsad.Rows[e.RowIndex].Selected = true;
            con.Close();
            try
            {
                int gharardadId = (int)dgvDarsad.Rows[e.RowIndex].Cells["GharardadID"].Value;
                cmd.Parameters.Clear();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from [tblGhardad] where GharardadID =" + gharardadId;
                con.Open();
                adp.Fill(dt);
                this.txtDarsad.Text = dt.Rows[0]["Darsad"].ToString();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در انتخاب رکورد رخ داده است");
            }
            dgvDarsad.Visible = false;

        }
        private void btnGharardadLoad_Click(object sender, EventArgs e)
        {
            if (dgvDarsad.Visible==true)
            {              
                dgvDarsad.Visible = false;
            }
            else
            {
                DisplayDarsadGhardad(cmbBankNo.Text);
                dgvDarsad.Visible = true;
            }
        }
        private void dgvDarsad_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvDarsad.Columns["AzM"].Index && e.RowIndex != this.dgvDarsad.NewRowIndex)
            {
                double d = double.Parse(e.Value.ToString());
                e.Value = d.ToString("#,##0.##");
            }
            if (e.ColumnIndex == dgvDarsad.Columns["TaM"].Index && e.RowIndex != this.dgvDarsad.NewRowIndex)
            {
                double d = double.Parse(e.Value.ToString());
                e.Value = d.ToString("#,##0.##");
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("آیا مایل به حذف رکورد هستتید؟", "هشدار", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var ts = new System.Transactions.TransactionScope())
                    {
                        btnZDelete_Click(ParvandeID);
                        cmd.Parameters.Clear();
                        cmd.Connection = con;
                        cmd.CommandText = "Delete from [tblParvande] where ParvandeID=" + ParvandeID;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ///////////////////////////////////////////////
                        UpdatetblMaliParvande();
                        
                        ///////////////////////////////////////////////
                        DisplayParvande();
                        ts.Complete();
                    }

                    MessageBox.Show("حذف  پرونده با موفقیت انجام شد.");
                    txtDate.Text = dt.GetYear(DateTime.Now).ToString() + dt.GetMonth(DateTime.Now).ToString("0#") + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
                    txtName.Text = "";
                    txtFatherName.Text = "";
                    txtMelli.Text = "";
                    txtShoghl.Text = "";
                    txtTelSabet.Text = "";
                    txtTelHamrah.Text = "";
                    txtMablaghBed.Text = "";
                    txtShTashilat.Text = "";
                    txtAddress.Text = "";
                    ParvandeID = -1;
                }
                catch (Exception)
                {
                    MessageBox.Show("خطایی در حذف رکورد رخ داده است");
                }
            }
        }
        private void cmdShobe_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DisplayParvande((int)dt.GetYear(DateTime.Now), cmbBankNo.Text,cmdShobe.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
