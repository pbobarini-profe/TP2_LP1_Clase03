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
    public partial class Cartas : Form
    {
        private int[] valores = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        private string[] palos = { "Espada", "Basto", "Copas", "Oro" };
        
        public Cartas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            string[] cartasBarajadas = new string[3];
            string[] posiciones = new string[3];
            for(int i = 0; i<3; i++)
            {
                int posicionValor=13;
                int posicionPalo=4;
                bool generar = true;
                
                while (generar) {
                    if(posicionValor ==13 && posicionPalo == 4)
                    {
                        posicionValor = random.Next(valores.Length);
                        posicionPalo = random.Next(palos.Length);
                        posiciones[i] = posicionValor.ToString() + posicionPalo.ToString();
                        if(i == 0)
                        {
                            generar = false;
                        }
                        if (i == 1 && posiciones[0] != posiciones[1]) {
                            generar = false;
                        }
                        if (i == 2 && posiciones[0] != posiciones[2] && posiciones[1] != posiciones[2])
                        {
                            generar = false;
                        }
                    }
                }
                int valorAleatorio = valores[posicionValor];
                string paloAleatorio = palos[posicionPalo];
                
                cartasBarajadas[i] = $"{valorAleatorio} de {paloAleatorio}";
                switch (i)
                {
                    case 0:
                        label1.Text = paloAleatorio;
                        label2.Text = valorAleatorio.ToString();
                        break;
                    case 1:
                        label3.Text = paloAleatorio;
                        label4.Text = valorAleatorio.ToString();
                        break;
                    case 2:
                        label5.Text = paloAleatorio;
                        label6.Text = valorAleatorio.ToString();
                        break;
                }
            }

        }
    }
}
