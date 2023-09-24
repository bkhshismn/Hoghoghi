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
    public partial class frmParvandeMain : Form
    {
        public frmParvandeMain()
        {
            InitializeComponent();
        }
        string path = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        System.Globalization.PersianCalendar dt = new System.Globalization.PersianCalendar();
        clsGetDataSource gds = new clsGetDataSource();
        int peygirilistID = -1;
        int ghararlistID = -1;
        int pardakhtID = -1;
        int pardakhtid = -1;
        int vaziat = -1;
        int cmbSelectted = 0;
        double GetDarsad(string bank, Int64 mablagh_bed)
        {
            string[] bnk = bank.Split('/');
            bank = "'" + bnk[0] + "'";
            double darsad=0 ;
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                DataTable dt = new DataTable();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select tblGhardad.Darsad,tblGhardad.AzM,tblGhardad.TaM from tblBank inner join [tblGhardad] on tblBank.BankID=tblGhardad.BankID   where tblBank.BankName=N" + bank;
                adp.Fill(dt);
                int cunt = dt.Rows.Count;
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if (mablagh_bed >= Convert.ToInt64(dt.Rows[i]["AzM"]) && mablagh_bed <= Convert.ToInt64(dt.Rows[i]["TaM"]))
                    {
                        darsad = Convert.ToDouble(dt.Rows[i]["Darsad"]);
                    }
                }
            }
            catch (Exception)
            {
            }
            return darsad;
        }
        public int UserID { get; set; }
        public int ParvandeID { get; set; }
        public string[] GrtSMSProfile()
        {
            string[] sing = new string[2];
            try
            {
                con.Close();
                string query = "SELECT  Singature FROM [tblWebService]";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
                
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                sing[0] = dt.Rows[0]["Singature"].ToString();
                sing[1] = dt.Rows[0]["Singature"].ToString();
                //}
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش نام بانک رخ داده است");
            }
            return sing;
        }
        public void DisplayComboNoPeygiri()
        {
            try
            {
                con.Close();
                string query = "SELECT  NoPeygiri FROM [tblNoPeygiri]";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
                cmbNoPygiri.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmbNoPygiri.Items.Add(dt.Rows[i]["NoPeygiri"]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش نام بانک رخ داده است");
            }
        }
        public void DisplayComboNoMakhtome()
        {
            try
            {
                con.Close();
                string query = "SELECT  NoMakhtome FROM [tblNoMakhtome]";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
                cmbNoMakhtome.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmbNoMakhtome.Items.Add(dt.Rows[i]["NoMakhtome"]);
                }
               
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش نام بانک رخ داده است");
            }
        }
        public void DisplayComboNoGharar()
        {
            try
            {
                con.Close();
                string query = "SELECT  NoGHarar FROM [tblNoGHarar]";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
                cmbNoGharar.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmbNoGharar.Items.Add(dt.Rows[i]["NoGHarar"]);
                    //cmbNoGhararPygiri.Items.Add(dt.Rows[i]["NoGHarar"]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش نام بانک رخ داده است");
            }
        }
        public void DisplayComboNoPardakht()
        {
            try
            {
                con.Close();
                string query = "SELECT  NoPardakht FROM [tblNoPardakht]";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
                cmbNoPardakht.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmbNoPardakht.Items.Add(dt.Rows[i]["NoPardakht"]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش نام بانک رخ داده است");
            }
        }
        public void DisplayComboNoSMS()
        {
            try
            {
                con.Close();
                string query = "SELECT  NoSMS FROM [tblNoSMS]";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
                //cmbNoPygiri.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmbNoSms.Items.Add(dt.Rows[i]["NoSMS"]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش نام بانک رخ داده است");
            }
        }
        //-------------------------------------------------
        void DisplayPardakht()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adb = new SqlDataAdapter();
            adb.SelectCommand = new SqlCommand();
            adb.SelectCommand.Connection = con;
            adb.SelectCommand.CommandText = "select * from tblPardakht where ParvandeID="+ParvandeID+" ORDER BY ParvandeID desc";
            adb.Fill(ds, "tblPardakht");
            dgvPardakht.DataSource = ds;
            dgvPardakht.DataMember = "tblPardakht";
            dgvPardakht.Columns["PardakhtID"].HeaderText = "کد ";
            dgvPardakht.Columns["ParvandeID"].Visible = false;
            dgvPardakht.Columns["ParvandeID"].HeaderText = "شماره پرونده";
            dgvPardakht.Columns["BankName"].HeaderText = "نام بانک";
            dgvPardakht.Columns["BankName"].Width = 100;
            dgvPardakht.Columns["NameVarizKonande"].HeaderText = "نام پردات کننده";
            dgvPardakht.Columns["NameVarizKonande"].Width = 100;
            dgvPardakht.Columns["ShomareSanad"].HeaderText = "شماره سند";
            dgvPardakht.Columns["ShomareSanad"].Width = 70;
            dgvPardakht.Columns["Mablagh"].HeaderText = "مبلغ";
            dgvPardakht.Columns["Mablagh"].Width = 150;
            dgvPardakht.Columns["Avarde"].HeaderText = "سهم شرکت";
            dgvPardakht.Columns["Avarde"].Width = 150;
            dgvPardakht.Columns["NoPardakht"].HeaderText = "نوع پرداخت";
            dgvPardakht.Columns["NoPardakht"].Width = 200;
            dgvPardakht.Columns["ShobeBank"].HeaderText = "نام شعبه";
            dgvPardakht.Columns["ShobeBank"].Width = 100;
            dgvPardakht.Columns["Tozihat"].HeaderText = "توضیحات";
            dgvPardakht.Columns["Tozihat"].Width = 500;
            dgvPardakht.Columns["VarizDate"].HeaderText = "تاریخ واریز ";
            dgvPardakht.Columns["VarizDate"].Width = 80;
            dgvPardakht.Columns["DateTimeSabt"].HeaderText = "تاریخ ثبت ";
            dgvPardakht.Columns["DateTimeSabt"].Width = 80;
            dgvPardakht.Columns["Years"].Visible = false;
            dgvPardakht.Columns["Month"].Visible = false;
        }
        //-------------------------------------------------
        void DisplayParvande()
        {
            con.Close();
            cmd.Parameters.Clear();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from tblParvande where ParvandeID=" + ParvandeID;
            con.Open();
            adp.Fill(dt);
            lblName.Text = dt.Rows[0]["Name"].ToString();
            txtSMSName.Text = dt.Rows[0]["Name"].ToString();
            lblNBank.Text = dt.Rows[0]["BankName"].ToString()+"/" + dt.Rows[0]["Shobe"].ToString();
            lblSDate.Text = dt.Rows[0]["DateSabt"].ToString();
            lblDate.Text = dt.Rows[0]["Date"].ToString();
            lblMelli.Text = dt.Rows[0]["Melli"].ToString();
            lblShParvande.Text = ParvandeID.ToString();
            lblTashilat.Text = dt.Rows[0]["ShTashilat"].ToString();
            txtTashilat.Text = dt.Rows[0]["ShTashilat"].ToString();
            lblBed.Text = Convert.ToInt64(dt.Rows[0]["Bed"]).ToString("N0");
            txtHamrah.Text = dt.Rows[0]["TelHamrah"].ToString();
            txtSMSNumber.Text = dt.Rows[0]["TelHamrah"].ToString();
            txtSabet.Text = dt.Rows[0]["TelSabet"].ToString();
            con.Close();
     
        }
        void DisplayZamen(int parvandeID)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adb = new SqlDataAdapter();
            adb.SelectCommand = new SqlCommand();
            adb.SelectCommand.Connection = con;
            adb.SelectCommand.CommandText = "select * from tblZamen where ParvandeID=" + parvandeID + " ORDER BY ZamenID desc";
            adb.Fill(ds, "tblZamen");
            dgvZamen.DataSource = ds;
            dgvZamen.DataMember = "tblZamen";
            dgvZamen.Columns["ZamenID"].HeaderText = "کد ضامن";
            dgvZamen.Columns["ZamenID"].Width = 100;
            dgvZamen.Columns["ZamenID"].Visible = false;
            dgvZamen.Columns["ParvandeID"].HeaderText = "شماره پرونده";
            dgvZamen.Columns["ParvandeID"].Visible = false;
            dgvZamen.Columns["Name"].HeaderText = "نام ضامن";
            dgvZamen.Columns["Name"].Width = 120;
            dgvZamen.Columns["FatherName"].HeaderText = "نام پدر";
            dgvZamen.Columns["FatherName"].Width = 100;
            dgvZamen.Columns["FatherName"].Visible = false;
            dgvZamen.Columns["Melli"].HeaderText = "شماره ملی";
            dgvZamen.Columns["Melli"].Width = 80;
            dgvZamen.Columns["Shoghl"].HeaderText = "شغل";
            dgvZamen.Columns["Shoghl"].Width = 100;
            dgvZamen.Columns["TelSabet"].HeaderText = "تلفن ثابت";
            dgvZamen.Columns["TelSabet"].Width = 80;
            dgvZamen.Columns["TelHamrah"].HeaderText = "شماره همراه";
            dgvZamen.Columns["TelHamrah"].Width = 80;
            dgvZamen.Columns["DateSabt"].HeaderText = "تاریخ ثبت ";
            dgvZamen.Columns["DateSabt"].Width = 70;
            dgvZamen.Columns["Address"].HeaderText = "آدرس";
            dgvZamen.Columns["Address"].Width = 300;
        }
        void DisplayListPeygiri()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adb = new SqlDataAdapter();
            adb.SelectCommand = new SqlCommand();
            adb.SelectCommand.Connection = con;
            adb.SelectCommand.CommandText = "select * from tblListPeygiri where ParvandeID=" + ParvandeID + " ORDER BY ParvandeID desc";
            adb.Fill(ds, "tblListPeygiri");
            dgvPeygiri.DataSource = ds;
            dgvPeygiri.DataMember = "tblListPeygiri";
            dgvPeygiri.Columns["ListPeygiriID"].Visible = false;
            dgvPeygiri.Columns["ParvandeID"].HeaderText = "شماره پرونده";
            dgvPeygiri.Columns["ParvandeID"].Visible = false;
            dgvPeygiri.Columns["NoPeygiri"].HeaderText = "نوع پیگیری";
            dgvPeygiri.Columns["NoPeygiri"].Width = 200;
            dgvPeygiri.Columns["Matn"].HeaderText = "متن";
            dgvPeygiri.Columns["Matn"].Width = 500;
            dgvPeygiri.Columns["DateTimeSabt"].HeaderText = "تاریخ ثبت ";
            dgvPeygiri.Columns["DateTimeSabt"].Width = 70;
        }
        void DisplayListGharar()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adb = new SqlDataAdapter();
            adb.SelectCommand = new SqlCommand();
            adb.SelectCommand.Connection = con;
            adb.SelectCommand.CommandText = "select * from tblGharar where ParvandeID=" + ParvandeID + " ORDER BY ParvandeID desc";
            adb.Fill(ds, "tblGharar");
            dgvGharar.DataSource = ds;
            dgvGharar.DataMember = "tblGharar";
            dgvGharar.Columns["GhararID"].Visible = false;
            dgvGharar.Columns["ParvandeID"].HeaderText = "شماره پرونده";
            dgvGharar.Columns["ParvandeID"].Visible = false;
            dgvGharar.Columns["NoGharar"].HeaderText = "نوع پیگیری";
            dgvGharar.Columns["NoGharar"].Width = 200;
            dgvGharar.Columns["Matn"].HeaderText = "متن";
            dgvGharar.Columns["Matn"].Width = 500;
            dgvGharar.Columns["DateGharar"].HeaderText = "تاریخ قرار ";
            dgvGharar.Columns["DateGharar"].Width = 70;
            dgvGharar.Columns["DateTimeSabt"].HeaderText = "تاریخ ثبت ";
            dgvGharar.Columns["DateTimeSabt"].Width = 70;
        }
        void DisplayListSMS()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adb = new SqlDataAdapter();
            adb.SelectCommand = new SqlCommand();
            adb.SelectCommand.Connection = con;
            adb.SelectCommand.CommandText = "select * from tblSMS where ParvandeID=" + ParvandeID + " ORDER BY ParvandeID desc";
            adb.Fill(ds, "tblSMS");
            dgvSMS.DataSource = ds;
            dgvSMS.DataMember = "tblSMS";
            dgvSMS.Columns["SMSID"].Visible = false;
            dgvSMS.Columns["SMSNumber"].HeaderText = "شماره همراه";
            dgvSMS.Columns["ParvandeID"].Visible = false;
            dgvSMS.Columns["SMSNo"].HeaderText = "نوع پیام";
            dgvSMS.Columns["SMSNo"].Width = 200;
            dgvSMS.Columns["SMSMatn"].HeaderText = "متن";
            dgvSMS.Columns["SMSMatn"].Width = 500;
            dgvSMS.Columns["SMSDate"].HeaderText = "تاریخ ارسال ";
            dgvSMS.Columns["SMSDate"].Width = 70;
            dgvSMS.Columns["SMSName"].HeaderText = "تاریخ ثبت ";
            dgvSMS.Columns["SMSName"].Width = 120;
            dgvSMS.Columns["SMSTime"].HeaderText = "ساعت ارسال ";
            dgvSMS.Columns["SMSTime"].Width = 70;
        }
        int GetReferIDtblPardakht()
        {
            int pardakhtID = -1;
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from tblPardakht ";
            adp.Fill(dt);
            int cunt = dt.Rows.Count;
            pardakhtID = (int)dt.Rows[cunt - 1]["PardakhtID"];
            return pardakhtID;
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
                adp.SelectCommand.CommandText = "select * from tblParvande where ParvandeID=" + ParvandeID;
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
        Int64 GetPardakht()
        {
            int bed = 0;
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter();
                DataTable dt = new DataTable();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from tblPardakht where ParvandeID=" + ParvandeID;
                adp.Fill(dt);
                int cunt = dt.Rows.Count;
                for (int i = 0; i <= cunt - 1; i++)
                {
                    bed += (int)dt.Rows[i]["Mablagh"];
                }
            }
            catch (Exception)
            {
                MessageBox.Show("مشکلی در دریافت اطلاعات میزان پرداختی رخ داده است");
            }
            
          
            
            return bed;
        }
        void DisplayMali()
        {
            Int64 bed = GetBedehkari();
            Int64 par = GetPardakht();
            lblPardakht.Text = par.ToString("N0");
            lblMande.Text = (bed - par).ToString("N0");
        }
        void DeleteMaliPardakht(int PardakhtID)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "delete from tblMaliParvande where PardakhtID=" + PardakhtID;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("خطایی در حذف اطلاعات مالی پرداخت رخ داده است.");
            }
        }
        private void InsertIntotblMaliParvande(int ParvandeID, string date , int pardakhtID,Int64 avarde)
        {
            int years = dt.GetYear(DateTime.Now);
            int month = dt.GetMonth(DateTime.Now);
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "insert into tblMaliParvande(Bes,ParvandeID,PardakhtDate,Month,Years,PardakhtID,Avarde)values(@Bes,@ParvandeID,@PardakhtDate,@Month,@Years,@PardakhtID,@Avarde)";
            cmd.Parameters.AddWithValue("@Bes", Convert.ToInt64(txtMablaghVariz.Text.Replace(",", "")));
            cmd.Parameters.AddWithValue("@ParvandeID", ParvandeID);
            cmd.Parameters.AddWithValue("@PardakhtDate", date);
            cmd.Parameters.AddWithValue("@Month", month);
            cmd.Parameters.AddWithValue("@Years", years);
            cmd.Parameters.AddWithValue("@PardakhtID", pardakhtID);
            cmd.Parameters.AddWithValue("@Avarde", avarde);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private void UpdateToMaliParvande(int pardakhtID)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE tblMaliParvande set Bes=N'" + Convert.ToDouble(txtMablaghVariz.Text.Replace(",", "")) +
                    "' where PardakhtID=" + pardakhtID;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("مشکلی در ویرایش  اطلاعات tblMaliPardakht وجود دارد!");
            }
        }
        private void frmParvandeMain_Load(object sender, EventArgs e)
        {
            txtDate.Text = dt.GetYear(DateTime.Now).ToString() + dt.GetMonth(DateTime.Now).ToString("0#") + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
            txtVarizDate.Text = dt.GetYear(DateTime.Now).ToString() + dt.GetMonth(DateTime.Now).ToString("0#") + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
            DisplayZamen(ParvandeID);
            DisplayParvande();
            DisplayComboNoPeygiri();
            DisplayComboNoGharar();
            DisplayListPeygiri();
            DisplayListGharar();
            DisplayComboNoPardakht();
            DisplayPardakht();
            DisplayComboNoMakhtome();
            DisplayComboNoSMS();
            DisplayListSMS();
            DisplayMali();         
        }
        private void buttonX11_Click(object sender, EventArgs e)
        {
            txtGharar.Text = "";
        }
        private void buttonX12_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region Peygiri
        private void btnPygiriSave_Click(object sender, EventArgs e)
        {

            if (cmbNoPygiri.Text == "" )
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
                    cmd.CommandText = "insert into tblListPeygiri(NoPeygiri,Matn,DateTimeSabt,ParvandeID)values                                        (@NoPeygiri,@Matn,@DateTimeSabt,@ParvandeID)";
                    cmd.Parameters.AddWithValue("@NoPeygiri", cmbNoPygiri.Text);
                    cmd.Parameters.AddWithValue("@Matn", txtTPygiri.Text);
                    cmd.Parameters.AddWithValue("@DateTimeSabt", date);
                    cmd.Parameters.AddWithValue("@ParvandeID", ParvandeID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    DisplayListPeygiri();
                    txtGharar.Text = txtTPygiri.Text;
                    MessageBox.Show("ثبت پیگیری با موفقیت انجام شد");

                }
                catch (Exception)
                {
                    MessageBox.Show("مشکلی در ثبت اطلاعات وجود دارد!");
                }
            }
        }
        private void btnPygiriEdit_Click(object sender, EventArgs e)
        {
            if (peygirilistID==-1)
            {
                MessageBox.Show("لطفا یک رکورد راانتخاب کنید ");
            }
            else
            {
                if (cmbNoPygiri.Text == "" || txtTPygiri.Text == "")
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
                            cmd.CommandText = "UPDATE tblListPeygiri set NoPeygiri=N'" + cmbNoPygiri.Text +
                                "' ,Matn=N'" + txtTPygiri.Text +
                                "' where ListPeygiriID=" + peygirilistID;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            DisplayComboNoPeygiri();
                            DisplayListPeygiri();
                            MessageBox.Show("ویرایش اطلاعات پیگیری باموفقیت انجام شد.");
                            txtTPygiri.Text = "";
                            peygirilistID = -1;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("خطایی در ثبت رکورد رخ داده است");
                        }
                    }
                }
            }         
        }
        private void dgvPeygiri_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                peygirilistID = (int)dgvPeygiri.Rows[e.RowIndex].Cells["ListPeygiriID"].Value;
                cmd.Parameters.Clear();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from [tblListPeygiri] where ListPeygiriID =" + peygirilistID;
                con.Open();
                adp.Fill(dt);
                this.txtTPygiri.Text = dt.Rows[0]["Matn"].ToString();
                this.cmbNoPygiri.Text = dt.Rows[0]["NoPeygiri"].ToString();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در انتخاب رکورد رخ داده است");
            }
        }
        private void btnPygiriDelete_Click(object sender, EventArgs e)
        {
            if (peygirilistID == -1)
            {
                MessageBox.Show("لطفا یک رکورد راانتخاب کنید ");
            }
            else
            {
                var result = MessageBox.Show("آیا مایل به حذف رکورد هستتید؟", "هشدار", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    con.Close();
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.Connection = con;
                        cmd.CommandText = "Delete from [tblListPeygiri] where ListPeygiriID=@n";
                        cmd.Parameters.AddWithValue("@n", peygirilistID);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DisplayListPeygiri();
                        ///////////////////////////////////////////////////////////////////
                        //////////////////////////////////////////////////////////////////
                        MessageBox.Show("عملیات حذف با موفقیت انجام شد.");
                        txtTPygiri.Text = "";
                        peygirilistID = -1;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("مشکلی در حذف اطلاعات پیگیری رخ دارد!");
                    }
                }
            }
           
        }
        private void buttonX22_Click(object sender, EventArgs e)
        {
            txtTPygiri.Text = "";
        }
        #endregion
        #region Gharar
        private void btnSaveGharar_Click(object sender, EventArgs e)
        {
            if (cmbNoGharar.Text == "")
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
                    cmd.CommandText = "insert into tblGharar(NoGharar,Matn,DateTimeSabt,ParvandeID,DateGharar)values                                        (@NoGharar,@Matn,@DateTimeSabt,@ParvandeID,@DateGharar)";
                    cmd.Parameters.AddWithValue("@NoGharar", cmbNoGharar.Text);
                    cmd.Parameters.AddWithValue("@Matn", txtGharar.Text);
                    cmd.Parameters.AddWithValue("@DateTimeSabt", date);
                    cmd.Parameters.AddWithValue("@DateGharar", txtDate.Text);
                    cmd.Parameters.AddWithValue("@ParvandeID", ParvandeID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    DisplayListGharar();
                    MessageBox.Show("ثبت قرار با موفقیت انجام شد");
                }
                catch (Exception)
                {
                    MessageBox.Show("مشکلی در ثبت اطلاعات وجود دارد!");
                }
            }
        }
        private void btnEditGharar_Click(object sender, EventArgs e)
        {
            if (ghararlistID == -1)
            {
                MessageBox.Show("لطفا یک رکورد راانتخاب کنید ");
            }
            else
            {
                if (cmbNoGharar.Text == "" || txtGharar.Text == "")
                {
                    MessageBox.Show("لطفا فیلد های ستاره دار و متن را پر کنید");
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
                            cmd.CommandText = "UPDATE tblGharar set NoGharar=N'" + cmbNoGharar.Text +
                                "' ,Matn=N'" + txtGharar.Text +
                                "' ,DateGharar=N'" + txtDate.Text +
                                "' where GhararID=" + ghararlistID;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            DisplayComboNoPeygiri();
                            DisplayListGharar();
                            MessageBox.Show("ویرایش اطلاعات قرار باموفقیت انجام شد.");
                            txtTPygiri.Text = "";
                            peygirilistID = -1;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("خطایی در ثبت رکورد رخ داده است");
                        }
                    }
                }
            }
        }
        private void btnDeleteGharar_Click(object sender, EventArgs e)
        {
            if (ghararlistID == -1)
            {
                MessageBox.Show("لطفا یک رکورد راانتخاب کنید ");
            }
            else
            {
                var result = MessageBox.Show("آیا مایل به حذف رکورد هستتید؟", "هشدار", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    con.Close();
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.Connection = con;
                        cmd.CommandText = "Delete from [tblGharar] where GhararID=@n";
                        cmd.Parameters.AddWithValue("@n", ghararlistID);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DisplayListGharar();
                        MessageBox.Show("عملیات حذف با موفقیت انجام شد.");
                        txtGharar.Text = "";
                        ghararlistID = -1;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("مشکلی در حذف اطلاعات قرار رخ دارد!");
                    }
                }
            }
        }        
        private void dgvGharar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ghararlistID = (int)dgvGharar.Rows[e.RowIndex].Cells["GhararID"].Value;
                cmd.Parameters.Clear();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from [tblGharar] where GhararID =" + ghararlistID;
                con.Open();
                adp.Fill(dt);
                this.txtGharar.Text = dt.Rows[0]["Matn"].ToString();
                this.txtDate.Text = dt.Rows[0]["DateGharar"].ToString();
                this.cmbNoGharar.Text = dt.Rows[0]["NoGharar"].ToString();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در انتخاب رکورد رخ داده است");
            }
        }
        #endregion
        private void btnZSave_Click(object sender, EventArgs e)
        {
            Int64 avarde = 0;
            double darsad = 0;
            string[] date1 = txtVarizDate.Text.Split('/');
            int years =int.Parse( date1[0]);
            int month =int.Parse( date1[1]);
            darsad = GetDarsad(lblNBank.Text, Int64.Parse( lblBed.Text.Replace(",","")));

            if (cmbNoPardakht.Text == "" || txtNameBank.Text == "" || txtMablaghVariz.Text == "" || txtNamePardakhkonande.Text == "" || txtDate.Text == "")
            {
                MessageBox.Show("لطفا فیلد های ستاره دار را پر کنید");
            }
            else
            {
                try
                {
                    using (var ts = new System.Transactions.TransactionScope())
                    {
                        var date = dt.GetYear(DateTime.Now).ToString() + "/" + dt.GetMonth(DateTime.Now).ToString("0#") + "/" + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
                        avarde = (Int64)(((Convert.ToInt64(txtMablaghVariz.Text.Replace(",", ""))) * darsad) / 100);
                        cmd.Parameters.Clear();
                        cmd.Connection = con;
                        cmd.CommandText = "insert into tblPardakht(ParvandeID,VarizDate,ShomareSanad,BankName,Mablagh,NameVarizKonande,NoPardakht,ShobeBank,Tozihat,DateTimeSabt,Avarde,Years,Month)values(@ParvandeID,@VarizDate,@ShomareSanad,@BankName,@Mablagh,@NameVarizKonande,@NoPardakht,@ShobeBank,@Tozihat,@DateTimeSabt,@Avarde,@Years,@Month)";
                        cmd.Parameters.AddWithValue("@ParvandeID", ParvandeID);
                        cmd.Parameters.AddWithValue("@VarizDate", txtVarizDate.Text);
                        cmd.Parameters.AddWithValue("@ShomareSanad", txtShomareSanad.Text);
                        cmd.Parameters.AddWithValue("@BankName", txtNameBank.Text);
                        cmd.Parameters.AddWithValue("@Mablagh", Convert.ToDouble(txtMablaghVariz.Text.Replace(",", "")));
                        cmd.Parameters.AddWithValue("@NameVarizKonande", txtNamePardakhkonande.Text);
                        cmd.Parameters.AddWithValue("@NoPardakht", cmbNoPardakht.Text);
                        cmd.Parameters.AddWithValue("@ShobeBank", txtShobeBank.Text);
                        cmd.Parameters.AddWithValue("@Tozihat", txtTozihat.Text);
                        cmd.Parameters.AddWithValue("@DateTimeSabt", date);
                        cmd.Parameters.AddWithValue("@Avarde", avarde);
                        cmd.Parameters.AddWithValue("@Years", years);
                        cmd.Parameters.AddWithValue("@Month", month);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DisplayPardakht();
                        pardakhtID = GetReferIDtblPardakht();
                        ////////////////////////////////////////////////////////////////////////
                        InsertIntotblMaliParvande(ParvandeID, date, pardakhtID, avarde);
                        DisplayMali();
                        ////////////////////////////////////////////////////////////////////////
                        ts.Complete();
                    }

                    MessageBox.Show("ثبت پرونده با موفقیت انجام شد");
                    txtNamePardakhkonande.Text = "";
                    txtShobeBank.Text = "";
                    txtNameBank.Text = "";
                    txtShomareSanad.Text = "";
                    txtMablaghVariz.Text = "";
                    txtTozihat.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("مشکلی در ثبت اطلاعات وجود دارد!");
                }
            }
        }
        private void btnzBack_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void btnMakhtome_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("آیا مایل به مختومه کردن پرونده هستتید؟", "هشدار", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string selValue = (string)cmbNoMakhtome.SelectedValue;
                if (cmbSelectted==0 || cmbNoMakhtome.Text=="")
                {
                    MessageBox.Show("لطفا نوع مختومه راانتخاب کنید."); 
                }
                else
                {
                    string datemakhtome = dt.GetYear(DateTime.Now).ToString() + "/" + dt.GetMonth(DateTime.Now).ToString("0#") + "/" + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.Connection = con;
                        cmd.CommandText = "UPDATE tblParvande set Vaziat=N'" + 0 + "',DateMakhtome=N'" + datemakhtome + "',NoMakhtome=N'" + cmbNoMakhtome.Text + "' where ParvandeID=" + ParvandeID;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        MessageBox.Show("پرونده با موفقیت مختومه شد.");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("خطایی در ثبت رکورد رخ داده است");
                    }
                }
              
            }
        }
        private void cmbNoMakhtome_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSelectted = 1;
        }
        private void frmParvandeMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            var frm = Application.OpenForms.Cast<Form>().Where(x => x.Name == "frmViewParvande").FirstOrDefault();
            if (null != frm)
            {
                frmViewParvande master = (frmViewParvande)Application.OpenForms["frmViewParvande"];
                master.btnRefresh.PerformClick();
            }
        }
        private void tbExit_Click(object sender, EventArgs e)
        {

        }
        private void btnSMSSave_Click(object sender, EventArgs e)
        {
            string[] sing = GrtSMSProfile();
            if (cmbNoSms.Text == "" || txtSMSNumber.Text == "" || txtSMSName.Text == "" )
            {
                MessageBox.Show("لطفا فیلد های ستاره دار را پر کنید");
            }
            else
            {
                try
                {
                    using (var ts = new System.Transactions.TransactionScope())
                    {
                        string[] _to = new string[1];
                        string _signature = sing[0];
                        string _from = sing[1];
                         _to [0]= txtSMSNumber.Text;
                        string _text = cmbNoSms.Text + "\n" + txtSMSMatn.Text;
                        bool _isflash = false;
                        string _udh = String.Empty;
                        var _ApiSMS = new PARSGREEN.API_SendSMS.SendSMS();
                        int _result = _ApiSMS.SendGroupSmsSimple(_signature, _from, _to, _text, _isflash, _udh);
                        //lblResult.Text = String.Format("Your Result Code Is :{0}", _result);

                        //-------------------------------------------------------------------
                        var date = dt.GetYear(DateTime.Now).ToString() + "/" + dt.GetMonth(DateTime.Now).ToString("0#") + "/" + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
                        var time= DateTime.Now.ToString("HH:mm:ss tt");
                        cmd.Parameters.Clear();
                        cmd.Connection = con;
                        cmd.CommandText = "insert into tblSMS(ParvandeID,SMSName,SMSNo,SMSNumber,SMSMatn,SMSDate,SMSTime)values(@ParvandeID,@SMSName,@SMSNo,@SMSNumber,@SMSMatn,@SMSDate,@SMSTime)";
                        cmd.Parameters.AddWithValue("@ParvandeID", ParvandeID);
                        cmd.Parameters.AddWithValue("@SMSName", txtSMSName.Text);
                        cmd.Parameters.AddWithValue("@SMSNo", cmbNoSms.Text);
                        cmd.Parameters.AddWithValue("@SMSNumber", txtSMSNumber.Text);
                        cmd.Parameters.AddWithValue("@SMSMatn", cmbNoSms.Text + "\n" + txtSMSMatn.Text);
                        cmd.Parameters.AddWithValue("@SMSDate", date);
                        cmd.Parameters.AddWithValue("@SMSTime", time);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DisplayListSMS();
                        ts.Complete();
                    }
                    MessageBox.Show("پیام با موفقیت ارسال شد");
                }
                catch (Exception)
                {
                    MessageBox.Show("مشکلی در ثبت اطلاعات وجود دارد!");
                }
            }
        }
        private void btnEblagh_Click(object sender, EventArgs e)
        {
            string text = "آقای/خانم (...)به استحضار میرساند برابر پیگیری های بعمل آمده در خصوص پرونده شما نزد اجرای ثبت بانک (...)و شعبه (...)و به شماره تسهیلات(...)، با توجه به ابلاغ اخطار جهت پرداخت تسهیلات، در صورت عدم پیگیری در موعد مقرر منجر به توقیف اموال شما و ضامنین خواهد شد. مقتضی است در اسرع وقت به اداره اجرای ثبت شهرستان مربوطه مندرج در برگه ابلاغ مراجعه نمایید و همچنین در اسرع وقت در ساعت اداری با موسسه حقوقی و داوری شکوه عدل زرین به شماره (...) تماس حاصل فرمایید.";
            txtSMSMatn.Text = text;
        }
        private void btnEkhtar1_Click(object sender, EventArgs e)
        {
            string text = "با سلام آقای/خانم  (...) پیگیری مکرر نتیجه موثری بابت وصول اقساط معوق بانک (...)  را در پی نداشته است. لذا پرونده در مرحله اجرای عملیات قانونی قرار میگیرد. لطفاً در اسرع وقت در ساعت اداری با موسسه حقوقی و داوری  شکوه عدل زرین به شماره (...) تماس حاصل فرمایید.";
            txtSMSMatn.Text = text;
        }
        private void btnEkhtar2_Click(object sender, EventArgs e)
        {
            string text = "با سلام آقای/خانم  (...)  به استحضارتان می رسانیم که پیگرد قانونی به جهت عدم پرداخت بدهی معوقه به بانک مسکن با توجه به اخطارهای قبلی آغاز شده است. لطفاً در اسرع وقت در ساعت اداری با موسسه حقوقی و داوری شکوه عدل زرین به شماره (...) تماس حاصل فرمایید.";
            txtSMSMatn.Text = text;
        }
        private void btnEkhtar3_Click(object sender, EventArgs e)
        {
            string text = "با سلام آقای/خانم  (...) عدم توجه به هشدارها باعث محرومیت از خدمات بانک های دولتی و خصوصی برای جنابعالی میگردد. لطفاً در اسرع وقت در ساعت اداری با موسسه حقوقی و داوری شکوه عدل زرین به شماره (...) تماس حاصل فرمایید.";
            txtSMSMatn.Text = text;
        }
        private void buttonX26_Click(object sender, EventArgs e)
        {
            string text = "با سلام آقای/خانم  (...)  پیگیری مکرر نتیجه موثری بابت وصول اقساط معوق به بانک (...)  را در پی نداشته است. لذا پرونده در مرحله اجرای عملیات قانونی قرار میگیرد. لطفاً در اسرع وقت در ساعت اداری با موسسه حقوقی و داوری عدالت شکوه عدل زرین به شماره (...) تماس حاصل فرمایید.";
            txtSMSMatn.Text = text;
        }
        private void buttonX42_Click(object sender, EventArgs e)
        {
            string text = "با سلام آقای/خانم  (...)  تعهد شما را در مورد سررسید تاریخ چک یادآوری می کند. لطفاً در اسرع وقت در ساعت اداری با موسسه حقوقی و داوری عدالت شکوه عدل زرین به شماره (...) تماس حاصل فرمایید.";
            txtSMSMatn.Text = text;
        }
        private void buttonX41_Click(object sender, EventArgs e)
        {
            string text = "با سلام آقای/خانم  (...)  لطفاً در اسرع وقت در ساعت اداری با موسسه حقوقی و داوری شکوه عدل زرین به شماره (...) تماس حاصل فرمایید";
            txtSMSMatn.Text = text;
        }
        private void buttonX40_Click(object sender, EventArgs e)
        {
            string text = "با سلام آقای/خانم  (...)  عدم توجه به هشدارها باعث صدور حکم توقیف خودروی شما شده است. لطفاً در اسرع وقت در ساعت اداری با موسسه حقوقی و داوری شکوه عدل زرین به شماره (...) تماس حاصل فرمایید.";
            txtSMSMatn.Text = text;
        }
        private void buttonX39_Click(object sender, EventArgs e)
        {
            string text = "با سلام آقای/خانم  (...)  انتظار می رود با مساعدت شما اقساط وام بدون نیاز به پیگرد قانونی مسترد گردد. لطفاً در اسرع وقت در ساعت اداری با موسسه حقوقی و داوری شکوه عدل زرین به شماره (...) تماس حاصل فرمایید.";
            txtSMSMatn.Text = text;
        }
        private void buttonX38_Click(object sender, EventArgs e)
        {
            string text = "با سلام آقای/خانم  (...)  لطفاً در مورد ضمانت پرونده وام معوقه بانک (...)ریعاً با موسسه حقوقی و شکوه عدل زرین به شماره (...) تماس حاصل فرمایید.";
            txtSMSMatn.Text = text;
        }
        private void buttonX31_Click(object sender, EventArgs e)
        {
            string text = "با سلام آقای/خانم  (...) نظر به عدم انجام تعهدات در پرداخت اقساط و دیون، وثایق شما نزد بانک (...)   در مرحله اجرای عملیات قانونی قرار خواهد گرفت. لطفاً در اسرع وقت در ساعت اداری با موسسه حقوقی و داوری شکوه عدل زرین به شماره (...) تماس حاصل فرمایید.";
            txtSMSMatn.Text = text;
        }
        private void buttonX37_Click(object sender, EventArgs e)
        {
            string text = "با سلام آقای/خانم  (...) نظر به عدم توجه به اخطاریه های صادره، اقدامات لازم بمنظور اعمال نامه کسر از حقوق شما و ضامنین در حال انجام می باشد. لطفاً در اسرع وقت در ساعت اداری با موسسه حقوقی و داوری شکوه عدل زرین به شماره (...) تماس حاصل فرمایید.";
            txtSMSMatn.Text = text;
        }
        private void buttonX43_Click(object sender, EventArgs e)
        {
            string text = "با سلام آقای/خانم  (...) تعهد شما را در مورد پرونده بدهی معوقه را یادآوری می کند. لطفاً در اسرع وقت در ساعت اداری با موسسه حقوقی و داوری شکوه عدل زرین به شماره (...) تماس حاصل فرمایید.";
            txtSMSMatn.Text = text;
        }
        private void buttonX30_Click(object sender, EventArgs e)
        {
            string text = "با سلام آقای/خانم  (...)  مشتری گرامی بانک (...)بدینوسیله پرداخت قسط تسهیلات شماره (...)، شعبه (...) استان مازندران یادآوری میگردد.";
            txtSMSMatn.Text = text;
        }
        private void buttonX29_Click(object sender, EventArgs e)
        {
            string text = "با سلام آقای/خانم  (...)  ضمن یادآوری جلسه فیمابین، حضور در موسسه حقوقی و داوری داوری شکوه عدل زرین بمنظور مذاکره پیرامون بازپرداخت دیون معوقه اعلام میگردد. لطفاً در اسرع وقت در ساعت اداری با شماره(...) تماس حاصل فرمایید.";
            txtSMSMatn.Text = text;
        }
        private void buttonX28_Click(object sender, EventArgs e)
        {
            frmReport frmReportInput = new frmReport();
            SqlDataAdapter da = new SqlDataAdapter("select * from tblSMS where ParvandeID='" + ParvandeID + "' ", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "tblSMS");
            ReporSMSrpt rptInput = new ReporSMSrpt();
            rptInput.SetDataSource(ds);
            frmReportInput.crystalReportViewer1.ReportSource = rptInput;
            frmReportInput.ShowDialog();
        }
        private void labelX36_Click(object sender, EventArgs e)
        {

        }
        private void dgvPardakht_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                pardakhtid = (int)dgvPardakht.Rows[e.RowIndex].Cells["PardakhtID"].Value;
                cmd.Parameters.Clear();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from [tblPardakht] where PardakhtID =" + pardakhtid;
                con.Open();
                adp.Fill(dt);
                this.txtNameBank.Text = dt.Rows[0]["BankName"].ToString();
                this.txtNamePardakhkonande.Text = dt.Rows[0]["NameVarizKonande"].ToString();
                this.txtMablaghVariz.Text = dt.Rows[0]["Mablagh"].ToString();
                this.txtShomareSanad.Text = dt.Rows[0]["ShomareSanad"].ToString();
                this.cmbNoPardakht.Text = dt.Rows[0]["NoPardakht"].ToString();
                this.txtShobeBank.Text = dt.Rows[0]["ShobeBank"].ToString();
                this.txtVarizDate.Text = dt.Rows[0]["VarizDate"].ToString();
                this.txtTozihat.Text = dt.Rows[0]["Tozihat"].ToString();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در انتخاب رکورد رخ داده است");
            }
        }
        private void btnZEdit_Click(object sender, EventArgs e)
        {
            if (pardakhtid == -1)
            {
                MessageBox.Show("لطفا یک رکورد راانتخاب کنید ");
            }
            else
            {
                if (cmbNoPardakht.Text == "" || txtNameBank.Text == "" || txtMablaghVariz.Text == "" || txtNamePardakhkonande.Text == "" || txtDate.Text == "")
                {
                    MessageBox.Show("لطفا فیلد های ستاره دار و متن را پر کنید");
                }
                else
                {
                    var result = MessageBox.Show("آیا مایل به ویرایش رکورد هستتید؟", "هشدار", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            using (var ts = new System.Transactions.TransactionScope())
                            {
                                cmd.Parameters.Clear();
                                cmd.Connection = con;
                                cmd.CommandText = "UPDATE tblPardakht set VarizDate=N'" + txtVarizDate.Text +
                                    "' ,ShomareSanad=N'" + txtShomareSanad.Text +
                                    "' ,BankName=N'" + txtNameBank.Text +
                                     "' ,Mablagh=N'" + Convert.ToDouble(txtMablaghVariz.Text.Replace(",", "")) +
                                    "' ,NameVarizKonande=N'" + txtNamePardakhkonande.Text +
                                     "' ,NoPardakht=N'" + cmbNoPardakht.Text +
                                    "' ,ShobeBank=N'" + txtShobeBank.Text +
                                    "' ,Tozihat=N'" + txtTozihat.Text +
                                    "' where PardakhtID=" + pardakhtid;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                                DisplayPardakht();
                                ////////////////////////////////////////////////////////////////////////
                                UpdateToMaliParvande(pardakhtid);
                                ////////////////////////////////////////////////////////////////////////
                                ts.Complete();
                            }
                            DisplayMali();
                            MessageBox.Show("ویرایش اطلاعات پرداخت باموفقیت انجام شد.");
                            pardakhtid = -1;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("خطایی در ویرایش رکورد رخ داده است");
                        }
                    }
                }
            }
        }
        private void btnZDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("آیا مایل به حذف رکورد هستید؟", "هشدار", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (pardakhtid != -1)
                {
                    try
                    {
                        using (var ts = new System.Transactions.TransactionScope())
                        {
                            cmd.Parameters.Clear();
                            cmd.Connection = con;
                            cmd.CommandText = "delete from tblPardakht where PardakhtID=" + pardakhtid;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            ////////////////////////////////////////////////////////////////
                            DeleteMaliPardakht(pardakhtid);
                            ////////////////////////////////////////////////////////////////
                            DisplayPardakht();
                            MessageBox.Show("حذف اطلاعات انجام شد.");
                            DisplayMali();
                            pardakhtid = -1;
                            ts.Complete();
                        }                      
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("خطایی در حذف اطلاعات رخ داده است.");
                    }
                }
                else { MessageBox.Show("لطفا روی رکورد سال مورد نظر کلیک کنید"); }
            }
        }
        private void btnPygiriPrint_Click(object sender, EventArgs e)
        {
            frmReport frmReportInput = new frmReport();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_ParvandeToPeygiri where ParvandeID='" + ParvandeID + "' ", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "View_ParvandeToPeygiri");
            ReporListPeygiri rptInput = new ReporListPeygiri();
            rptInput.SetDataSource(ds);
            frmReportInput.crystalReportViewer1.ReportSource = rptInput;
            frmReportInput.ShowDialog();
        }
        private void dgvPardakht_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvPardakht.Columns["Mablagh"].Index && e.RowIndex != this.dgvPardakht.NewRowIndex)
            {
                double d = double.Parse(e.Value.ToString());
                e.Value = d.ToString("#,##0.##");
            }
            if (e.ColumnIndex == dgvPardakht.Columns["Avarde"].Index && e.RowIndex != this.dgvPardakht.NewRowIndex)
            {
                double d = double.Parse(e.Value.ToString());
                e.Value = d.ToString("#,##0.##");
            }
        }
    }
}
