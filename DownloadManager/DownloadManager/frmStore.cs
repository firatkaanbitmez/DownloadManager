using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadManager
{
    public partial class frmStore : Form
    {
        public frmStore()
        {
            InitializeComponent();
        }

        private void frmStore_Load(object sender, EventArgs e)
        {
            

        }

        
        

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if(comboBox1.SelectedIndex == 0) {

                
                listBox1.Items.Clear();
                listBox1.Items.Add("Opera GX, özellikle oyuncular ");
                listBox1.Items.Add("için tasarlanmış özel");
                listBox1.Items.Add("bir Opera tarayıcı sürümüdür..");

            }

            if (comboBox1.SelectedIndex == 1)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("FIRAT KAAN BİTMEZ");
                
            }
            if (comboBox1.SelectedIndex == 2)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("FIRAT KAAN BİTMEZ");

            }

        }

       
    }
}

