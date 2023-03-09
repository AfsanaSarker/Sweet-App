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
    public partial class frmStudent : Form
    {
        SqlConnection con = new SqlConnection("Data Source=BDPNT-AZMAN;Initial Catalog=AzDB;Trusted_connection=True");
        public frmStudent()
        {
            InitializeComponent();
        }

        private void frmStudent_Load(object sender, EventArgs e)
        {
            LoadCombo();
            LoadGrid();
        }

        private void LoadCombo()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT courseId,courseName FROM course", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            cmbCourse.DataSource = dt;
            cmbCourse.DisplayMember = "courseName";
            cmbCourse.ValueMember = "courseId";
            con.Close();
        }

        private void LoadGrid()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT s.id, s.name as 'Name',s.dob as 'Date of Birth',s.email as 'Email',c.courseName as 'Course' FROM students s INNER JOIN course c ON s.courseId=c.courseId order by s.id", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            
            SqlCommand cmd = new SqlCommand("INSERT INTO students VALUES('" + txtName.Text + "','" + txtEmail.Text + "','" + dateTimePicker1.Value + "','" + cmbGender.SelectedItem.ToString() + "','" + txtContact.Text + "'," + cmbCourse.SelectedValue + ")", con);
            con.Open();
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data inserted successfully!!!";
            con.Close();
            LoadGrid();
            ClearAll();
        }

        private void ClearAll()
        {
            txtName.Clear();
            txtEmail.Clear();
            txtContact.Clear();
            txtName.Clear();
            cmbCourse.SelectedIndex = -1;
            cmbGender.SelectedIndex = -1;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[students] WHERE id="+txtSearch.Text+"", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["name"].ToString();
                txtEmail.Text = dt.Rows[0]["email"].ToString();
                txtContact.Text = dt.Rows[0]["contactNo"].ToString();
                cmbGender.SelectedItem = dt.Rows[0]["gender"].ToString();
                dateTimePicker1.Text= dt.Rows[0]["dob"].ToString();
                cmbCourse.SelectedValue = dt.Rows[0]["courseId"].ToString();
            }
            con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE students SET name='"+txtName.Text+"',email='"+txtEmail.Text+"',dob='"+dateTimePicker1.Value+"',gender='"+cmbGender.SelectedItem.ToString()+"',contactNo='"+txtContact.Text+"',courseId='"+cmbCourse.SelectedValue+"' WHERE id="+txtSearch.Text+"";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            lblMsg.Text = "Data Updated successfully!!";
            LoadGrid();
            ClearAll();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DELETE FROM students WHERE id="+txtSearch.Text+"";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            lblMsg.Text = "Data Deleted successfully!!";
            LoadGrid();
            ClearAll();
        }
    }
}
