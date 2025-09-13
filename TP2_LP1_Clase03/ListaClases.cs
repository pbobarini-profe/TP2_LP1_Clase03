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
    public partial class ListaClases : Form
    {
        public class Item
        {
            public string texto { get; set; }
            public int valor { get; set; }
            public override string ToString()
            {
                return texto;
            }
        }


        public int id;
        List<Persona> personas = new List<Persona>();
        public ListaClases()
        {
            InitializeComponent();
            id = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtNombre.Text != "" && txtApellido.Text != "")
            {
                Persona persona = new Persona();
                persona.id = id;
                persona.nombre = txtNombre.Text;
                persona.apellido = txtApellido.Text;
                persona.edad = (int)numericUpDown1.Value;
                personas.Add(persona);
                cargarListBox();
                id++;
                txtApellido.Text = string.Empty;
                txtNombre.Text = string.Empty;
                numericUpDown1.Value = 0;
            }
            else
            {
                MessageBox.Show("Complete los datos necesarios");
            }

        }

        private void cargarListBox()
        {
            listBox1.Items.Clear();
            foreach (Persona persona in personas) {
                listBox1.Items.Add(new Item { texto = $"{persona.apellido}, {persona.nombre}", valor = persona.id });
            }
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idSelecionado = 0;
            if(listBox1.SelectedItem is Item item)
            {
                idSelecionado = item.valor;
            }
            Persona personaSeleccionada = personas.FirstOrDefault(p => p.id == idSelecionado);
            txtApellido.Text = personaSeleccionada.apellido;
            txtNombre.Text = personaSeleccionada.nombre;
            numericUpDown1.Value = personaSeleccionada.edad;
            label1.Text = personaSeleccionada.id.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            personas.RemoveAll(persona => persona.id == int.Parse(label1.Text));
            cargarListBox();
            txtApellido.Text = string.Empty;
            txtNombre.Text = string.Empty;
            numericUpDown1.Value = 0;
        }
    }
}
