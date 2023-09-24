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
    public partial class frmEventGharar : Form
    {
        public frmEventGharar()
        {
            InitializeComponent();
        }
        string path = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        System.Globalization.PersianCalendar dt = new System.Globalization.PersianCalendar();
        clsGetDataSource gds = new clsGetDataSource();
        public int GhararID { get; set; }
        public int parvandeID { get; set; }
        public string NoGharar { get; set; }
        public string Matn { get; set; }
        void EditGharar()
        {
            if (chkGharar.Checked)
            {
                var result = MessageBox.Show("آیا مایل به ویرایش رکورد هستتید؟", "هشدار", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.Connection = con;
                        cmd.CommandText = "UPDATE tblGharar set VaziatGharar=N'" + 1 + "' where GhararID=" + GhararID;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("انجام شد.");
                        GhararID = -1;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("خطایی در ویرایش رکورد رخ داده است");
                    }
                }
            }
        }
        private void frmEventGharar_Load(object sender, EventArgs e)
        {
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
            txtDate.Text = dt.GetYear(DateTime.Now).ToString() + dt.GetMonth(DateTime.Now).ToString("0#") + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
            this.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
            txtDate.Visible = false;
            btnDateOk.Visible = false;
            //MessageBox.Show(GhararID.ToString());
        }
        private void chkGharar_CheckedChanged(object sender, EventArgs e)
        {
            txtDate.Visible = true;
            btnDateOk.Visible = true;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnEventOk_Click(object sender, EventArgs e)
        {

            if (chkOk.Checked)
            {
                var result = MessageBox.Show("آیا مایل به ویرایش رکورد هستتید؟", "هشدار", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.Connection = con;
                        cmd.CommandText = "UPDATE tblGharar set VaziatGharar=N'" + 1 + "' where GhararID=" + GhararID;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("انجام شد.");
                        GhararID = -1;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("خطایی در ویرایش رکورد رخ داده است");
                    }
                }
            }
        }
        private void frmEventGharar_FormClosing(object sender, FormClosingEventArgs e)
        {
            var frm = Application.OpenForms.Cast<Form>().Where(x => x.Name == "frmGhararRoz").FirstOrDefault();
            if (null != frm)
            {
                frmGhararRoz master = (frmGhararRoz)Application.OpenForms["frmGhararRoz"];
                master.btnRefresh.PerformClick();
            }
        }
        private void btnDateOk_Click(object sender, EventArgs e)
        {
            using (var ts = new System.Transactions.TransactionScope())
            {
                try
                {
                    var date = dt.GetYear(DateTime.Now).ToString() + "/" + dt.GetMonth(DateTime.Now).ToString("0#") + "/" + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
                    cmd.Parameters.Clear();
                    cmd.Connection = con;
                    cmd.CommandText = "insert into tblGharar(NoGharar,Matn,DateTimeSabt,ParvandeID,DateGharar)values                                        (@NoGharar,@Matn,@DateTimeSabt,@ParvandeID,@DateGharar)";
                    cmd.Parameters.AddWithValue("@NoGharar", NoGharar);
                    cmd.Parameters.AddWithValue("@Matn", "قرار مجدد" + "\n" + Matn);
                    cmd.Parameters.AddWithValue("@DateTimeSabt", date);
                    cmd.Parameters.AddWithValue("@DateGharar", txtDate.Text);
                    cmd.Parameters.AddWithValue("@ParvandeID", parvandeID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    EditGharar();
                    MessageBox.Show(" قرار  مجدد با موفقیت ثبت شد");
                }
                catch (Exception)
                {
                    MessageBox.Show("مشکلی در ثبت اطلاعات وجود دارد!");
                }
                ////////////////////////////////////////////////////////////////////////

                ////////////////////////////////////////////////////////////////////////
                ts.Complete();
            }
        }
    }
}
