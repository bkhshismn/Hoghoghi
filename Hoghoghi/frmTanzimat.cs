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
    public partial class frmTanzimat : Form
    {
        public frmTanzimat()
        {
            InitializeComponent();
        }
        string path = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        System.Globalization.PersianCalendar dt = new System.Globalization.PersianCalendar();
        clsGetDataSource gds = new clsGetDataSource();
        public void GetPath()
        {
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
                    textBoxX1.Text=(dt.Rows[0]["PathBkup"]).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش نام بانک رخ داده است");
            }
        }
        private void btnNopeygiri_Click(object sender, EventArgs e)
        {
            new frmNoPeygiri().ShowDialog();
        }

        private void btnGharardad_Click(object sender, EventArgs e)
        {
            new frmKarmozd().ShowDialog();
        }

        private void btnBank_Click(object sender, EventArgs e)
        {
            new frmBank().ShowDialog();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            new frmUser().ShowDialog();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            new frmShobe().ShowDialog();
        }

        private void frmTanzimat_Load(object sender, EventArgs e)
        {
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
            GetPath();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            try
            {

                if (textBoxX1.Text == "" )
                {
                    MessageBox.Show("فیلد های خالی را پر کنید...");
                }
                else
                {
                    var result = MessageBox.Show("آیا مایل به ویرایش رکورد هستید؟", "هشدار", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        cmd.Parameters.Clear();
                        cmd.Connection = con;
                        cmd.CommandText = "update tblPathBkup set PathBkup=N'" + textBoxX1.Text +"'";
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("ویرایش با موفقیت انجام شد");

                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در ویرایش رکورد رخ داده است");
            }
        }

        private void btnSarbarg_Click(object sender, EventArgs e)
        {
            new frmSarbarg().ShowDialog();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            new frmSortKarshenas().ShowDialog();
        }

        private void btnGhardadBanki_Click(object sender, EventArgs e)
        {
            new frmGharardadBank().ShowDialog();
        }
    }
}
