using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP2_LP1_Clase03
{
    public partial class Listas : Form
    {
        public List<string> nombres;
        public int idSeleccionado;
        public Listas()
        {
            InitializeComponent();
            nombres = new List<string>();
            idSeleccionado = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nombres.Add(textBox1.Text);
            MessageBox.Show("Elemento agregado");
            textBox1.Text = string.Empty;
            refrescarLabel();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            refrescarLabel();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            nombres.Clear();
            MessageBox.Show("Lista Vaciada");
            refrescarLabel();
        }
        private void refrescarLabel()
        {
            label1.Text = string.Empty;

            //Otra forma sin usar Linq
            //foreach (string s in nombres) {
            //    label1.Text += s + "\n";
            //}

            //Usando Expresion Lambda
            nombres.ForEach(nombre => label1.Text += nombre + "\n");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Forma con foreach
            //int contador = 0;
            //foreach (string nombre in nombres)
            //{
            //    if (nombre == textBox1.Text)
            //    {
            //        idSeleccionado = contador;
            //        MessageBox.Show($"La posicion es {idSeleccionado}");
            //        break;
            //    }
            //    contador++;
            //}

            //IndexOf

            //idSeleccionado = nombres.IndexOf(textBox1.Text);
            //if (nombres.IndexOf(textBox1.Text) != -1)
            //{
            //    MessageBox.Show($"La posicion es {idSeleccionado}");
            //}
            //else
            //{
            //    MessageBox.Show($"No hay elemento con ese nombre.");
            //}

            //FindIndex()

            int indice = nombres.FindIndex(nombre => nombre == textBox1.Text);
            if (indice != -1) {
                idSeleccionado = indice;
                MessageBox.Show($"La posicion es {idSeleccionado}");
            }
            else
            {
                MessageBox.Show($"No hay elemento con ese nombre.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            nombres[idSeleccionado] = textBox1.Text;
            MessageBox.Show($"Nombre actualizado.");
            refrescarLabel();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            nombres.Remove(textBox1.Text);
            MessageBox.Show($"Nombre eliminado.");
            refrescarLabel();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label1.Text = string.Empty;
            List<string> listaFiltrada = nombres.Where(nombre => nombre.StartsWith(textBox1.Text)).ToList();
            listaFiltrada.ForEach(nombre => label1.Text += nombre + "\n");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            List<int> cantidades = nombres.Select(nombre => nombre.Length).ToList();
            cantidades.ForEach(cantidad => label2.Text += cantidad + "\n");
        }
    }
}
