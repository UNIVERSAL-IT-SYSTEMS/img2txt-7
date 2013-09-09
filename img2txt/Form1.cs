using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace img2txt
{
    public partial class Form1 : Form
    {
        char[] charGradient = { '#', '@', '0', 'O', 'C', 'o', 'c', ':', '.', ' ' };
        StringBuilder sb = new StringBuilder();
        Bitmap image;
        bool fileLoaded = false;

        List<string> fonts = new List<string>();

        public Form1()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            foreach (FontFamily font in System.Drawing.FontFamily.Families)
            {
                this.toolStripComboBox1.Items.Add(font.Name);
            }
            this.toolStripComboBox1.Text = "Consolas";
            this.textColors.Text = "Branco no Preto";
            this.toolStripComboBox2.Text = "#@0OCoc:. ";
            
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.Text = "img2ascii";
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            sb.Clear();
            txtView.Text = "";
            imgPreview.Image = null;
            fileLoaded = false;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            openImage();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void loadImage()
        {
            sb.Clear();

            image = new Bitmap(openFileDialog1.FileName);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    int intensity = (image.GetPixel(x, y).R + image.GetPixel(x, y).G + image.GetPixel(x, y).B);
                    sb.Append(charGradient[(int)Utils.Remap(intensity, 0, 755, 0, charGradient.Length - 1)]);
                }
                sb.Append(Environment.NewLine);
            }


        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            loadImage();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label1.Text = openFileDialog1.FileName;
            label2.Text = image.Width + "x" + image.Height + ", " + image.PixelFormat;
            label3.Text = "" + image.Tag;
            imgPreview.Image = image;
            txtView.Text = sb.ToString();
            this.toolStripProgressBar1.MarqueeAnimationSpeed = 0;
            this.toolStripProgressBar1.Visible = false;
            this.toolStripStatusLabel1.Text = "Pronto";
            this.Text = "img2txt - " + openFileDialog1.FileName;
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtView.Font = new Font(this.toolStripComboBox1.Text, 10);
        }

        private void toolStripComboBox1_TextUpdate(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox2_TextUpdate(object sender, EventArgs e)
        {

        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            sb.Clear();
            txtView.Text = "";
            imgPreview.Image = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveImage();
        }

        private void openImage() 
        {            
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sb.Clear();
                txtView.Text = "";
                this.toolStripStatusLabel1.Text = "Carregando " + openFileDialog1.FileName + "...";
                this.toolStripProgressBar1.Visible = true;
                this.toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
                this.toolStripProgressBar1.MarqueeAnimationSpeed = 30;
                fileLoaded = true;
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void saveImage()
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveFileDialog1.FileName, sb.ToString());
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openImage();
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveImage();
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutForm frm = new aboutForm();
            frm.Show();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void textColors_TextChanged(object sender, EventArgs e)
        {
            switch (textColors.Text)
            {
                case "Branco no Preto":
                    txtView.ForeColor = Color.White;
                    txtView.BackColor = Color.Black;
                    break;

                case "Preto no Branco":
                    txtView.ForeColor = Color.Black;
                    txtView.BackColor = Color.White;
                    break;

                case "H4xx0r":
                    txtView.ForeColor = Color.Green;
                    txtView.BackColor = Color.Black;
                    break;
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripComboBox2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox2_TextUpdate_1(object sender, EventArgs e)
        {
            
        }

        private void toolStripComboBox2_TextChanged(object sender, EventArgs e)
        {
            charGradient = this.toolStripComboBox2.Text.ToCharArray();
            sb.Clear();
            if (fileLoaded)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        int intensity = image.GetPixel(x, y).R + image.GetPixel(x, y).G + image.GetPixel(x, y).B;
                        if (charGradient.Length == 0)
                        {

                        }
                        else
                        {
                            sb.Append(charGradient[(int)Utils.Remap(intensity, 0, 755, 0, charGradient.Length - 1)]);
                        }
                    }
                    sb.Append(Environment.NewLine);
                }
                txtView.Text = sb.ToString();
            }
        }

        private void btnSave_ButtonClick(object sender, EventArgs e)
        {

        }

        private void salvarComoTextoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveFileDialog1.FileName, "<html><body>" + sb.ToString() + "</body></html>");
            }
            Console.WriteLine("HUA");
        }


    }
}
