using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace pengkar_laura
{
    public partial class Form2 : Form
    {
        private ReportDocument DokumenLaporan;
        public Form2()
        {
            InitializeComponent();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string reportPath = @"E:\TugasDestop\Project_destop_laura\pengkar_laura\LAPORAN ABSENSI_(V1-LAPORAN).rpt";

            try
            {
                if (!File.Exists(reportPath))
                {
                    MessageBox.Show(
                        "File report tidak ditemukan :\n" + reportPath,
                        "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DokumenLaporan = new ReportDocument();
                DokumenLaporan.Load(reportPath);

                crystalReportViewer1.ReportSource = DokumenLaporan;
                crystalReportViewer1.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gagal menampilkan report:\n" + ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                if(DokumenLaporan != null)
                {
                    DokumenLaporan.Close();
                    DokumenLaporan.Dispose();
                    DokumenLaporan = null;
                }
            }
        }
    }
}
