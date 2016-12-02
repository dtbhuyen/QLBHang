using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
namespace Bai10
{
    public partial class frmOrder : Form
    {
        string cnStr = "";
        SqlConnection cn;
        DataSet ds;
        DataTable Orders;
        public frmOrder()
        {
            InitializeComponent();
        }

        private void frmOrder_Load(object sender, EventArgs e)
        {
            cnStr = ConfigurationManager.ConnectionStrings["cnStr"].ConnectionString;
            cn = new SqlConnection(cnStr);

            //string sql = "SELECT * FROM CTHD";
            //dgvOrderDetail.DataSource = GetCustomerDataset().Tables[0];

            string sql = "SELECT * FROM HoaDon";
            Orders = GetCustomerDataset(sql).Tables[0];

            //Binding du lieu cho ComboBox
            cboOrderID.DataSource = Orders;
            cboOrderID.DisplayMember = "MaHD";
            cboOrderID.ValueMember = "MaHD";

            txtEmployeeID.DataBindings.Add("Text", Orders, "MaNV");
            txtCustomerID.DataBindings.Add("Text", Orders, "MaKH");

            dtpOrderDate.DataBindings.Add("Text", Orders, "NgayLapHD");
            dtpShippedDate.DataBindings.Add("Text", Orders, "NgayGiaoHang");
        }
        public DataSet GetCustomerDataset(string sql)
        {
            try
            {
                //sql = "SELECT * FROM CTHD";
                SqlDataAdapter da = new SqlDataAdapter(sql, cn);
                /*DataSet*/
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

            finally
            {
                cn.Close();
            }
        }

        private void cboOrderID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM CTHD WHERE MaHD = '" + cboOrderID.Text + "'";
            dgvOrderDetail.DataSource = GetCustomerDataset(sql).Tables[0];
        }
    }
}
