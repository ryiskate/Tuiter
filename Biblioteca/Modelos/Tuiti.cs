using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biblioteca.Modelos
{
    public class Tuiti
    {
        public int Id {get; set;}
        public string Texto { get; set; }
        public DateTime DataDeCriacao { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
