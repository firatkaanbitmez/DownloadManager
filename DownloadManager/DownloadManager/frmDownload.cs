using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadManager
{
    public partial class frmDownload : Form
    {
        public frmDownload(frmMain frm)
        {
            InitializeComponent();
            frmMain1 = frm;
        }
        
        
        WebClient client; //
        public string Url { get; set; } //GET metodu veri okunduğu zaman
        public string FileName { get; set; }// SET metodu ise veri yazıldığı zaman  yürütülür
        public double FileSize { get; set; }
        public double Percentage { get; set; }
        private frmMain frmMain1;

        private void frmDownload_Load(object sender, EventArgs e)
        {
            client = new WebClient();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            txbAddress.Text = Url; //texbox içindeki URL
            txbPath.Text = Properties.Settings.Default.Path;// Proje özelliklerinde Settings oluştur
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Database.FilesRow row = App.DB.Files.NewFilesRow(); //db newfilew satır açma
            row.Url = Url;
            row.FileName = FileName;//db deki satır içeriklerimiz ve biçim ifadeleri
            row.FileSize = (string.Format("{0:0.##} KB", FileSize / 1024));
            row.DateTime = DateTime.Now;
            App.DB.Files.AddFilesRow(row); //db ye save
            App.DB.AcceptChanges();
            App.DB.WriteXml(string.Format("{0}/data.dat", Application.StartupPath));
            ListViewItem item = new ListViewItem(row.Id.ToString()); //PK Id ile listview
            item.SubItems.Add(row.Url);
            item.SubItems.Add(row.FileName);
            item.SubItems.Add(row.FileSize);
            item.SubItems.Add(row.DateTime.ToLongDateString());
            frmMain1.listView1.Items.Add(item);
            this.Close();
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Minimum = 0; //progressbar tanımlamaları 
            double receive = double.Parse(e.BytesReceived.ToString());
            FileSize = double.Parse(e.TotalBytesToReceive.ToString());
            Percentage = receive / FileSize * 100; //yüzdelik gösterge 
            lbStatus.Text = $" % {string.Format("{0:0.##}",Percentage)}"; 
            progressBar1.Value = int.Parse(Math.Truncate(Percentage).ToString()); // yüzdeliği İntegral temelli math çözümler
            progressBar1.Update();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Uri uri = new Uri(this.Url);  //Download Start
            FileName = System.IO.Path.GetFileName(uri.AbsolutePath); //Filenam proje özellikleri
            client.DownloadFileAsync(uri, Properties.Settings.Default.Path + "/" + FileName);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            client.CancelAsync(); //Download Stop // Bağlantı koptuğunda çözüm için kalıcı bişey bul!
        }

        private void btnBrowse_Click(object sender, EventArgs e)// standart dosya yolu belirlemek için dialog penceris
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Select your path." })
            {
                if(fbd.ShowDialog() == DialogResult.OK)
                {
                    txbPath.Text = fbd.SelectedPath;
                    Properties.Settings.Default.Path = txbPath.Text;
                    Properties.Settings.Default.Save();
                }
            }
        }
    }
}
