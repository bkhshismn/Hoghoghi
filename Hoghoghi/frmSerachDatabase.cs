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
    public partial class frmSerachDatabase : Form
    {
        public frmSerachDatabase()
        {
            InitializeComponent();
        }
        string path = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        System.Globalization.PersianCalendar dt = new System.Globalization.PersianCalendar();
        clsGetDataSource gds = new clsGetDataSource();
        void DisplayParvande(DataGridView dgvParvande)
        {

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
            dgvParvande.Columns["DateSabt"].HeaderText = "تاریخ ثبت ";
            dgvParvande.Columns["DateSabt"].Width = 70;
            dgvParvande.Columns["Komision"].HeaderText = "کمیسیون";
        }
        private void buttonX4_Click(object sender, EventArgs e)
        {
            //try
            //{
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                DataTable dtbl = new DataTable();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from tblParvande where   Melli Like '%' + @Melli + '%'";
                adp.SelectCommand.Parameters.AddWithValue("@Melli", txtMelli.Text + "%");
                
                adp.Fill(ds, "tblParvande");
                dgvParvande.DataSource = ds;
                adp.Fill(dtbl);
                int count = dtbl.Rows.Count;
                if (count == 0)
                {
                    labelX1.Text = "پرونده با چنین شماره ملی تا کنون در این سیستم ثبت نشد";
                }
                else
                {
                    dgvParvande.DataMember = "tblParvande";
                    labelX1.Text = "تعداد " + count.ToString() + " پرونده در سیستم ثبت شده است ";
                    DisplayParvande(dgvParvande);
                }


            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("مشکلی در جستوجو با  کلید جستوجو رخ داده است.");
            //}
        }

        private void frmSerachDatabase_Load(object sender, EventArgs e)
        {
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
        }
    }
}
