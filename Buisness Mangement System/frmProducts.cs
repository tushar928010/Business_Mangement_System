using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Buisness_Mangement_System
{
    public partial class frmProducts : Form
    {
        string conn = "Data Source=.;Initial Catalog=Business;Integrated Security=True;Encrypt=False";
        bool sidebarExpand;

        public frmProducts()
        {
            InitializeComponent();
          
        }


        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Business;Integrated Security=True;Encrypt=False");
        SqlCommand cmd = new SqlCommand();

        SqlDataAdapter sda;
        public void LoadGrid()
        {
            DataSet ds = new DataSet();
            //con.Open();
            sda = new SqlDataAdapter("select * from  tbl_Products", con);
            sda.Fill(ds);
            dgvProdusctsEntry.DataSource = ds.Tables[0];
            //con.Close();
        }
        private bool IsUsernameExists(string Product_Name)
        {
            // SQL query to check if username exists
            string query = "SELECT COUNT(*) FROM tbl_products WHERE Product_Name= @Product_Name";

            using (SqlConnection connection = new SqlConnection(conn))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@Product_Name", Product_Name);

                    // Open connection
                    connection.Open();

                    // Execute query
                    int count = (int)command.ExecuteScalar();

                    // Check if count is greater than 0 (username exists)
                    return count > 0;
                }
            }
        }




        private void btnSAve_Click(object sender, EventArgs e)
        {


            string Product_Name = txtProduct_Name.Text.Trim();

            // Check if username already exists
            if (IsUsernameExists(Product_Name))
            {
                MessageBox.Show(" Product Name already exists. Please choose a different one.");
                return;
            }


            try
            {

                SqlParameter p1 = new SqlParameter("@Date", SqlDbType.Date);
                p1.Value =Convert.ToDateTime(dtpProductEntry.Value.ToShortDateString());

                SqlParameter p2 = new SqlParameter("@Product_Name", SqlDbType.VarChar);
                p2.Value = txtProduct_Name.Text.ToUpper().Trim();

                SqlParameter p3 = new SqlParameter("@Brand_Name", SqlDbType.VarChar);
                p3.Value = txtBrand.Text.ToUpper().Trim();

                SqlParameter p4 = new SqlParameter("@Wholesale_cost", SqlDbType.Int);
                p4.Value = NupWholeSalePrice.Text.ToUpper().Trim();

                SqlParameter p5 = new SqlParameter("@Selling_cost", SqlDbType.Int);
                p5.Value = NupSellingPrice.Text.ToUpper().Trim();


                // step 5
                cmd.Parameters.Clear();
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);


                //step 6 
                cmd.Connection = con;

                // step 7 
                cmd.CommandText = "insert into tbl_Products(Date,Product_Name,Brand_Name,WholeSale_cost,Selling_Cost) Values (@Date,@Product_Name,@Brand_Name,@WholeSale_cost,@Selling_Cost)";

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

        private void MenuButton_Click(object sender, EventArgs e)
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

        private void Sidebartimer_Tick(object sender, EventArgs e)
        {
            Sidebartimer.Start();
        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }

        private void btnEntryOfStock_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmEntryOfStock frmEntryOfStock = new frmEntryOfStock();
            frmEntryOfStock.Show();
        }

        private void btnNew_transaction_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmNewTransaction billing = new frmNewTransaction();
            billing.Show();
        }

        private void BtnSuppiler_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSupplier supplier = new frmSupplier();
            supplier.Show();
        }

        private void btnAddCategoru_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmaddCategory frmaddCategory = new frmaddCategory();
            frmaddCategory.Show();
        }

        private void BtnEntryOFProducts(object sender, EventArgs e)
        {
            this.Hide();
            frmProducts frmProducts = new frmProducts();
            frmProducts.Show();
        }

        public int ID;

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete the Record", "Delete Record ", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);


                if (dr == DialogResult.Yes)
                {

                    //step 6 
                    cmd.Connection = con;

                    // step 7 
                    cmd.CommandText = "delete from tbl_Products where Product_id = " + ID;

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

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {


                SqlParameter p1 = new SqlParameter("@Product_Name", SqlDbType.VarChar);
                p1.Value = txtProduct_Name.Text.ToUpper().Trim();

                SqlParameter p2 = new SqlParameter("@Date", SqlDbType.Date);
                p2.Value = Convert.ToDateTime(dtpProductEntry.Value.ToShortDateString());

                SqlParameter p3 = new SqlParameter("@Brand_Name", SqlDbType.VarChar);
                p3.Value = txtBrand.Text.ToUpper().Trim();

                SqlParameter p4 = new SqlParameter("@Wholesale_cost", SqlDbType.Int);
                p4.Value = NupWholeSalePrice.Text.ToUpper().Trim();

                SqlParameter p5 = new SqlParameter("@Selling_cost", SqlDbType.Int);
                p5.Value = NupSellingPrice.Text.ToUpper().Trim();




                // step 5
                cmd.Parameters.Clear();
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);



                //step 6 
                cmd.Connection = con;

                // step 7 
                cmd.CommandText = "update tbl_Products set Product_Name = @Product_Name,Brand_Name = @Brand_Name,WholeSale_cost = @WholeSale_cost,Selling_Cost = @Selling_Cost   where Product_id =  " + ID;

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


        private void DgvProdusctsEntry_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            ID = Convert.ToInt32(dgvProdusctsEntry.Rows[e.RowIndex].Cells[0].Value.ToString());

            dtpProductEntry.Value = Convert.ToDateTime(dgvProdusctsEntry.Rows[e.RowIndex].Cells[1].Value.ToString());

            txtProduct_Name.Text = dgvProdusctsEntry.Rows[e.RowIndex].Cells[2].Value.ToString();


            txtBrand.Text = dgvProdusctsEntry.Rows[e.RowIndex].Cells[3].Value.ToString();

            NupWholeSalePrice.Text = dgvProdusctsEntry.Rows[e.RowIndex].Cells[4].Value.ToString();

            NupSellingPrice.Text = dgvProdusctsEntry.Rows[e.RowIndex].Cells[5].Value.ToString();


        }

        private void FrmProducts_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }

        private void Pboxusersetting_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmRegistation frmRegistation = new frmRegistation();
            frmRegistation.Show();
        }
    }
}
