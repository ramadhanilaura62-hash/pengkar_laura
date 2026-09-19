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
    public partial class Ftransaksi : Form
    {
        public Ftransaksi()
        {
            InitializeComponent();

            cmbkar.DropDown += cmbkar_DropDown;
            cmbbln.DropDown += cmbbln_DropDown;
            cmbyr.DropDown += cmbyr_DropDown;
        }

        // ==========================================
        // BERSIHKAN INPUT
        // ==========================================
        private void bersih()
        {
            cmbkar.Text = "";
            cmbbln.Text = "";
            cmbyr.Text = "";

            txtg.Text = "";
            txtj.Text = "";
            txtl.Text = "";
            txtp.Text = "";
            txtgj.Text = "";

            label3.Text = "";

            tm.Value = DateTime.Now;
        }

        // ==========================================
        // ISI COMBOBOX KARYAWAN
        // ==========================================
        private void isiKaryawan()
        {
            cmbkar.Items.Clear();

            if (koneksi.DS != null)
            {
                koneksi.DS.Clear();
            }

            koneksi.CRUD(
                "SELECT id_karyawan, nama_karyawan FROM tkaryawan"
            );

            if (koneksi.DS != null &&
                koneksi.DS.Tables.Count > 0)
            {
                foreach (DataRow baris in koneksi.DS.Tables[0].Rows)
                {
                    string id =
                        baris["id_karyawan"].ToString();

                    string nama =
                        baris["nama_karyawan"].ToString();

                    cmbkar.Items.Add(
                        id + " - " + nama
                    );
                }
            }
        }

        // ==========================================
        // ISI COMBOBOX BULAN
        // ==========================================
        private void isiBulan()
        {
            cmbbln.Items.Clear();

            cmbbln.Items.Add("Januari");
            cmbbln.Items.Add("Februari");
            cmbbln.Items.Add("Maret");
            cmbbln.Items.Add("April");
            cmbbln.Items.Add("Mei");
            cmbbln.Items.Add("Juni");
            cmbbln.Items.Add("Juli");
            cmbbln.Items.Add("Agustus");
            cmbbln.Items.Add("September");
            cmbbln.Items.Add("Oktober");
            cmbbln.Items.Add("November");
            cmbbln.Items.Add("Desember");
        }

        // ==========================================
        // ISI COMBOBOX TAHUN
        // ==========================================
        private void isiTahun()
        {
            cmbyr.Items.Clear();

            cmbyr.Items.Add("2024");
            cmbyr.Items.Add("2025");
            cmbyr.Items.Add("2026");
            cmbyr.Items.Add("2027");
            cmbyr.Items.Add("2028");
        }

        // ==========================================
        // DROPDOWN KARYAWAN
        // ==========================================
        private void cmbkar_DropDown(
            object sender,
            EventArgs e)
        {
            isiKaryawan();
        }

        // ==========================================
        // DROPDOWN BULAN
        // ==========================================
        private void cmbbln_DropDown(
            object sender,
            EventArgs e)
        {
            isiBulan();
        }

        // ==========================================
        // DROPDOWN TAHUN
        // ==========================================
        private void cmbyr_DropDown(
            object sender,
            EventArgs e)
        {
            isiTahun();
        }

        // ==========================================
        // AMBIL DATA KARYAWAN DAN JABATAN
        // ==========================================
        private void ambilDataKaryawan()
        {
            if (cmbkar.Text == "")
            {
                return;
            }

            string idKaryawan =
                cmbkar.Text.Split('-')[0].Trim();

            if (koneksi.DS != null)
            {
                koneksi.DS.Clear();
            }

            koneksi.CRUD(
                $"SELECT tkaryawan.id_karyawan, " +
                $"tkaryawan.nama_karyawan, " +
                $"tjabatan.nama_jabatan, " +
                $"tjabatan.gaji_pokok, " +
                $"tjabatan.tunjangan " +
                $"FROM tkaryawan " +
                $"JOIN tjabatan " +
                $"ON tkaryawan.id_jabatan = tjabatan.id_jabatan " +
                $"WHERE tkaryawan.id_karyawan = '{idKaryawan}'"
            );

            if (koneksi.DS != null &&
                koneksi.DS.Tables.Count > 0)
            {
                foreach (DataRow baris in koneksi.DS.Tables[0].Rows)
                {
                    txtg.Text =
                        baris["gaji_pokok"].ToString();

                    txtj.Text =
                        baris["tunjangan"].ToString();
                }
            }

            hitungLembur();
            hitungGaji();
        }

        // ==========================================
        // HITUNG LEMBUR
        // ==========================================
        private void hitungLembur()
        {
            if (cmbkar.Text == "" ||
                cmbbln.Text == "" ||
                cmbyr.Text == "")
            {
                txtl.Text = "0";
                return;
            }

            string idKaryawan =
                cmbkar.Text.Split('-')[0].Trim();

            int bulan =
                cmbbln.SelectedIndex + 1;

            string tahun =
                cmbyr.Text;

            if (bulan <= 0)
            {
                txtl.Text = "0";
                return;
            }

            if (koneksi.DS != null)
            {
                koneksi.DS.Clear();
            }

            koneksi.CRUD(
                $"SELECT SUM(lembur) AS total_lembur " +
                $"FROM tabsensi " +
                $"WHERE id_karyawan = '{idKaryawan}' " +
                $"AND MONTH(tanggal) = '{bulan}' " +
                $"AND YEAR(tanggal) = '{tahun}'"
            );

            if (koneksi.DS != null &&
                koneksi.DS.Tables.Count > 0 &&
                koneksi.DS.Tables[0].Rows.Count > 0)
            {
                object hasil =
                    koneksi.DS.Tables[0]
                    .Rows[0]["total_lembur"];

                if (hasil != DBNull.Value)
                {
                    double totalJam =
                        Convert.ToDouble(hasil);

                    double tarifLembur = 25000;

                    double uangLembur =
                        totalJam * tarifLembur;

                    txtl.Text =
                        uangLembur.ToString();
                }
                else
                {
                    txtl.Text = "0";
                }
            }
            else
            {
                txtl.Text = "0";
            }
        }

        // ==========================================
        // HITUNG GAJI BERSIH
        // ==========================================
        private void hitungGaji()
        {
            double gajiPokok = 0;
            double tunjangan = 0;
            double lembur = 0;
            double potongan = 0;

            double.TryParse(
                txtg.Text,
                out gajiPokok
            );

            double.TryParse(
                txtj.Text,
                out tunjangan
            );

            double.TryParse(
                txtl.Text,
                out lembur
            );

            double.TryParse(
                txtp.Text,
                out potongan
            );

            double gajiBersih =
                gajiPokok +
                tunjangan +
                lembur -
                potongan;

            txtgj.Text =
                gajiBersih.ToString();
        }

        // ==========================================
        // TAMPIL DATA PENGGAJIAN
        // ==========================================
        private void tampilData()
        {
            guna2DataGridView1.Rows.Clear();

            if (koneksi.DS != null)
            {
                koneksi.DS.Clear();
            }

            koneksi.CRUD(
                "SELECT tpenggajian.*, " +
                "tkaryawan.nama_karyawan " +
                "FROM tpenggajian " +
                "JOIN tkaryawan " +
                "ON tpenggajian.id_karyawan = " +
                "tkaryawan.id_karyawan"
            );

            if (koneksi.DS != null &&
                koneksi.DS.Tables.Count > 0)
            {
                foreach (DataRow baris in
                         koneksi.DS.Tables[0].Rows)
                {
                    string id =
                        "" + baris["id_penggajian"];

                    string idkaryawan =
                        "" + baris["id_karyawan"];

                    string nama =
                        "" + baris["nama_karyawan"];

                    string periode =
                        baris["periode_bulan"] +
                        "/" +
                        baris["periode_tahun"];

                    string gp =
                        "" + baris["gaji_pokok"];

                    string tunjangan =
                        "" + baris["tunjangan"];

                    string lembur =
                        "" + baris["lembur"];

                    string potongan =
                        "" + baris["potongan"];

                    string gajibersih =
                        "" + baris["gaji_bersih"];

                    string tanggal =
                        "" + baris["tanggal_proses"];

                    guna2DataGridView1.Rows.Add(
                        id,
                        idkaryawan,
                        nama,
                        periode,
                        gp,
                        tunjangan,
                        lembur,
                        potongan,
                        gajibersih,
                        tanggal
                    );
                }
            }
        }

        // ==========================================
        // PILIH KARYAWAN
        // ==========================================
        private void cmbkar_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            ambilDataKaryawan();
        }

        // ==========================================
        // PILIH BULAN
        // ==========================================
        private void cmbbln_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            hitungLembur();
            hitungGaji();
        }

        // ==========================================
        // PILIH TAHUN
        // ==========================================
        private void cmbyr_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            hitungLembur();
            hitungGaji();
        }

        // ==========================================
        // SIMPAN DATA
        // ==========================================
        private void guna2Button16_Click(
            object sender,
            EventArgs e)
        {
            if (cmbkar.Text == "" ||
                cmbbln.Text == "" ||
                cmbyr.Text == "" ||
                txtg.Text == "")
            {
                MessageBox.Show(
                    "Lengkapi data"
                );
            }
            else
            {
                string idKaryawan =
                    cmbkar.Text
                    .Split('-')[0]
                    .Trim();

                string bulan =
                    (cmbbln.SelectedIndex + 1)
                    .ToString();

                string tahun =
                    cmbyr.Text;

                string gp =
                    txtg.Text;

                string tunjangan =
                    txtj.Text;

                string lembur =
                    txtl.Text;

                string potongan =
                    txtp.Text;

                string gajibersih =
                    txtgj.Text;

                string tanggal =
                    tm.Value
                    .ToString("yyyy-MM-dd");

                koneksi.CRUD(
                    $"INSERT INTO tpenggajian " +
                    $"(id_karyawan, periode_bulan, " +
                    $"periode_tahun, gaji_pokok, " +
                    $"tunjangan, lembur, potongan, " +
                    $"gaji_bersih, tanggal_proses) " +
                    $"VALUES " +
                    $"('{idKaryawan}', '{bulan}', " +
                    $"'{tahun}', '{gp}', " +
                    $"'{tunjangan}', '{lembur}', " +
                    $"'{potongan}', '{gajibersih}', " +
                    $"'{tanggal}')"
                );

                bersih();
                tampilData();

                MessageBox.Show(
                    "Data penggajian berhasil disimpan!"
                );
            }
        }

        // ==========================================
        // UBAH DATA
        // ==========================================
        private void guna2Button15_Click(
            object sender,
            EventArgs e)
        {
            string idp =
                label3.Text;

            if (idp == "")
            {
                MessageBox.Show(
                    "Pilih data yang ingin diubah!"
                );

                return;
            }

            if (cmbkar.Text == "" ||
                cmbbln.Text == "" ||
                cmbyr.Text == "")
            {
                MessageBox.Show(
                    "Lengkapi data!"
                );

                return;
            }

            string idKaryawan =
                cmbkar.Text
                .Split('-')[0]
                .Trim();

            string bulan =
                (cmbbln.SelectedIndex + 1)
                .ToString();

            string tahun =
                cmbyr.Text;

            string gp =
                txtg.Text;

            string tunjangan =
                txtj.Text;

            string lembur =
                txtl.Text;

            string potongan =
                txtp.Text;

            string gajibersih =
                txtgj.Text;

            string tanggal =
                tm.Value
                .ToString("yyyy-MM-dd");

            koneksi.CRUD(
                $"UPDATE tpenggajian SET " +
                $"id_karyawan = '{idKaryawan}', " +
                $"periode_bulan = '{bulan}', " +
                $"periode_tahun = '{tahun}', " +
                $"gaji_pokok = '{gp}', " +
                $"tunjangan = '{tunjangan}', " +
                $"lembur = '{lembur}', " +
                $"potongan = '{potongan}', " +
                $"gaji_bersih = '{gajibersih}', " +
                $"tanggal_proses = '{tanggal}' " +
                $"WHERE id_penggajian = '{idp}'"
            );

            bersih();
            tampilData();

            MessageBox.Show(
                "Data penggajian berhasil diubah!"
            );
        }

        // ==========================================
        // TOMBOL TAMPIL DATA
        // ==========================================
        private void guna2Button13_Click(
            object sender,
            EventArgs e)
        {
            tampilData();
        }

        // ==========================================
        // KLIK DATA GRID
        // ==========================================
        private void guna2DataGridView1_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int baris =
                    e.RowIndex;

                if (guna2DataGridView1
                    .Rows[baris]
                    .Cells[0]
                    .Value == null)
                {
                    return;
                }

                string id =
                    guna2DataGridView1
                    .Rows[baris]
                    .Cells[0]
                    .Value
                    .ToString();

                label3.Text =
                    id;

                // Pastikan ComboBox sudah terisi
                isiKaryawan();
                isiBulan();
                isiTahun();

                if (koneksi.DS != null)
                {
                    koneksi.DS.Clear();
                }

                koneksi.CRUD(
                    $"SELECT * FROM tpenggajian " +
                    $"WHERE id_penggajian = '{id}'"
                );

                if (koneksi.DS != null &&
                    koneksi.DS.Tables.Count > 0)
                {
                    foreach (DataRow brs in
                             koneksi.DS.Tables[0].Rows)
                    {
                        string idKaryawan =
                            "" + brs["id_karyawan"];

                        string bulan =
                            "" + brs["periode_bulan"];

                        string tahun =
                            "" + brs["periode_tahun"];

                        // Pilih karyawan
                        for (int i = 0;
                             i < cmbkar.Items.Count;
                             i++)
                        {
                            if (cmbkar
                                .Items[i]
                                .ToString()
                                .StartsWith(
                                    idKaryawan + " - "))
                            {
                                cmbkar.SelectedIndex =
                                    i;

                                break;
                            }
                        }

                        // Pilih bulan
                        int nomorBulan =
                            Convert.ToInt32(bulan);

                        if (nomorBulan >= 1 &&
                            nomorBulan <= 12)
                        {
                            cmbbln.SelectedIndex =
                                nomorBulan - 1;
                        }

                        // Pilih tahun
                        cmbyr.Text =
                            tahun;

                        txtg.Text =
                            "" + brs["gaji_pokok"];

                        txtj.Text =
                            "" + brs["tunjangan"];

                        txtl.Text =
                            "" + brs["lembur"];

                        txtp.Text =
                            "" + brs["potongan"];

                        txtgj.Text =
                            "" + brs["gaji_bersih"];

                        if (DateTime.TryParse(
                            "" + brs["tanggal_proses"],
                            out DateTime tanggal))
                        {
                            tm.Value =
                                tanggal;
                        }
                    }
                }
            }
        }

        // ==========================================
        // EVENT KOSONG DARI DESIGNER
        // ==========================================
        private void label16_Click(
            object sender,
            EventArgs e)
        {
        }

        private void label11_Click(
            object sender,
            EventArgs e)
        {
        }

        private void label20_Click(
            object sender,
            EventArgs e)
        {
        }

        private void label17_Click(
            object sender,
            EventArgs e)
        {
        }

        private void label18_Click(
            object sender,
            EventArgs e)
        {
        }

        private void label19_Click(
            object sender,
            EventArgs e)
        {
        }

        private void guna2DateTimePicker1_ValueChanged(
            object sender,
            EventArgs e)
        {
        }

        private void label21_Click(
            object sender,
            EventArgs e)
        {
        }

        private void guna2TextBox4_TextChanged(
            object sender,
            EventArgs e)
        {
        }
    }
}