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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region Prop
        public int SathDasresi { get; set; }
        public int UserID { get; set; }
        public string UserPass{ get; set; }
        public string UserName{ get; set; }
        public string uName { get; set; }

        #endregion
        string path = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        System.Globalization.PersianCalendar dt = new System.Globalization.PersianCalendar();
        clsGetDataSource gds = new clsGetDataSource();
        int parvandeID = -1;
        string dateNow;
        #region Methods
        public string GetPath()
        {
            string path = "";
            try
            {
                con.Close();
                string query = "SELECT  PathBkup FROM [tblPathBkup]";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                cmd.ExecuteScalar();
                con.Close();
                path = (dt.Rows[0]["PathBkup"]).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش نام بانک رخ داده است");
            }
            return path;
        }
        void sath()
        {
            if (SathDasresi == 0)
            {
                DisplayParvande();
            }
            else
            {
                DisplayParvande(dt.GetYear(DateTime.Now));
            }
        }
        void DisplayParvande()
        {
            dateNow = "'" + dateNow + "'";
            DataSet ds = new DataSet();
            DataTable dtb = new DataTable();
            SqlDataAdapter adb = new SqlDataAdapter();
            adb.SelectCommand = new SqlCommand();
            adb.SelectCommand.Connection = con;
            adb.SelectCommand.CommandText = "select * from View_ParvandeToGharar where UserID=" + UserID + "and Vaziat=" + 1 + "and VaziatGharar=" + 0 + " and DateGharar=N" + dateNow + "  ORDER BY ParvandeID desc";
            adb.Fill(dtb);
            int cunt = dtb.Rows.Count;
            btnGHarar.Text = (" تعداد قرار امروز" +": "+ cunt).ToString();

        }
        void DisplayParvande(int y)
        {

            dateNow = "'" + dateNow + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from View_ParvandeToGharar where (UserID=" + UserID + " or Years=" + y + ") and Vaziat=" + 1 + " and VaziatGharar=" + 0 + " and DateGharar = N" + dateNow + " ORDER BY ParvandeID desc";
            adp.Fill(dt);
            int cunt = dt.Rows.Count;
            btnGHarar.Text = ("تعداد قرار امروز" + ": " + cunt).ToString();

        }
        private void Backup(string filename)
        {
            SqlConnection oconnection = null;
            try
            {
                string command = @"Backup DataBase [DBHoghoghi] To Disk='" + filename + "'";
                this.Cursor = Cursors.WaitCursor;
                SqlCommand ocommand = null;
                oconnection = con;
                if (oconnection.State != ConnectionState.Open)
                    oconnection.Open();
                ocommand = new SqlCommand(command, oconnection);
                ocommand.ExecuteNonQuery();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
            finally
            {
                oconnection.Close();
            }
        }
        private void Restore(string filename)
        {
            SqlConnection oconnection = null;
            try
            {
                string command = @"ALTER DATABASE [DBHoghoghi] SET SINGLE_USER with ROLLBACK IMMEDIATE " + " USE master " + " RESTORE DATABASE [DBHoghoghi] FROM DISK= N'" + filename + "'WITH RECOVERY, REPLACE";
                this.Cursor = Cursors.WaitCursor;
                SqlCommand ocommand = null;
                oconnection = con;// new SqlConnection("Data Source=.;Initial Catalog=DBTajeranBerenj;Integrated Security=True");
                if (oconnection.State != ConnectionState.Open)
                    oconnection.Open();
                ocommand = new SqlCommand(command, oconnection);
                ocommand.ExecuteNonQuery();
                this.Cursor = Cursors.Default;
                MessageBox.Show("باز نشانی پشتیبان  انجام شد");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : ", ex.Message);
            }
            finally
            {
                oconnection.Close();
            }
        }
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
            dateNow = dt.GetYear(DateTime.Now).ToString() + "/" + dt.GetMonth(DateTime.Now).ToString("0#") + "/" + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
            if (SathDasresi == 0)
            {
                btnTanzimat.Visible = false;
                btnRprtKol.Visible = false;
                btbMakhtome.Visible = false;
                btbRKarbar.Visible = false;
                btnHazine.Visible = false;
                this.Text = this.Text + "-" + "کاربر فعال: " + uName;
                this.WindowState = FormWindowState.Maximized;
                DisplayParvande();
            }
            else
            {
                this.Text = this.Text + "-" + "کاربر فعال: " + uName;
                //this.WindowState = FormWindowState.Maximized;
                DisplayParvande(dt.GetYear(DateTime.Now));
            }

        }
        private void btnKol_Click(object sender, EventArgs e)
        {
            frmParvande frmparvande = new frmParvande();
            frmparvande.UserID = UserID;
            frmparvande.Sath = SathDasresi;
            frmparvande.ShowDialog();
        }
        private void btnListParvande_Click(object sender, EventArgs e)
        {
            frmViewParvande ViewParvande = new frmViewParvande();
            ViewParvande.UserID = UserID;
            ViewParvande.Sath = SathDasresi;
            ViewParvande.ShowDialog();
        }
        private void btnHoghogh_Click(object sender, EventArgs e)
        {
            frmHoghogh frmGhararRoz = new frmHoghogh();
            frmGhararRoz.UserID = UserID;
            //frmGhararRoz.chk = 1;
            frmGhararRoz.Sath = SathDasresi;
            frmGhararRoz.ShowDialog();
        }
        private void btnTanzimat_Click(object sender, EventArgs e)
        {
            new frmTanzimat().ShowDialog();
        }
        private void btbRKarbar_Click(object sender, EventArgs e)
        {
            new frmGozareshKarsenas().ShowDialog();
        }
        private void btnHazine_Click(object sender, EventArgs e)
        {
            new frmHazine().ShowDialog();
        }
        private void btnRprtKol_Click(object sender, EventArgs e)
        {
            new frmGozareshKol().ShowDialog();
        }
        private void btnGHarar_Click(object sender, EventArgs e)
        {
            frmGhararRoz frmGhararRoz = new frmGhararRoz();
            frmGhararRoz.UserID = UserID;
            frmGhararRoz.Sath = SathDasresi;
            frmGhararRoz.ShowDialog();
        }
        private void btnBackUp_Click(object sender, EventArgs e)
        {
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
            SaveFileDialog SaveBackUp = new SaveFileDialog();
            string filename = string.Empty;
            SaveBackUp.OverwritePrompt = true;
            SaveBackUp.Filter = @"SQL Backup Files ALL Files (*.*) |*.*| (*.Bak)|*.Bak";
            SaveBackUp.DefaultExt = "Bak";
            SaveBackUp.FilterIndex = 1;
            SaveBackUp.FileName = DateTime.Now.ToString("Zarrin dd-MM-yyyy_HH-mm-ss");
            SaveBackUp.Title = "Backup SQL File";
            if (SaveBackUp.ShowDialog() == DialogResult.OK)
            {
                filename = SaveBackUp.FileName;
                Backup(filename);
            }
        }
        private void btbRestor_Click(object sender, EventArgs e)
        {
            string filename = string.Empty;
            OpenFileDialog OpenBackUp = new OpenFileDialog();
            OpenBackUp.Filter = @"SQL Backup Files ALL Files (*.*) |*.*| (*.Bak)|*.Bak";
            OpenBackUp.FilterIndex = 1;
            OpenBackUp.Filter = @"SQL Backup Files (*.*)|";

            OpenBackUp.FileName = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
            if (OpenBackUp.ShowDialog() == DialogResult.OK)
            {
                filename = OpenBackUp.FileName;
                Restore(filename);
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            new frmSerachDatabase().ShowDialog();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string pth = "" + GetPath() + DateTime.Now.ToString("Zarrin dd-MM-yyyy_HH-mm-ss") + ".BAK" + "";
                Backup(pth);
            }
            catch
            {
                MessageBox.Show("مشکلی در پشتیبانی خودکار رخ داده است");
            }
        }

        private void btbMakhtome_Click(object sender, EventArgs e)
        {
            frmMakhtome ViewParvande = new frmMakhtome();
            ViewParvande.UserID = UserID;
            ViewParvande.Sath = SathDasresi;
            ViewParvande.ShowDialog();
        }
    }
}
