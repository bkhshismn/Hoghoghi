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
    public partial class frmListFaaliat : Form
    {
        public frmListFaaliat()
        {
            InitializeComponent();
        }
        string path = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        System.Globalization.PersianCalendar dt = new System.Globalization.PersianCalendar();
        public int UserID { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string AzDate { get; set; }
        public string TaDate { get; set; }

        clsGetDataSource gds = new clsGetDataSource();
        void DisplayView_UserFaliat()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adb = new SqlDataAdapter();
            adb.SelectCommand = new SqlCommand();
            adb.SelectCommand.Connection = con;
            //adb.SelectCommand.CommandText = "select * from View_UserFaliat where UserID=" + UserID + " and Years=" + Year + " and Month=" + Month + "and Bes IS NOT NULL ORDER BY Month desc ";
            adb.SelectCommand.CommandText = "select * from View_UserFaliat where UserID=" + UserID + " and PardakhtDate between   '" + AzDate + "'and '" + TaDate + "'and Bes IS NOT NULL ORDER BY Month desc ";
            adb.Fill(ds, "View_UserFaliat");
            dgvFaaliat.DataSource = ds;
            dgvFaaliat.DataMember = "View_UserFaliat";
            dgvFaaliat.Columns["Bes"].HeaderText = "واریزی";
            dgvFaaliat.Columns["Bes"].Width = 180;
            dgvFaaliat.Columns["Avarde"].HeaderText = "آورده";
            dgvFaaliat.Columns["Avarde"].Width = 180;

            dgvFaaliat.Columns["ParvandeID"].HeaderText = "شماره پرونده";
            dgvFaaliat.Columns["ParvandeID"].Width = 100;

            dgvFaaliat.Columns["Name"].HeaderText = "نام";
            dgvFaaliat.Columns["Name"].Width = 170;

            dgvFaaliat.Columns["FatherName"].HeaderText = "نام پدر";
            dgvFaaliat.Columns["FatherName"].Width = 100;

            dgvFaaliat.Columns["UserID"].Visible = false;
            dgvFaaliat.Columns["Bed"].HeaderText = " مبلغ دین";
            dgvFaaliat.Columns["Bed"].Width =200;

            dgvFaaliat.Columns["Years"].HeaderText = "سال";
            dgvFaaliat.Columns["Years"].Width = 50;

            dgvFaaliat.Columns["Month"].HeaderText = "ماه";
            dgvFaaliat.Columns["Month"].Width = 40; ;

            dgvFaaliat.Columns["PardakhtDate"].HeaderText = "تاریخ پرداخت ";
            dgvFaaliat.Columns["PardakhtDate"].Width = 90;
            con.Close();
        }
        private void frmListFaaliat_Load(object sender, EventArgs e)
        {
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
            DisplayView_UserFaliat();
        }

        private void dgvFaaliat_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex != this.dgvFaaliat.NewRowIndex)
            {
                double d = double.Parse(e.Value.ToString());
                e.Value = d.ToString("#,##0.##");
            }
            if (e.ColumnIndex == 5 && e.RowIndex != this.dgvFaaliat.NewRowIndex)
            {
                double d = double.Parse(e.Value.ToString());
                e.Value = d.ToString("#,##0.##");
            }
            if (e.ColumnIndex == dgvFaaliat.Columns["Avarde"].Index && e.RowIndex != this.dgvFaaliat.NewRowIndex)
            {
                double d = double.Parse(e.Value.ToString());
                e.Value = d.ToString("#,##0.##");
            }
        }
    }
}
