using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pengkar_laura
{
    public partial class Fdetail : Form
    {
        public Fdetail()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void bersih()
        {
            txtnm.Text = "";
            txtidpeng.Text = "";
            txtj.Text = "";
            txtjum.Text = "";
            label3.Text = "";
        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void tampilData()
        {
            guna2DataGridView1.Rows.Clear();

            if (koneksi.DS != null)
            {
                koneksi.DS.Clear();
            }

            koneksi.CRUD("SELECT * from detail_penggajian");
            foreach (DataRow baris in koneksi.DS.Tables[0].Rows)
            {
                String id = "" + baris["id_detail"];
                String nm = "" + baris["id_penggajian"];
                String bt_val = "" + baris["nama_komponen"];
                String th = "" + baris["jenis"];
                String nj = "" + baris["jumlah"];
                

                guna2DataGridView1.Rows.Add(id, nm, bt_val, th, nj);
            }

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            if (txtnm.Text == "" || txtjum.Text == "" || txtj.Text == "" || txtidpeng.Text == "")
            {
                MessageBox.Show("lengkapi data");
            }
            else
            {
               
             
                string th = txtjum.Text;
                string TM = txtidpeng.Text;
                string ket = txtj.Text;
                string st = txtnm.Text;

                koneksi.CRUD($"INSERT INTO detail_penggajian VALUES(null,'{TM}','{st}','{TM}','{ket}','{th}')");
                bersih();
                tampilData();
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            string th = txtjum.Text;
            string TM = txtidpeng.Text;
            string ket = txtj.Text;
            string st = txtnm.Text;
            string id = label3.Text;

            koneksi.CRUD($"UPDATE detail_penggajian SET id_penggajian = '{TM}', nama_komponen = '{st}', jenis ='{ket}', jumlah = '{th}'WHERE id_detail ='{id}'");
            bersih();
            tampilData();
        }
    }
}
