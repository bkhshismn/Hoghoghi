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
    public partial class frmGhararRoz : Form
    {
        public frmGhararRoz()
        {
            InitializeComponent();
        }
        string path = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        System.Globalization.PersianCalendar dt = new System.Globalization.PersianCalendar();
        clsGetDataSource gds = new clsGetDataSource();
        int parvandeID = -1;
        int ghararID = -1;
        public int GhararID;
        public string NoGharar;
        public string Matn;
        string dateNow;
        public int UserID { get; set; }
        public int Sath { get; set; }
        void DgvList()
        {
            dgvParvande.Columns["UserID"].Visible = false;
            dgvParvande.Columns["Vaziat"].Visible = false;
            dgvParvande.Columns["Years"].Visible = false;
            dgvParvande.Columns["GhararID"].Visible = false;
            dgvParvande.Columns["VaziatGharar"].Visible = false;
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
            dgvParvande.Columns["Melli"].Visible = false;
            dgvParvande.Columns["Shoghl"].HeaderText = "شغل";
            dgvParvande.Columns["Shoghl"].Width = 50;
            dgvParvande.Columns["Shoghl"].Visible = false;
            dgvParvande.Columns["TelSabet"].HeaderText = "تلفن ثابت";
            dgvParvande.Columns["TelSabet"].Width = 80;
            dgvParvande.Columns["TelSabet"].Visible = false;
            dgvParvande.Columns["TelHamrah"].HeaderText = "شماره همراه";
            dgvParvande.Columns["TelHamrah"].Width = 80;
            dgvParvande.Columns["TelHamrah"].Visible = false;
            dgvParvande.Columns["ShTashilat"].HeaderText = "شماره تسهیلات";
            dgvParvande.Columns["ShTashilat"].Width = 80;
            dgvParvande.Columns["Date"].HeaderText = "تاریخ دریافت ";
            dgvParvande.Columns["Date"].Width = 70;
            dgvParvande.Columns["Bed"].HeaderText = "مبلغ بدهی";
            dgvParvande.Columns["Bed"].Width = 100;
            dgvParvande.Columns["Address"].HeaderText = "آدرس";
            dgvParvande.Columns["Address"].Width = 250;
            dgvParvande.Columns["Address"].Visible = false;
            dgvParvande.Columns["DateGharar"].HeaderText = "تاریخ قرار ";
            dgvParvande.Columns["DateGharar"].Width = 70;
            dgvParvande.Columns["NoGharar"].HeaderText = "نوع قرار ";
            dgvParvande.Columns["NoGharar"].Width = 70;
            dgvParvande.Columns["Matn"].HeaderText = "متن قرار ";
            dgvParvande.Columns["Matn"].Width = 500;
            dgvParvande.Columns["DateTimeSabt"].HeaderText = "تاریخ قرار ";
            dgvParvande.Columns["DateTimeSabt"].Width = 70;
        }
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
        void DisplayParvande()
        {
            dateNow = "'" + dateNow + "'";
            DataSet ds = new DataSet();
            SqlDataAdapter adb = new SqlDataAdapter();
            adb.SelectCommand = new SqlCommand();
            adb.SelectCommand.Connection = con;
            adb.SelectCommand.CommandText = "select * from View_ParvandeToGharar where UserID=" + UserID + "and Vaziat=" + 1 + " and VaziatGharar=" + 0 + " and DateGharar=N" + dateNow + " ORDER BY GhararID desc";
            adb.Fill(ds, "tblParvande");
            dgvParvande.DataSource = ds;
            dgvParvande.DataMember = "tblParvande";
          
            DgvList();
        }
        void DisplayParvande(int y)
        {
            dateNow = "'" + dateNow + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlDataAdapter adb = new SqlDataAdapter();
            adb.SelectCommand = new SqlCommand();
            adb.SelectCommand.Connection = con;

            adb.SelectCommand.CommandText = "select * from View_ParvandeToGharar where (UserID=" + UserID + " or Years=" + y + ") and Vaziat=" + 1 + " and VaziatGharar=" + 0 + " and DateGharar = N" + dateNow + " ORDER BY GhararID desc";
            adb.Fill(ds, "View_ParvandeToGharar");
            adb.Fill(dt);
            int count = dt.Rows.Count;
            dgvParvande.DataSource = ds;
            dgvParvande.DataMember = "View_ParvandeToGharar";
            DgvList();
        }
        private void LoadfrmPardakht(int outid)
        {
            frmParvandeMain frmPM = new frmParvandeMain();
            frmPM.ParvandeID = parvandeID;
            frmPM.ShowDialog();
        }
        private void LoadfrmEvent(int outid, int parvandeid)
        {
            frmEventGharar frmgh = new frmEventGharar();
            frmgh.GhararID= ghararID;
            frmgh.parvandeID = parvandeid;
            frmgh.Matn = Matn;
            frmgh.NoGharar = NoGharar;
            frmgh.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
            frmgh.ShowDialog();
        }
        private void frmGhararRoz_Load(object sender, EventArgs e)
        {
            btnRefresh.Visible = false;
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
            dateNow = dt.GetYear(DateTime.Now).ToString() + "/" + dt.GetMonth(DateTime.Now).ToString("0#") + "/" + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
            txtDate.Text = dt.GetYear(DateTime.Now).ToString() + dt.GetMonth(DateTime.Now).ToString("0#") + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
            txtDate1.Text = dt.GetYear(DateTime.Now).ToString() + dt.GetMonth(DateTime.Now).ToString("0#") + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
            sath();
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dgvParvande.Columns.Add(btn);
            btn.HeaderText = "رویداد";
            btn.Text = "وضعیت";
            btn.Name = "btnEvent";
            btn.UseColumnTextForButtonValue = true;
        }
        private void dgvParvande_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                parvandeID = (int)dgvParvande.Rows[e.RowIndex].Cells["ParvandeID"].Value;
                LoadfrmPardakht(parvandeID);
                dgvParvande.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception)
            {
                MessageBox.Show("لطفا روب رکورد مورد نظر کلیک کنید");
            }
        }
        private void txtDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from View_ParvandeToGharar where DateGharar Like '%' + @s + '%' and UserID=" + UserID + "and Vaziat=" + 1 + "";
                adp.SelectCommand.Parameters.AddWithValue("@s", txtDate.Text + "%");
                adp.Fill(ds, "View_ParvandeToGharar");
                dgvParvande.DataSource = ds;
                dgvParvande.DataMember = "View_ParvandeToGharar";
                dgvParvande.Columns["UserID"].Visible = false;
                dgvParvande.Columns["Vaziat"].Visible = false;
                dgvParvande.Columns["ParvandeID"].HeaderText = "شماره پرونده";
                dgvParvande.Columns["BankName"].HeaderText = "نام بانک";
                dgvParvande.Columns["BankName"].Width = 100;
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
                dgvParvande.Columns["DateGharar"].HeaderText = "تاریخ قرار ";
                dgvParvande.Columns["DateGharar"].Width = 70;
                dgvParvande.Columns["NoGharar"].HeaderText = "نوع قرار ";
                dgvParvande.Columns["NoGharar"].Width = 70;
                dgvParvande.Columns["Matn"].HeaderText = "متن قرار ";
                dgvParvande.Columns["Matn"].Width = 500;
                dgvParvande.Columns["DateTimeSabt"].HeaderText = "تاریخ قرار ";
                dgvParvande.Columns["DateTimeSabt"].Width = 70;
            }
            catch (Exception)
            {
                MessageBox.Show("خطا در نمایش اطلاعات قرار رخ داده است");
            }

        }
        private void btnGHarar_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("آیا مایل به ویرایش رکورد هستتید؟", "هشدار", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    cmd.Parameters.Clear();
                    cmd.Connection = con;
                    cmd.CommandText = "UPDATE tblGharar set DateGharar=N'" + txtDate1.Text + "' where DateGharar Like '%' + @n + '%' and VaziatGharar=" + 0 + "";
                    cmd.Parameters.AddWithValue("@n", txtDate.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("ویرایش اطلاعات قرار باموفقیت انجام شد.");
                }
                catch (Exception)
                {
                    MessageBox.Show("خطایی در ویرایش رکورد رخ داده است");
                }
            }
        }
        private void dgvParvande_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.dgvParvande.Columns["Bed"].Index && e.RowIndex != this.dgvParvande.NewRowIndex)
                {
                    double d = double.Parse(e.Value.ToString());
                    e.Value = d.ToString("#,##0.##");
                }
            }
            catch (Exception)
            {
            }
        }

        private void dgvParvande_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        dgvParvande.Rows[e.RowIndex].Selected = true;
            try
            {
                if (dgvParvande.Columns[e.ColumnIndex].Name == "btnEvent")
                {
                    ghararID = (int)dgvParvande.Rows[e.RowIndex].Cells["GhararID"].Value;
                    parvandeID = (int)dgvParvande.Rows[e.RowIndex].Cells["ParvandeID"].Value;
                    NoGharar = (string)dgvParvande.Rows[e.RowIndex].Cells["NoGharar"].Value;
                    Matn = (string)dgvParvande.Rows[e.RowIndex].Cells["Matn"].Value;
                    LoadfrmEvent(ghararID, parvandeID);
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dateNow = dt.GetYear(DateTime.Now).ToString() + "/" + dt.GetMonth(DateTime.Now).ToString("0#") + "/" + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
            sath();
        }
    }
}
