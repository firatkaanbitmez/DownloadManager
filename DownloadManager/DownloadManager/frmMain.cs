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
using System.Net;
using System.Diagnostics;
using System.Threading;
using System.Net.NetworkInformation;



namespace DownloadManager
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)// Add URL form buton
        {
            using (frmAddUrl frm = new frmAddUrl())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    frmDownload frmDownload = new frmDownload(this);
                    frmDownload.Url = frm.Url;
                    frmDownload.Show();
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e) //Remove butonnn
        {    
            if (MessageBox.Show("Are you sure want to delete this record ?", "Task Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = listView1.SelectedItems.Count; i > 0; i--)
                {
                    ListViewItem item = listView1.SelectedItems[i - 1];
                    App.DB.Files.Rows[item.Index].Delete(); // DBden ve Listview1 yani listemizden siliyoruz
                    listView1.Items[item.Index].Remove();
                }
                
                App.DB.AcceptChanges(); //DB değişikleri kaydediyoruz
                App.DB.WriteXml(string.Format("{0}/data.dat", Application.StartupPath));
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)// Setting Form butonu 
        {
            using (frmSetting frm = new frmSetting()) 
            {
                frm.ShowDialog();
            }
        }
        private void toolStripButton4_Click(object sender, EventArgs e) // Silent Install
        {
            if (MessageBox.Show("Are you sure want to Install this record ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string winrar;
                winrar = @"C:\Users\FIRAT\Desktop\winrar-x64-602tr.exe";
                System.Diagnostics.Process instalmovav = System.Diagnostics.Process.Start(winrar, "/S /V / qn");
                MessageBox.Show("Winrar Installation is Completed. ");
                // Dinamik düzgün çalışmıyor

               // System.Diagnostics.Process silne = System.Diagnostics.Process.Start("/S /V / qn");
            }



        }

        private void toolStripButton5_Click(object sender, EventArgs e)// App-store Formu
        {
            using (frmStore frm = new frmStore())
            {
                frm.ShowDialog();
            }

        }


        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            string fileNames = string.Format("{0}/data.dat", Application.StartupPath);
            if (File.Exists(fileNames))
                App.DB.ReadXml(fileNames);
            foreach(Database.FilesRow  row in App.DB.Files)
            {
                ListViewItem item = new ListViewItem(row.Id.ToString());
                item.SubItems.Add(row.Url);
                item.SubItems.Add(row.FileName);
                item.SubItems.Add(row.FileSize);
                item.SubItems.Add(row.DateTime.ToLongDateString());
                listView1.Items.Add(item);
            }
        }

    
    }
}
        