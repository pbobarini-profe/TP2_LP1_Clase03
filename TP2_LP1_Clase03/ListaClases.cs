using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP2_LP1_Clase03
{
    public partial class ListaClases : Form
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["TP2_LP1_Clase03.Properties.Settings.cadena"].ConnectionString;
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
                //personas.Add(persona);
                //cargarListBox();
                id++;
                txtApellido.Text = string.Empty;
                txtNombre.Text = string.Empty;
                numericUpDown1.Value = 0;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //string sql = $"insert into Personas (nombre, apellido, edad) Values (@nombre,@apellido,@edad)";
                    
                    string sql = $"insert into Personas (nombre, apellido, edad) Values ('{persona.nombre}','{persona.apellido}','{persona.edad}')";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    
                    //cmd.Parameters.AddWithValue("@nombre", persona.nombre);
                    //cmd.Parameters.AddWithValue("@apellido", persona.apellido);
                    //cmd.Parameters.AddWithValue("@edad", persona.edad);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                Actualizar();
            }
            else
            {
                MessageBox.Show("Complete los datos necesarios");
            }

        }
        private void actualizarGrilla()
        {
            personas.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "select * from Personas";
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        Persona persona = new Persona
                        {
                            id= reader.GetInt32(0),
                            nombre = reader.GetString(1),
                            apellido = reader.GetString(2),
                            edad = reader.GetInt32(3)
                        };
                        personas.Add(persona);
                    }
                }
            }
        }
        private void cargarListBox()
        {
            listBox1.Items.Clear();
            foreach (Persona persona in personas) {
                listBox1.Items.Add(persona);
            }
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Persona personaSeleccionada = (Persona)listBox1.SelectedItem;
            txtApellido.Text = personaSeleccionada.apellido;
            txtNombre.Text = personaSeleccionada.nombre;
            numericUpDown1.Value = personaSeleccionada.edad;
            label1.Text = personaSeleccionada.id.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int idSeleccionado = int.Parse(label1.Text);
            Persona personaSeleccionada = personas.FirstOrDefault(x => x.id == idSeleccionado);
            personaSeleccionada.apellido = txtApellido.Text;
            personaSeleccionada.nombre = txtNombre.Text;
            personaSeleccionada.edad = (int)numericUpDown1.Value;
            //cargarListBox();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $@"update Personas SET
                    nombre = '{personaSeleccionada.nombre}',
                    apellido = '{personaSeleccionada.apellido}',
                    edad = '{personaSeleccionada.edad}'
                    Where id = '{personaSeleccionada.id}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            Actualizar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //personas.RemoveAll(persona => persona.id == int.Parse(label1.Text));
            //cargarListBox();
            int idSeleccionado = int.Parse(label1.Text);
            txtApellido.Text = string.Empty;
            txtNombre.Text = string.Empty;
            numericUpDown1.Value = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"delete from personas where id = {idSeleccionado}";
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            Actualizar();
        }
        private void Actualizar()
        {
            actualizarGrilla();
            cargarListBox();
            personaBindingSource.DataSource = null;
            personaBindingSource.DataSource = personas;
        }
        private void ListaClases_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla '_TP2_LP1_202402DataSet.Personas' Puede moverla o quitarla según sea necesario.
            this.personasTableAdapter.Fill(this._TP2_LP1_202402DataSet.Personas);
            Actualizar();

        }
    }
}
