using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_LP1_Clase03
{
    public class Opciones
    {
        public string opcion { get; set; }
        public int valor { get; set; }
        public Opciones(string op, int val)
        {
            opcion = op;
            valor = val;
        }
        public override string ToString()
        {
            return $"{opcion} - {valor}";
        }
    }
}
