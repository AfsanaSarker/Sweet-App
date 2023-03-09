using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace work_01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=BDPNT-AZMAN;Initial Catalog=AzDB;Trusted_connection=True");
            SqlCommand cmd = new SqlCommand("INSERT INTO course VALUES('"+txtCourseName.Text+"')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data inserted successfully!!!";
            LoadGrid();
            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            SqlConnection con = new SqlConnection("Data Source=BDPNT-AZMAN;Initial Catalog=AzDB;Trusted_connection=True");
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM course", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
    }
}
