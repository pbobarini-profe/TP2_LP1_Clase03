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

namespace TP2_LP1_Clase03
{
    public partial class Form1 : Form
    {
        int[,] tiros;
        int[] dadosReservados = new int[5] { 0, 0, 0, 0, 0 };
        int contador = 0;
        List<Opciones> jugada = new List<Opciones>();
        public Form1()
        {
            InitializeComponent();
            tiros = new int[3,5];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] dados = new int[5];
            Random random = new Random();
            if(flowLayoutPanel1.Controls.Count > 0)
            {
                ReservarDados();
            }
            if (contador < 3) {
                flowLayoutPanel1.Controls.Clear();
                for (int i = 0; i < 5; i++) {
                    int numero1;
                    if (dadosReservados[i] == 0)
                    {
                        numero1 = random.Next(1, 7);
                    }
                    else
                    {
                        numero1 = dadosReservados[i];
                    }
                    tiros[contador, i] = numero1;
                    dados[i] = numero1;
                    CargarCheck(numero1, contador);
                }
                CargarLabel(dados);
                if(contador == 2)
                {
                    button1.Text = "Cerrar";
                }
               contador++;
            }
            else
            {
                UltimoTiro();
                CargarOpciones();
                contador = 0;
                button2.Visible = true;
                Array.Clear(dadosReservados,0,dadosReservados.Length);
                button1.Text = "Tirar";
                button1.Visible = false;
            }
        }
        private void ReservarDados()
        {
            int posicion = 0;
            foreach (CheckBox control in flowLayoutPanel1.Controls)
            {
                if (control.Checked == true)
                {
                    dadosReservados[posicion] = (int)control.Tag;
                }
                else
                {
                    dadosReservados[posicion] = 0;
                }
                posicion++;
            }
        }
        private void UltimoTiro()
        {
            int posicion = 0;
            foreach (CheckBox control in flowLayoutPanel1.Controls)
            {
                dadosReservados[posicion] = (int)control.Tag;
                posicion++;
            }
            flowLayoutPanel4.Controls.Clear();
        }
        private void CargarOpciones()
        {
            flowLayoutPanel7.Controls.Clear();
            List<Opciones> combinaciones = new List<Opciones>();

            //agrupe dados por valor
            var valores = dadosReservados.GroupBy(x => x);
            //cuente cantidad de dados.
            var grupos = valores.Select(g => g.Count()).OrderByDescending(c => c).ToArray();

            //valores
            foreach (var valor in valores)
            {
                string nombre = valor.Key.ToString();
                int suma = valor.Sum();
                bool yalo = jugada.Any(o => o.opcion.Trim().Equals(nombre.Trim()));
                if (!yalo)
                {
                    combinaciones.Add(new Opciones(nombre, suma));
                }
            }

            //especiales
            int[] dadosOrdenados = dadosReservados.OrderBy(x => x).ToArray();
            if (grupos[0] == 5 && !jugada.Any(o => o.opcion.ToString().Trim().Equals("Generala")))
                combinaciones.Add(new Opciones("Generala", 50));
            if (grupos[0] == 4 && !jugada.Any(o => o.opcion.ToString().Trim() == "Poker"))
                combinaciones.Add(new Opciones("Poker", 40));
            if (grupos[0] == 3 && grupos[1] == 2 && !jugada.Any(o => o.opcion.ToString().Trim() == "Full"))
                combinaciones.Add(new Opciones("Full", 30));
            if (dadosOrdenados.SequenceEqual(new int[] { 1, 2, 3, 4, 5 }) ||
                     dadosOrdenados.SequenceEqual(new int[] { 2, 3, 4, 5, 6 }))
            if(!jugada.Any(o => o.opcion.ToString().Trim() == "Escalera"))
                    combinaciones.Add(new Opciones("Escalera", 20));

            foreach(Opciones opcion in combinaciones)
            {
                RadioButton check = new RadioButton
                {
                    Name = $"op{opcion.opcion}",
                    AutoSize = true,
                    Text = $"{opcion}",
                    Tag = opcion.valor
                };
                flowLayoutPanel7.Controls.Add(check);
            }
        }

        private void CargarLabel(int [] dados)
        {
            Label label = new Label
            {
                AutoSize = true,
                Text = ""
            };
            flowLayoutPanel4.Controls.Add(label);
            foreach(int dado in dados)
            {
                label.Text += $"{dado.ToString()}-";
            }
        }
        private void CargarCheck(int dado, int i)
        {
            string path="";
            switch (dado)
            {
                case 1:
                    path= Path.Combine(Application.StartupPath, "Images", "1.jpg");
                    break;
                case 2:
                    path = Path.Combine(Application.StartupPath, "Images", "2.jpg");
                    break;
                case 3:
                    path = Path.Combine(Application.StartupPath, "Images", "3.jpg");
                    break;
                case 4:
                    path = Path.Combine(Application.StartupPath, "Images", "4.jpg");
                    break;
                case 5:
                    path = Path.Combine(Application.StartupPath, "Images", "5.jpg");
                    break;
                case 6:
                    path = Path.Combine(Application.StartupPath, "Images", "6.jpg");
                    break;
            }
            CheckBox check = new CheckBox
            {
                Name = $"dado{i}",
                AutoSize = true,
                Image = Image.FromFile(path),
                Appearance = Appearance.Button,
                Tag = dado
            };
            flowLayoutPanel1.Controls.Add(check);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (jugada.Count <= 10)
            {

                dataGridView1.DataSource = null;
                bool agregado = false;
                foreach (RadioButton radio in flowLayoutPanel7.Controls)
                {
                    if (radio.Checked)
                    {
                        string op = radio.Text.Split('-')[0];
                        Opciones opcion = new Opciones(op, (int)radio.Tag);
                        jugada.Add(opcion);
                        jugada = jugada.OrderBy(o => o.opcion).ToList();
                        agregado = true;
                        break;
                    }
                }
                if (!agregado)
                {
                    Opciones opcion = new Opciones("Turno Perdido", 0);
                    jugada.Add(opcion);
                }
                dataGridView1.DataSource = jugada;
                flowLayoutPanel7.Controls.Clear();
                button1.Visible = true;
                button2.Visible = false;
            }
            else
            {
                MessageBox.Show($"Partida Terminada. Puntuacion = {jugada.Sum(o => o.valor)}");
                jugada.Clear();
                dataGridView1.DataSource = null;
                flowLayoutPanel7.Controls.Clear();
                flowLayoutPanel1.Controls.Clear();
                button1.Visible = true;
                button2.Visible = false;

            }
        }
    }
}
