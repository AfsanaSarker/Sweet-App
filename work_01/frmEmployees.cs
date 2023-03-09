using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace work_01
{
    public partial class frmEmployees : Form
    {
        SqlConnection con = new SqlConnection("Data Source=BDPNT-AZMAN;Initial Catalog=AzDB;Trusted_connection=True");
        public frmEmployees()
        {
            InitializeComponent();
        }

        private void frmEmployees_Load(object sender, EventArgs e)
        {
            LoadCombo();
            LoadGridData();
        }
        private void LoadGridData()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT e.id,e.name,e.basicSalary,d.departmentName,e.photo FROM employees e INNER JOIN department d ON e.departmentId=d.id", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }
        private void LoadCombo()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT id,departmentName FROM department", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            cmbDepartment.DataSource = dt;
            cmbDepartment.DisplayMember = "departmentName";
            cmbDepartment.ValueMember = "id";
            con.Close();
        }
        private void LoadComboForEdit()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT id,departmentName FROM department", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            cmbEditDept.DataSource = dt;
            cmbEditDept.DisplayMember = "departmentName";
            cmbEditDept.ValueMember = "id";
            con.Close();
        }
        private void btnPhoto_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);
                this.pictureBox1.Image = img;
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"INSERT INTO employees VALUES(@n,@j,@d,@s,@p)", con);
            cmd.Parameters.AddWithValue("@n", txtName.Text);
            cmd.Parameters.AddWithValue("@j", dateTimePicker1.Value.Date);
            cmd.Parameters.AddWithValue("@d", cmbDepartment.SelectedValue);
            cmd.Parameters.AddWithValue("@s", txtSalary.Text);
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            cmd.Parameters.AddWithValue("@p", ms.ToArray());
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data inserted successfully!!!");
            con.Close();
            LoadGridData();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            LoadComboForEdit();
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                int id = (int)this.dataGridView1.SelectedRows[0].Cells[0].Value;
                SqlCommand cmd = new SqlCommand("SELECT * FROM employees where id=@id",con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtEditId.Text = dr.GetInt32(0).ToString();
                    txtEditName.Text = dr.GetString(1);
                    dateTimePicker2.Value = dr.GetDateTime(2).Date;
                    cmbEditDept.SelectedValue= dr.GetInt32(3).ToString();
                    txtEditSalary.Text = dr.GetDecimal(4).ToString("0.00");
                    MemoryStream ms = new MemoryStream((byte[])dr[5]);
                    Image img = Image.FromStream(ms);
                    pictureBox2.Image = img;
                }
                con.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"UPDATE employees SET name=@n,joinDate=@j,departmentId=@d,basicSalary=@s,photo=@p WHERE id=@i", con);
            cmd.Parameters.AddWithValue("@i", txtEditId.Text);
            cmd.Parameters.AddWithValue("@n", txtEditName.Text);
            cmd.Parameters.AddWithValue("@j", dateTimePicker2.Value.Date);
            cmd.Parameters.AddWithValue("@d", cmbEditDept.SelectedValue);
            cmd.Parameters.AddWithValue("@s", txtEditSalary.Text);
            MemoryStream ms = new MemoryStream();
            pictureBox2.Image.Save(ms, pictureBox2.Image.RawFormat);
            cmd.Parameters.AddWithValue("@p", ms.ToArray());
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data Updated successfully!!!");
            con.Close();
            LoadGridData();
        }

        private void btnFileUploadForEdit_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);
                this.pictureBox2.Image = img;
            }
        }
    }
}
