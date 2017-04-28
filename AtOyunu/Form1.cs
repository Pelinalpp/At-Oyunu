using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YSOdev3AtOyunu
{
    public partial class frmAtGezdir : Form
    {
        public frmAtGezdir()
        {
            InitializeComponent();
        }

        Button[,] labirent = new Button[0,0];
        int deger = 0, son = 0, mod = 0, sayac = 1, adIntX, adIntY;
        string[,] koordinat = new string[8, 2];
        string adX = "", adY = "";

        private void btnOlustur_Click(object sender, EventArgs e)
        {
            if (lbBoyutSec.SelectedItem == "3 x 3")
                mod = 3;
            else if (lbBoyutSec.SelectedItem == "4 x 4")
                mod = 4;
            else if (lbBoyutSec.SelectedItem == "5 x 5")
                mod = 5;
            else if (lbBoyutSec.SelectedItem == "6 x 6")
                mod = 6;
            else if (lbBoyutSec.SelectedItem == "7 x 7")
                mod = 7;
            else if (lbBoyutSec.SelectedItem == "8 x 8")
                mod = 8;
            else if (lbBoyutSec.SelectedItem == "9 x 9")
                mod = 9;
            else
            {
                mod = 0;
                MessageBox.Show("Seçim yapın.");
            }
            
            labirent = new Button[mod, mod];
            int i, j;
            for (i = 0; i < mod; i++)
            {
                for (j = 0; j < mod; j++)
                {
                    labirent[i, j] = new Button();
                    labirent[i, j].Width = 30;
                    labirent[i, j].Height = 30;
                    labirent[i, j].Left = 30 + (i * 30);
                    labirent[i, j].Top = 30 + (j * 30);
                    labirent[i, j].Name = (i).ToString() + "-" + (j).ToString();
                    labirent[i, j].ForeColor = Color.RosyBrown;
                    if ((i + j) % 2 == 0) labirent[i, j].BackColor = Color.Honeydew;
                    else labirent[i, j].BackColor = Color.Bisque;

                    labirent[i, j].Click += new EventHandler(Tikla);
                    this.Controls.Add(labirent[i, j]);
                }
            }
        }

        private void Tikla(object sender, EventArgs e)
        {
            ((Button)sender).Text = sayac.ToString();
            lblSkor.Text = sayac.ToString();
            sayac++;

            for (int i = 0; i < mod; i++)
            {
                for (int j = 0; j < mod; j++)
                {
                    if ((i + j) % 2 == 0) labirent[i, j].BackColor = Color.Honeydew;
                    else labirent[i, j].BackColor = Color.Bisque;
                }
            }

            adX = (((Button)sender).Name).Split('-')[1];
            adY = (((Button)sender).Name).Split('-')[0];
            adIntX = Convert.ToInt32(adX);
            adIntY = Convert.ToInt32(adY);

            KoordinatBul(adIntX, adIntY);
            KonumGosterKontrol();
            KonumGoster();

            OyunSonMu();
        }

        public void KoordinatBul(int adIntX, int adIntY)
        {
            koordinat[0, 0] = (adIntX - 2).ToString();
            koordinat[0, 1] = (adIntY - 1).ToString();
            koordinat[1, 0] = (adIntX - 2).ToString();
            koordinat[1, 1] = (adIntY + 1).ToString();
            koordinat[2, 0] = (adIntX - 1).ToString();
            koordinat[2, 1] = (adIntY + 2).ToString();
            koordinat[3, 0] = (adIntX + 1).ToString();
            koordinat[3, 1] = (adIntY + 2).ToString();
            koordinat[4, 0] = (adIntX + 2).ToString();
            koordinat[4, 1] = (adIntY + 1).ToString();
            koordinat[5, 0] = (adIntX + 2).ToString();
            koordinat[5, 1] = (adIntY - 1).ToString();
            koordinat[6, 0] = (adIntX + 1).ToString();
            koordinat[6, 1] = (adIntY - 2).ToString();
            koordinat[7, 0] = (adIntX - 1).ToString();
            koordinat[7, 1] = (adIntY - 2).ToString();
        }

        public void KonumGosterKontrol()
        {
            for (int i = 0; i < 8; i++)
            {
                int X = Convert.ToInt32(koordinat[i, 0]);
                int Y = Convert.ToInt32(koordinat[i, 1]);
                if (X < 0 || X >= mod)
                    koordinat[i, 0] = "boyama";
                if (Y < 0 || Y >= mod)
                    koordinat[i, 0] = "boyama";
            }

            for (int i = 0; i < mod; i++)
            {
                for (int j = 0; j < mod; j++)
                {
                    labirent[i, j].Enabled = false;
                }
            }
        }

        public void KonumGoster()
        {
            for (int i = 0; i < 8; i++)
            {
                if (koordinat[i, 0] != "boyama")
                {
                    if (labirent[Convert.ToInt32(koordinat[i, 1]), Convert.ToInt32(koordinat[i, 0])].Text == "")
                    {
                        labirent[Convert.ToInt32(koordinat[i, 1]), Convert.ToInt32(koordinat[i, 0])].BackColor = Color.LightCoral;
                        labirent[Convert.ToInt32(koordinat[i, 1]), Convert.ToInt32(koordinat[i, 0])].Enabled = true;
                    }
                }
            }
        }

        public void OyunSonMu()
        {
            int son = 0;
            for (int i = 0; i < mod; i++)
            {
                for (int j = 0; j < mod; j++)
                {
                    if (labirent[i, j].BackColor == Color.LightCoral)
                        son = 1;
                }
            }
            if (son == 0)
                MessageBox.Show("Oyun bitti!" + Environment.NewLine + "Skorunuz : " + lblSkor.Text);
        }
    }
}
