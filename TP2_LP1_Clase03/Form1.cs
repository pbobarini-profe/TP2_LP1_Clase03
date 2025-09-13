using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP2_LP1_Clase03
{
    public partial class Form1 : Form
    {
        string[] tiros;
        int contador = 0;
        public Form1()
        {
            InitializeComponent();
            tiros = new string[3];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            if (contador < 3) {
                string tiro = string.Empty;
                for (int i = 0; i < 5; i++) {
                    int numero1 = random.Next(1,6);
                    tiro += $"{numero1.ToString()}-";
                }
                label1.Text += tiro + "\n";
                tiros[contador] = tiro;
                contador++;
            }
            else
            {
                MessageBox.Show("Ya hizo los 3 tiros permitidos");
            }
        }
    }
}
