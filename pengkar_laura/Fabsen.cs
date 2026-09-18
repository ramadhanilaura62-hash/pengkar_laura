using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.IO;

namespace pengkar_laura
{
    public partial class Fabsen : Form
    {
        public Fabsen()
        {
            InitializeComponent();
        }

        private void guna2NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2NumericUpDown1_Click(object sender, EventArgs e)
        {

        }

        private void bersih()
        {
            txtnm.Text = "";
            txtJK.Text = "";
            txtJM.Text = "";
            cmbstatus.Text = "";
            cmbket.Text = "";

            time.Value = DateTime.Now;
        }

        private void tampilData()
        {
            guna2DataGridView1.Rows.Clear();

            if (koneksi.DS != null)
            {
                koneksi.DS.Clear();
            }

            koneksi.CRUD("SELECT * from tabsensi");
            foreach (DataRow baris in koneksi.DS.Tables[0].Rows)
            {
                String id = "" + baris["id_absensi"];
                String nm = "" + baris["id_karyawan"];
                String bt_val = "" + baris["tanggal"];
                String th = "" + baris["jam_masuk"];
                String nj = "" + baris["jam_keluar"];
                String jl = "" + baris["status"];
                String ket = "" + baris["keterangan"];

                guna2DataGridView1.Rows.Add(id, nm, bt_val, th, nj, jl, ket);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtnm.Text == "" || txtJM.Text == "" || txtJK.Text == "" || cmbket.SelectedIndex == 0 || cmbstatus.SelectedIndex == 0)
            {
                MessageBox.Show("lengkapi data");
            }
            else
            {
                string NM = txtnm.Text;
                string NT = time.Value.ToString("yyyy-MM-dd");
                string th = txtJM.Text;
                string TM = txtJK.Text;
                string ket = cmbket.Text;
                string st = cmbstatus.Text;

                koneksi.CRUD($"INSERT INTO tabsensi VALUES(null,'{NM}','{NT}','{th}','{TM}','{st}','{ket}')");
                bersih();
                tampilData();
            }
        }

        private void guna2DataGridView1_DefaultCellStyleChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string idp = label3.Text;
            string NM = txtnm.Text;
            string NT = time.Value.ToString("yyyy-MM-dd");
            string th = txtJM.Text;
            string TM = txtJK.Text;
            string ket = cmbket.Text;
            string st = cmbstatus.Text;

            koneksi.CRUD($"UPDATE tabsensi SET id_karyawan ='{NM}', tanggal = '{NT}', jam_masuk = '{th}', jam_keluar ='{TM}', status = '{st}', keterangan = '{ket}' WHERE id_absensi ='{idp}'");
            bersih();
            tampilData();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int baris = e.RowIndex;
                int kolom = e.ColumnIndex;


                if (kolom == 7)
                {
                    string idpt = guna2DataGridView1.Rows[baris].Cells[0].Value.ToString();

                    if (koneksi.DS != null) koneksi.DS.Clear();

                    koneksi.CRUD($"SELECT * FROM tabsensi WHERE id_absensi = '{idpt}'");
                    foreach (DataRow brs in koneksi.DS.Tables[0].Rows)
                    {
                        String idpet = "" + brs["id_absensi"];
                        String NM = "" + brs["id_karyawan"];
                        String bu = "" + brs["tanggal"];
                        String nt = "" + brs["jam_masuk"];
                        String nj = "" + brs["jam_keluar"];
                        String st = "" + brs["status"];
                        String ket = "" + brs["keterangan"];

                        label3.Text = idpet;
                        txtnm.Text = NM;
                        txtJM.Text = nt;
                        txtJK.Text = nj;
                        cmbstatus.Text = st;
                        cmbket.Text = ket;


                        if (DateTime.TryParse(bu, out DateTime parsedDate))
                        {
                            time.Value = parsedDate;
                        }
                    }
                }

                if (kolom == 8)
                {
                    string idpt = guna2DataGridView1.Rows[baris].Cells[0].Value.ToString();
                    DialogResult setuju = MessageBox.Show("Hapus data?", "Pemberitahuan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (setuju == DialogResult.Yes)
                    {
                        koneksi.CRUD($"DELETE FROM tabsensi WHERE id_absensi = '{idpt}'");
                        bersih();
                        tampilData();
                    }
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            tampilData();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Excel Files|*.xlsx";
            openFileDialog.Title = "Pilih File Excel";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet(1);

                    foreach (var row in worksheet.RowsUsed().Skip(1))
                    {
                        string NM = row.Cell(2).Value.ToString();
                        string tanggal = row.Cell(3).GetDateTime().ToString("yyyy-MM-dd");
                        string jamMasuk = row.Cell(4).Value.ToString();
                        string jamKeluar = row.Cell(5).Value.ToString();
                        string st = row.Cell(6).Value.ToString();
                        string ket = row.Cell(7).Value.ToString();

                        koneksi.CRUD(
                            $"INSERT INTO tabsensi " +
                            $"(id_karyawan, tanggal, jam_masuk, jam_keluar, status, keterangan) " +
                            $"VALUES ('{NM}', '{tanggal}', '{jamMasuk}', '{jamKeluar}', '{st}', '{ket}')"
                        );
                    }
                }

                tampilData();

                MessageBox.Show("Data Excel berhasil diimport!");
            }
        }

        private void cmbstatus_DropDown(object sender, EventArgs e)
        {
            cmbstatus.Items.Clear();
            cmbstatus.Items.Add("Hadir");
            cmbstatus.Items.Add("Tidak Hadir");

        }

        

        private void cmbket_DropDown_1(object sender, EventArgs e)
        {
            cmbket.Items.Clear();
            cmbket.Items.Add("Sakit");
            cmbket.Items.Add("Izin");
            cmbket.Items.Add("--");
        }
    }
}
    
