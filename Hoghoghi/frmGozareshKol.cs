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
using Stimulsoft.Report;


namespace Hoghoghi
{
    public partial class frmGozareshKol : Form
    {
        public frmGozareshKol()
        {
            InitializeComponent();

        }
        string path = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        System.Globalization.PersianCalendar dt = new System.Globalization.PersianCalendar();
        clsGetDataSource gds = new clsGetDataSource();
        int bankID = -1;
        public DataTable SetTableWithProcedure(string ProcedureName, string[] WhereField, string[] WhereValue)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                SqlCommand cmd = new SqlCommand(ProcedureName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < WhereField.Length; i++)
                {
                    SqlParameter p = new SqlParameter("@" + WhereField[i], WhereValue[i]);
                    cmd.Parameters.Add(p);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.CommandTimeout = 360;
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;
            }
            catch
            {
                con.Close();
                return null;
            }
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
  
        Int64 GetAvarde(int years)
        {
            Int64 Daryafti = 0;
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from tblMaliParvande where Years=" + years;
                adp.Fill(dt);
                int cunt = dt.Rows.Count;
                //**************************************************************
                for (int i = 0; i <= cunt - 1; i++)
                {
                    Daryafti += (Int64)dt.Rows[i]["Avarde"];
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش اطلاعات مالی پرونده رخ داده است");
            }
            return Daryafti;
        }
        Int64 GetAvarde(int years, int month)
        {
            Int64 Daryafti = 0;
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from tblMaliParvande where Years=" + years + "and Month=" + month;
                adp.Fill(dt);
                int cunt = dt.Rows.Count;
                //**************************************************************
                for (int i = 0; i <= cunt - 1; i++)
                {
                    Daryafti += (Int64)dt.Rows[i]["Avarde"];
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش اطلاعات مالی پرونده رخ داده است");
            }
            return Daryafti;
        }
        //------------------------------------------------------------
        Int64 GetSahmSherkat(int years)
        {
            Int64 Daryafti = 0;
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from tblMaliParvande where Years=" + years;
                adp.Fill(dt);
                int cunt = dt.Rows.Count;
                //**************************************************************
                for (int i = 0; i <= cunt - 1; i++)
                {
                    Daryafti += (Int64)dt.Rows[i]["Bes"];
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش اطلاعات مالی پرونده رخ داده است");
            }
            return Daryafti;
        }
        Int64 GetSahmSherkat(int years,int month)
        {
            Int64 Daryafti = 0;
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from tblMaliParvande where Years=" + years + "and Month=" + month;
                adp.Fill(dt);
                int cunt = dt.Rows.Count;
                //**************************************************************
                for (int i = 0; i <= cunt - 1; i++)
                {
                    Daryafti += (Int64)dt.Rows[i]["Bes"];
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش اطلاعات مالی پرونده رخ داده است");
            }
            return Daryafti;
        }
        //------------------------------------------------------------
        Int64 GetHoghoghKol(int years)
        {
            Int64 Daryafti = 0;
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from tblHoghogh where Years=" + years;
                adp.Fill(dt);
                int cunt = dt.Rows.Count;
                //**************************************************************
                for (int i = 0; i <= cunt - 1; i++)
                {
                    Daryafti += (Int64)dt.Rows[i]["Mablagh"];
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش اطلاعات حقوق رخ داده است");
            }
            return Daryafti;
        }
        Int64 GetHoghoghKol(int years, int month)
        {
            Int64 Daryafti = 0;
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from tblHoghogh where Years=" + years + "and Month=" + month;
                adp.Fill(dt);
                int cunt = dt.Rows.Count;
                //**************************************************************
                for (int i = 0; i <= cunt - 1; i++)
                {
                    Daryafti += (Int64)dt.Rows[i]["Mablagh"];
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش اطلاعات مالی پرونده رخ داده است");
            }
            return Daryafti;
        }
        //------------------------------------------------------------
        int GetHazine(int years)
        {
            int Daryafti = 0;
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from tblHazine where Years=" + years;
                adp.Fill(dt);
                int cunt = dt.Rows.Count;
                //**************************************************************
                for (int i = 0; i <= cunt - 1; i++)
                {
                    Daryafti += (int)dt.Rows[i]["Mablagh"];
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش اطلاعات هزینه سال رخ داده است");
            }
            return Daryafti;
        }
        int GetHazine(int years, int month)
        {
            int Daryafti = 0;
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from tblHazine where Years=" + years + "and Month=" + month;
                adp.Fill(dt);
                int cunt = dt.Rows.Count;
                //**************************************************************
                for (int i = 0; i <= cunt - 1; i++)
                {
                    Daryafti += (int)dt.Rows[i]["Mablagh"];
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش اطلاعات هزینه ماه رخ داده است");
            }
            return Daryafti;
        }
        //------------------------------------------------------------
        int GetSabtShode(int years)
        {
            int cunt = 0;
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from tblParvande where Years=" + years;
                adp.Fill(dt);
               cunt = dt.Rows.Count;
                //**************************************************************
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش اطلاعات هزینه سال رخ داده است");
            }
            return cunt;
        }
        int GetSabtShode(int years, int month)
        {
            int cunt = 0;
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from tblParvande where Years=" + years + "and Month=" + month;
                adp.Fill(dt);
                cunt = dt.Rows.Count;
                //**************************************************************
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش اطلاعات هزینه سال رخ داده است");
            }
            return cunt;
        }
        //----------------------------------------------------------
        int GetMakhtome(int years)
        {
            int cunt = 0;
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from tblParvande where Years=" + years + "and Vaziat=" + 0;
                adp.Fill(dt);
                cunt = dt.Rows.Count;
                //**************************************************************
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش اطلاعات هزینه سال رخ داده است");
            }
            return cunt;
        }
        int GetMakhtome(int years, int month)
        {
            int cunt = 0;
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from tblParvande where Years=" + years + "and Month=" + month + "and Vaziat=" + 0;
                adp.Fill(dt);
                cunt = dt.Rows.Count;
                //**************************************************************
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در نمایش اطلاعات هزینه سال رخ داده است");
            }
            return cunt;
        }
        //----------------------------------------------------------
        void ADDTodmnYears()
        {
            int years = dt.GetYear(DateTime.Now);
            for (int i = years; i > years-5; i--)
            {
                dmnYears.Items.Add(i);
            }
            dmnYears.SelectedIndex = 0;
        }
        private void frmGozareshKol_Load(object sender, EventArgs e)
        {
            txtDate.Text = dt.GetYear(DateTime.Now).ToString() + dt.GetMonth(DateTime.Now).ToString("0#") + dt.GetDayOfMonth(DateTime.Now).ToString("0#");
            txtDate2.Text = txtDate.Text;
            Int64 daryaftiSal = 0;
            Int64 hoghoghSal = 0;
            int hazineSal = 0;
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
            grpKol.Text = "سال " + dt.GetYear(DateTime.Now);
            //-----------------------------------------------------
            int yeras = dt.GetYear(DateTime.Now);
            ADDTodmnYears();
            daryaftiSal = GetSahmSherkat(int.Parse(dmnYears.Text));
            lblKSahm.Text = daryaftiSal.ToString("N0");
            //-----------------------------------------------------
            Int64 avarde = 0;
            avarde = GetAvarde(int.Parse(dmnYears.Text));
            lblAvarde.Text = avarde.ToString("N0");
            //-----------------------------------------------------
            hoghoghSal = GetHoghoghKol(int.Parse(dmnYears.Text));
            lblKHoghogh.Text=hoghoghSal.ToString("N0");
            //-----------------------------------------------------
            hazineSal = GetHazine(int.Parse(dmnYears.Text));
            lblKHazine.Text = hazineSal.ToString("N0");
            //-----------------------------------------------------
            Int64 sodSal = 0;
            sodSal = avarde - hoghoghSal - hazineSal;
            lblKSod.Text = sodSal.ToString("N0");
            //-----------------------------------------------------
            int cuntSal = 0;
            cuntSal = GetSabtShode(int.Parse(dmnYears.Text));
            lblSabt.Text=cuntSal.ToString("N0");
            //-----------------------------------------------------
            int makhtomeSal = 0;
            makhtomeSal = GetMakhtome(int.Parse(dmnYears.Text));
            lblKMakhtome.Text = makhtomeSal.ToString("N0");
            //-----------------------------------------------------
            int JariSal = 0;
            JariSal = cuntSal - makhtomeSal;
            lblKJari.Text = JariSal.ToString("N0");
            //-----------------------------------------------------         
        }
        private void cmbNoPardakht_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int64 daryaftiSal = 0;
            Int64 hoghoghSal = 0;
            Int64 AvardeSal = 0;
            int hazineSal = 0;
            //-----------------------------------------------------
            int x = cmbNoPardakht.SelectedIndex + 1;
            int y = int.Parse(dmnYears.Text);
            daryaftiSal = GetSahmSherkat(y,x);
            lblMSahm.Text = daryaftiSal.ToString("N0");
            //-----------------------------------------------------
            AvardeSal = GetAvarde(int.Parse(dmnYears.Text), cmbNoPardakht.SelectedIndex + 1);
            lblMAvarde.Text = AvardeSal.ToString("N0");
            //-----------------------------------------------------
            hoghoghSal = GetHoghoghKol(int.Parse(dmnYears.Text), cmbNoPardakht.SelectedIndex+1);
            lblMHoghogh.Text = hoghoghSal.ToString("N0");
            //-----------------------------------------------------
            hazineSal = GetHazine(int.Parse(dmnYears.Text), cmbNoPardakht.SelectedIndex + 1);
            lblMHazine.Text = hazineSal.ToString("N0");
            //-----------------------------------------------------
            Int64 sodSal = 0;
            sodSal = AvardeSal - hoghoghSal - hazineSal;
            lblMSod.Text = sodSal.ToString("N0");
            //-----------------------------------------------------
            int cuntSal = 0;
            cuntSal = GetSabtShode(int.Parse(dmnYears.Text), cmbNoPardakht.SelectedIndex + 1);
            lblSabtm.Text = cuntSal.ToString("N0");
            //-----------------------------------------------------
            int makhtomeSal = 0;
            makhtomeSal = GetMakhtome(int.Parse(dmnYears.Text), cmbNoPardakht.SelectedIndex + 1);
            lblMMakhtome.Text = makhtomeSal.ToString("N0");
            //-----------------------------------------------------
            int JariSal = 0;
            JariSal = cuntSal - makhtomeSal;
            lblMJari.Text = JariSal.ToString("N0");
        }
        private void dmnYears_SelectedItemChanged(object sender, EventArgs e)
        {
            Int64 daryaftiSal = 0;
            Int64 hoghoghSal = 0;
            int hazineSal = 0;
            //groupPanel2.Text = cmbNoPardakht.SelectedItem.ToString();
            daryaftiSal = GetSahmSherkat(int.Parse(dmnYears.Text));
            lblKSahm.Text = daryaftiSal.ToString("N0");
            //-----------------------------------------------------
            hoghoghSal = GetHoghoghKol(int.Parse(dmnYears.Text));
            lblKHoghogh.Text = hoghoghSal.ToString("N0");
            //-----------------------------------------------------
            hazineSal = GetHazine(int.Parse(dmnYears.Text));
            lblKHazine.Text = hazineSal.ToString("N0");
            //-----------------------------------------------------
            Int64 sodSal = 0;
            sodSal = daryaftiSal - hoghoghSal - hazineSal;
            lblKSod.Text = sodSal.ToString("N0");
            //-----------------------------------------------------
            int cuntSal = 0;
            cuntSal = GetSabtShode(int.Parse(dmnYears.Text));
            lblSabt.Text = cuntSal.ToString("N0");
            //-----------------------------------------------------
            int makhtomeSal = 0;
            makhtomeSal = GetMakhtome(int.Parse(dmnYears.Text));
            lblKMakhtome.Text = makhtomeSal.ToString("N0");
            //-----------------------------------------------------
            int JariSal = 0;
            JariSal = cuntSal - makhtomeSal;
            lblKJari.Text = JariSal.ToString("N0");
        }

        private void btnVarizi_Click(object sender, EventArgs e)
        {
            new frmVarizi().ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt_parametr = new DataTable("dt_parametr");
            dt_parametr.Columns.Add("AzDate");
            dt_parametr.Columns.Add("TaDate");
            DataRow row1 = dt_parametr.NewRow();
            row1["AzDate"] = txtDate.Text;
            row1["TaDate"] = txtDate2.Text;
            dt_parametr.Rows.Add(row1); 
            DataTable dt_gozaresh = new DataTable();
            dt_gozaresh = SetTableWithProcedure("ReportHazine", new string[] { "AzDate" , "TaDate" }, new string[] { txtDate.Text,txtDate2.Text });
            StiReport report = new StiReport();
            report.Load("Report/rptMaliExccel.mrt");
            report["@AzDate"] = txtDate.Text;
            report["@TaDate"] = txtDate2.Text;
            report.Compile();
            report.RegData("dt_gozaresh", dt_gozaresh);
            report.RegData("dt_parametr", dt_parametr);
            report.Render();
            report.ShowWithRibbonGUI();

        }
        void SetDataTable_To_Excel(System.Data.DataTable dtTable, string PathFileName)
        {

            try
            {
                var excel = new Microsoft.Office.Interop.Excel.Application { Visible = false };
                var misValue = System.Reflection.Missing.Value;
                var wb = excel.Workbooks.Add(misValue);
                Microsoft.Office.Interop.Excel.Worksheet sh = wb.Sheets.Add();
                sh.Name = "TestSheet";
                sh.Cells[1, "A"].Value2 = "از تاریخ";
                sh.Cells[1, "B"].Value2 = txtDate.Text;
                sh.Cells[1, "C"].Value2 = "تا تاریخ";
                sh.Cells[1, "D"].Value2 = txtDate2.Text;
                ////////////////////////////
                sh.Cells[2, "A"].Value2 = "ردیف";
                sh.Cells[2, "B"].Value2 = "کد ملی";
                sh.Cells[2, "C"].Value2 = "کد بانک";
                sh.Cells[2, "D"].Value2 = "کد شعبه";
                sh.Cells[2, "E"].Value2 = "بانک";
                sh.Cells[2, "F"].Value2 = "شعبه";
                sh.Cells[2, "G"].Value2 = "نام کارشناس";
                sh.Cells[2, "H"].Value2 = "شماره تصحیلات";
                sh.Cells[2, "I"].Value2 = "تاریخ";
                sh.Cells[2, "J"].Value2 = "مبلغ";


                // Insert Rows
                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    sh.Cells[i + 3, "A"].Value2 = i + 1; // Redif
                    sh.Cells[i + 3, "B"].Value2 += dtTable.Rows[i][0];
                    sh.Cells[i + 3, "C"].Value2 += dtTable.Rows[i][1];
                    sh.Cells[i + 3, "D"].Value2 += dtTable.Rows[i][2];
                    sh.Cells[i + 3, "E"].Value2 += dtTable.Rows[i][3];
                    sh.Cells[i + 3, "F"].Value2 += dtTable.Rows[i][4];
                    sh.Cells[i + 3, "G"].Value2 += dtTable.Rows[i][5];
                    sh.Cells[i + 3, "H"].Value2 += dtTable.Rows[i][6];
                    sh.Cells[i + 3, "I"].Value2 += dtTable.Rows[i][7];
                    sh.Cells[i + 3, "J"].Value2 += dtTable.Rows[i][8]; //
                }
                sh.DisplayRightToLeft = true;
                wb.SaveAs(PathFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                wb.Close(true);
                excel.Quit();
            }
            catch (Exception ex)
            {
            }
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {

            DataTable dt_gozaresh = new DataTable();
            dt_gozaresh = SetTableWithProcedure("ReportHazine", new string[] { "AzDate", "TaDate" }, new string[] { txtDate.Text, txtDate2.Text });
            SaveFileDialog SaveBackUp = new SaveFileDialog();
            string filename = string.Empty;
            SaveBackUp.OverwritePrompt = true;
            SaveBackUp.Filter = "Execl files (*.xls)|*.xls";
            SaveBackUp.FilterIndex = 1;
            SaveBackUp.FileName = DateTime.Now.ToString("Zarrin dd-MM-yyyy_HH-mm-ss");
            SaveBackUp.Title = "Save To Excel";
            if (SaveBackUp.ShowDialog() == DialogResult.OK)
            {
                SetDataTable_To_Excel(dt_gozaresh, SaveBackUp.FileName);
            }

        }
    }
}
