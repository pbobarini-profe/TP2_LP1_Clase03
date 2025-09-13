using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_LP1_Clase03
{
    public class Persona
    {
        public int id { get; set; }
        public string nombre {  get; set; }
        public string apellido { get; set; }
        public int edad { get; set; }
        public override string ToString()
        {
            return $"{nombre},{apellido}-{edad}";
        }
    }
}
