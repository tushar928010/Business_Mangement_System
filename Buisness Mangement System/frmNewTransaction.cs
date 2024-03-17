using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


namespace Buisness_Mangement_System
{
    public partial class frmNewTransaction : Form
    {
        string conn = "Data Source=.;Initial Catalog=Business;Integrated Security=True";

        bool sidebarExpand;

        public frmNewTransaction()
        {
            InitializeComponent();

            PopulateComboBox();

        }


        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Business;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();

        private string connectionString = "Data Source=.;Initial Catalog=Business;Integrated Security=True";


        SqlDataAdapter sda;

        public void LoadGrid()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string query = "select * from grid";
            sda = new SqlDataAdapter(query, con);
            sda.Fill(ds);
            DgvNewTransaction.DataSource = ds.Tables[0];

        }


        public void LoadData()
        {
            SqlCommand cmd = new SqlCommand("Select * From grid", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DgvNewTransaction.DataSource = dt;
            DgvNewTransaction.AllowUserToAddRows = false;
            DataGridViewCheckBoxColumn checkboxcol = new DataGridViewCheckBoxColumn();
            checkboxcol.Width = 40;
            checkboxcol.Name = "check1";
            checkboxcol.HeaderText = "CheckToAdd";
            DgvNewTransaction.Columns.Insert(0, checkboxcol);
        }

        private void Billing_Load(object sender, EventArgs e)
        {
            LoadGrid();
            timer1.Start();
            LoadData();


        }

        private void LblTimer_Click(object sender, EventArgs e)
        {
            lblTimer.Text = DateTime.Now.ToString("hh:mm:ss  tt");
        }



        private void Sidebartimer_Tick(object sender, EventArgs e)
        {

            if (sidebarExpand)
            {
                sidebar.Width -= 150;

                if (sidebar.Width == sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    Sidebartimer.Stop();
                }
            }
            else
            {
                sidebar.Width += 150;
                if (sidebar.Width == sidebar.MinimumSize.Width) ;
                sidebarExpand = true;
                Sidebartimer.Stop();

            }


        }

        private void PboxDashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }

        private void Pboxnew_transaction(object sender, EventArgs e)
        {
            this.Hide();
            frmEntryOfStock form = new frmEntryOfStock();
            form.Show();
        }

        private void PboxBilling(object sender, EventArgs e)
        {
            this.Hide();
            frmNewTransaction bill = new frmNewTransaction();
            bill.Show();
        }

        private void Pboxusersetting_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmRegistation frmRegistation = new frmRegistation();
            frmRegistation.Show();
        }

        private void MenuButton_Click_1(object sender, EventArgs e)
        {
            Sidebartimer.Start();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSuppiler_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSupplier supplier = new frmSupplier();
            supplier.Show();
        }

        private void PboxSupplier(object sender, EventArgs e)
        {

            frmSupplier supplier = new frmSupplier();
            supplier.Show();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmaddCategory frmaddCategoriescs = new frmaddCategory();
            frmaddCategoriescs.Show();
        }

        private void PboxAddCategory(object sender, EventArgs e)
        {
            frmaddCategory categoriescs = new frmaddCategory();
            categoriescs.Show();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }

        private void New_Transaction_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            frmEntryOfStock form1 = new frmEntryOfStock();
            form1.Show();
        }

        private void btnNewTransaction_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            frmNewTransaction billing = new frmNewTransaction();
            billing.Show();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmProducts frmProducts = new frmProducts();
            frmProducts.Show();
        }

        private void Pboxaddcate_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmaddCategory frmaddCategory = new frmaddCategory();
            frmaddCategory.Show();
        }

        private void PopulateComboBox()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Business;Integrated Security=True"))
                {
                    // SQL query to retrieve data from the table
                    string query = "SELECT Supplier_Name FROM tbl_Supplier";
                    string query2 = "SELECT Category_Name FROM tbl_Category";
                    string query3 = "SELECT Product_Name FROM tbl_Products";


                    // Create a SqlDataAdapter to execute the query and fill the DataTable
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    SqlDataAdapter adapter2 = new SqlDataAdapter(query2, connection);
                    SqlDataAdapter adapter3 = new SqlDataAdapter(query3, connection);

                    DataTable dataTable1 = new DataTable();
                    DataTable dataTable2 = new DataTable();
                    DataTable dataTable3 = new DataTable();

                    // Open the connection and fill the DataTable
                    connection.Open();
                    adapter.Fill(dataTable1);
                    adapter2.Fill(dataTable2);
                    adapter3.Fill(dataTable3);

                    // Close the connection
                    connection.Close();

                    // Bind the DataTable to the ComboBox

                    CBoxCategory.DataSource = dataTable2;
                    CBoxCategory.DisplayMember = "Category_Name";

                    CBoxProductName.DataSource = dataTable3;
                    CBoxProductName.DisplayMember = "Product_Name";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

 


        private void BtnAdd_Click_1(object sender, EventArgs e)
        {
            
            try
            {
                SqlParameter p1 = new SqlParameter("@Customer_Name", SqlDbType.VarChar);
                p1.Value = txtCustomer_Name.Text.Trim();

                SqlParameter p2 = new SqlParameter("@Date", SqlDbType.Date);
                p2.Value = Convert.ToDateTime(DtpNewTransaction.Value.ToShortDateString());

                SqlParameter p3 = new SqlParameter("@Product_Name", SqlDbType.VarChar);
                p3.Value = CBoxProductName.Text.ToUpper().Trim();

                SqlParameter p4 = new SqlParameter("Category_Name", SqlDbType.VarChar);
                p4.Value = CBoxCategory.Text.ToUpper().Trim();

                SqlParameter p5 = new SqlParameter("@Price", SqlDbType.Int);
                p5.Value = Convert.ToInt32(txtPrice.Text.ToString());

                SqlParameter p6 = new SqlParameter("@Product_Qty", SqlDbType.Int);
                p6.Value = Convert.ToInt32(NupProduct_Qty.Text.ToUpper().Trim());

                //SqlParameter p7 = new SqlParameter("@Taxx", SqlDbType.Int);
                //p7.Value = Convert.ToInt32(CBoxTaxx.Text.ToUpper().Trim());

                SqlParameter p7= new SqlParameter("@Discount", SqlDbType.Int);
                p7.Value = Convert.ToInt32(CBoxDiscout.SelectedItem.ToString());

                SqlParameter p8= new SqlParameter("@Customer_Contactno", SqlDbType.BigInt);
                p8.Value = Convert.ToInt64(txtCustomerContactNo.Text.ToUpper().Trim());

                SqlParameter p9 = new SqlParameter("@Total", SqlDbType.Int);
                p9.Value = Convert.ToInt32(txtTotal.Text.ToUpper().Trim());

                SqlParameter p10 = new SqlParameter("@Payment", SqlDbType.Float);
                p10.Value = (txtPayment.Text.ToUpper().Trim());






                // step 5
                cmd.Parameters.Clear();
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);
                cmd.Parameters.Add(p6);
                cmd.Parameters.Add(p7);
                cmd.Parameters.Add(p8);
                cmd.Parameters.Add(p9);
                cmd.Parameters.Add(p10);
         


                //step 6 
                cmd.Connection = con;

                // step 7 
                cmd.CommandText = "insert into grid (Customer_Name,Date,Product_Name,Category_Name,Price,Product_Qty,Discount,Customer_Contactno,Total,Payment) Values (@Customer_Name,@Date,@Product_Name,@Category_Name,@Price,@Product_Qty,@Discount,@Customer_Contactno,@Total,@Payment)";

                //step 8

                con.Open();

                //step 9
                cmd.ExecuteNonQuery();


                con.Close();
                MessageBox.Show("Add successfully....");
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                LoadGrid();
            }

        }

        private void PboxaddProduct_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmProducts frmProducts = new frmProducts();
            frmProducts.Show();
        }


        private void BtnDelete_Click_1(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete the Record", "Delete Record ", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);


                if (dr == DialogResult.Yes)
                {

                    //step 6 
                    cmd.Connection = con;

                    // step 7 
                    cmd.CommandText = "delete from grid where Unique_id = " + ID;

                    //step 8

                    con.Open();

                    //step 9
                    cmd.ExecuteNonQuery();

                    //step 1

                    con.Close();
                    MessageBox.Show("Delete record....");
                    LoadGrid();
                }
                else
                {
                    LoadGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                LoadGrid();
            }

        }
        public int ID;
    

        private void dgvNewTransaction_Click(object sender, DataGridViewCellEventArgs e)
        {
            btnColumnAdd.PerformClick();

            if (DgvNewTransaction != null && e.RowIndex >= 1 && e.RowIndex < DgvNewTransaction.Rows.Count)
            {
                // Ensure the cell is not null
                DataGridViewCell cell = DgvNewTransaction.Rows[e.RowIndex].Cells[1];
                if (cell != null && cell.Value != null)
                {
                    ID = Convert.ToInt32(cell.Value.ToString());
                }
            }

            //ID = Convert.ToInt32(DgvNewTransaction.Rows[e.RowIndex].Cells[0].Value.ToString());

            txtCustomer_Name.Text = DgvNewTransaction.Rows[e.RowIndex].Cells[2].Value.ToString();

            DtpNewTransaction.Value = Convert.ToDateTime(DgvNewTransaction.Rows[e.RowIndex].Cells[3].Value.ToString());

            CBoxProductName.Text = DgvNewTransaction.Rows[e.RowIndex].Cells[5].Value.ToString();

            CBoxCategory.Text = DgvNewTransaction.Rows[e.RowIndex].Cells[4].Value.ToString();


            txtPrice.Text = DgvNewTransaction.Rows[e.RowIndex].Cells[6].Value.ToString();

            NupProduct_Qty.Text = DgvNewTransaction.Rows[e.RowIndex].Cells[7].Value.ToString();

            //CBoxTaxx.Text = DgvNewTransaction.Rows[e.RowIndex].Cells[8].Value.ToString();

            CBoxDiscout.Text = DgvNewTransaction.Rows[e.RowIndex].Cells[8].Value.ToString();

            txtCustomerContactNo.Text = DgvNewTransaction.Rows[e.RowIndex].Cells[9].Value.ToString();

            txtTotal.Text = DgvNewTransaction.Rows[e.RowIndex].Cells[10].Value.ToString();

            txtPayment.Text = DgvNewTransaction.Rows[e.RowIndex].Cells[11].Value.ToString();


        }

        private void BtnUpdatee_Click_1(object sender, EventArgs e)
        {
            try
            {

                SqlParameter p1 = new SqlParameter("@Customer_Name", SqlDbType.VarChar);
                p1.Value = txtCustomer_Name.Text.Trim();

                SqlParameter p2 = new SqlParameter("@Date", SqlDbType.Date);
                p2.Value = Convert.ToDateTime(DtpNewTransaction.Value.ToShortDateString());

                SqlParameter p3 = new SqlParameter("@Product_Name", SqlDbType.VarChar);
                p3.Value = CBoxProductName.Text.ToUpper().Trim();

                SqlParameter p4 = new SqlParameter("Category_Name", SqlDbType.VarChar);
                p4.Value = CBoxCategory.Text.ToUpper().Trim();

                SqlParameter p5 = new SqlParameter("@Price", SqlDbType.Int);
                p5.Value = Convert.ToInt32(txtPrice.Text.ToString());

                SqlParameter p6 = new SqlParameter("@Product_Qty", SqlDbType.Int);
                p6.Value = Convert.ToInt32(NupProduct_Qty.Text.ToUpper().Trim());

                //SqlParameter p7 = new SqlParameter("@Taxx", SqlDbType.Int);
                //p7.Value = Convert.ToInt32(CBoxTaxx.Text.ToUpper().Trim());

                SqlParameter p7 = new SqlParameter("@Discount", SqlDbType.Int);
                p7.Value = Convert.ToInt32(CBoxDiscout.SelectedItem.ToString());

                SqlParameter p8 = new SqlParameter("@Customer_Contactno", SqlDbType.BigInt);
                p8.Value = Convert.ToInt64(txtCustomerContactNo.Text.ToUpper().Trim());

                SqlParameter p9 = new SqlParameter("@Total", SqlDbType.Int);
                p9.Value = Convert.ToInt32(txtTotal.Text.ToUpper().Trim());

                SqlParameter p10 = new SqlParameter("@Payment", SqlDbType.Float);
                p10.Value = (txtPayment.Text.ToUpper().Trim());


                // step 5
                cmd.Parameters.Clear();
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);
                cmd.Parameters.Add(p6);
                cmd.Parameters.Add(p7);
                cmd.Parameters.Add(p8);
                cmd.Parameters.Add(p9);
                cmd.Parameters.Add(p10);

                //step 6 
                cmd.Connection = con;

                // step 7 
                cmd.CommandText = "update grid set Customer_Name = @Customer_Name,Date = @Date,Product_Name = @Product_Name,Price = @Price,Product_Qty = @Product_Qty,Discount = @Discount,Customer_Contactno = @Customer_Contactno,Total = @Total,Payment = @Payment  where Unique_id =  " + ID;
                //step 8

                con.Open();

                //step 9
                cmd.ExecuteNonQuery();


                con.Close();
                MessageBox.Show("Update successfully....");
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                LoadGrid();
            }

        }

        private void Timer1_Tick_1(object sender, EventArgs e)
        {
            lblTimer.Text = DateTime.Now.ToString("hh:mm:ss  tt");
        }

        private void TxtSerachBox_TextChanged_1(object sender, EventArgs e)
        {
            string Name = txtSerachBox.Text.ToString().Trim();
            DataSet ds = new DataSet();
            string query = " select * from grid where Customer_Contactno like '" + Name + "%'";
            sda = new SqlDataAdapter(query, con);
            sda.Fill(ds);
            DgvNewTransaction.DataSource = ds.Tables[0];
        }



        private void BtnCalculate_Click_1(object sender, EventArgs e)
        {
            int id, qty;

            int row = 0;

            row = DgvNewTransaction.Rows.Count - 2;

            float Price, Total, Payment, Discount;

            qty = int.Parse(NupProduct_Qty.Text);

            Price = float.Parse(txtPrice.Text);

            Total = qty * Price;

            txtTotal.Text = "" + Total;

            Discount = (CBoxDiscout.SelectedIndex);

            if (CBoxDiscout.SelectedIndex == 1)
            {
                Discount = (Total * 5) / 100;
            }
            else if (CBoxDiscout.SelectedIndex == 2)
            {
                Discount = (Total * 10) / 100;
            }
            else
            {
                Discount = 0;
            }

            Payment = Total - Discount;

            txtPayment.Text = "" + Payment;

        }

        public void SelectedRowTotal()
        {
            double sum = 0;

            for (int i = 0; i < DgvNewTransaction.Rows.Count; i++)
            {
                if (Convert.ToBoolean(DgvNewTransaction.Rows[i].Cells[0].Value) == true)
                {
                    sum += double.Parse(DgvNewTransaction.Rows[i].Cells[11].Value.ToString());
                }

                txtSumColumn.Text = sum.ToString();
            }
        }


        private void BtnAddColumn_Click_1(object sender, EventArgs e)
        {
            SelectedRowTotal();



        }

        private void LblTotalWithDiscount_Click(object sender, EventArgs e)
        {

        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {

            string n = Interaction.InputBox("Enter Your Mobile Number", "Advance Teaching");
            if (n != string.Empty)
            {
                SqlConnection connection = new SqlConnection("Data Source =.; Initial Catalog = Business; Integrated Security = True");
                string query = "SELECT * FROM grid WHERE Customer_Contactno =" + n;
                SqlCommand command = new SqlCommand(query, connection);
                DataTable data = new DataTable();
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                data.Load(reader);

                if (data.Rows.Count > 0)
                {
                    ReportViewForm reportview = new ReportViewForm();
                    string apppath = Application.StartupPath;
                    string reportpath = @"GetByIdReport.rpt";
                    string fullpath = Path.Combine(apppath, reportpath);

                    reportview.ReportName = fullpath;
                    reportview.ReportData = data;
                    reportview.Show();
                }
                else
                {
                    MessageBox.Show("Record not Found", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                connection.Close();
            }
            else
            {
                MessageBox.Show("Please Enter Your Mobile Number", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {

            //ReportViewForm reportView = new ReportViewForm();
            //GetByIdReport getByIdReport = new GetByIdReport();
            //TextObject total = (TextObject)getByIdReport.ReportDefinition.Sections["Section4"].ReportObjects["txtTotal"];
            //total.Text = txtTotal.Text;

            //reportView.crystalReportViewer1.ReportSource = getByIdReport;
            //reportView.Show();

        }
        private void PopulateCategoryComboBox()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Category_Name FROM tbl_Category";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CBoxCategory.Items.Add(reader["Category_Name"].ToString());
                }
            }
        }


        private void CBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            string Country = CBoxCategory.SelectedItem.ToString();
            switch (Country)
            {
                case "SAREE":
                    CBoxProductName.Items.Add("Maharashtra");
                    CBoxProductName.Items.Add("Maharashtra");
                    CBoxProductName.Items.Add("Maharashtra");
                    CBoxProductName.Items.Add("Maharashtra");
                    CBoxProductName.Items.Add("Maharashtra");
                    break;
            
                    



            }
        }

        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void TxtSumColumn_TextChanged(object sender, EventArgs e)
        {

        }

        private void LblTotalRs_Click(object sender, EventArgs e)
        {

        }
    }
}

































