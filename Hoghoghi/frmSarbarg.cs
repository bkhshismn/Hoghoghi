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
    public partial class frmSarbarg : Form
    {
        public frmSarbarg()
        {
            InitializeComponent();
        }
        string path = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        System.Globalization.PersianCalendar dt = new System.Globalization.PersianCalendar();
        clsGetDataSource gds = new clsGetDataSource();
        int ParvandeID = -1;
        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        int GetReferID()
        {
            int Parvandeid = -1;
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "select * from tblSarbarg ";
            adp.Fill(dt);
            int cunt = dt.Rows.Count;
            Parvandeid = (int)dt.Rows[cunt - 1]["SarbargID"];
            return Parvandeid;
        }
        void display()
        {
            try
            {
                cmd.Parameters.Clear();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand();
                adp.SelectCommand.Connection = con;
                adp.SelectCommand.CommandText = "select * from [tblSarbarg] where SarbargID =" + 1;
                con.Open();
                adp.Fill(dt);
                this.txtAddress.Text = dt.Rows[0]["Address"].ToString();
                this.txtHamrah.Text = dt.Rows[0]["ShomareHamrah"].ToString();
                this.txtSabet.Text = dt.Rows[0]["ShomareSabet"].ToString();
                this.txtSarbarg.Text = dt.Rows[0]["Sarbarg"].ToString();
                this.pictureBox1.Image = (Bitmap)((new ImageConverter()).ConvertFrom(dt.Rows[0]["Pic"]));
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("خطایی در انتخاب رکورد رخ داده است");
            }
        }
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = op.FileName;
            }
        }
        private void frmSarbarg_Load(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            path = gds.GetDataSource();
            con.ConnectionString = @"" + path + "";
            display();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.Close();
            string query = "Insert Into tblSarbarg (Sarbarg,ShomareHamrah,ShomareSabet,Address,Pic) Values (@Sarbar,@ShomareHamrah,@ShomareSabet,@Address,@Pic)";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@Sarbar", txtSarbarg.Text);
            com.Parameters.AddWithValue("@ShomareHamrah", txtHamrah.Text);
            com.Parameters.AddWithValue("@ShomareSabet", txtSabet.Text);
            com.Parameters.AddWithValue("@Address", txtAddress.Text);
            com.Parameters.AddWithValue("@Pic", ImageToByte(pictureBox1.Image));
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtAddress.Text == "" || txtSarbarg.Text == "" )
            {
                MessageBox.Show("لطفا فیلد های ستاره دار را پر کنید");
            }
            else
            {
                var result = MessageBox.Show("آیا مایل به ویرایش رکورد هستتید؟", "هشدار", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    con.Close();
                    //try
                    //{
                    cmd.Parameters.Clear();
                        cmd.Connection = con;
                        cmd.CommandText = "UPDATE tblSarbarg set Sarbarg=N'" + txtSarbarg.Text +
                            "' ,ShomareHamrah=N'" + txtHamrah.Text +
                            "' ,ShomareSabet=N'" + txtSabet.Text +
                            "' ,Address=N'" + txtAddress.Text +
                            "' ,Pic= @p where SarbargID=" + 1;
                    cmd.Parameters.AddWithValue("@p", ImageToByte(pictureBox1.Image));
                    con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        display();
                        MessageBox.Show("ویرایش اطلاعات باموفقیت انجام شد.");
                    //}
                    //catch (Exception)
                    //{
                    //    MessageBox.Show("خطایی در ثبت رکورد رخ داده است");
                    //}
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
