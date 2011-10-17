using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Modelos
{
    public class Usuario
    {
        public Usuario()
        {
            Tuitis = new List<Tuiti>();
        }
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Apelido { get; set; }
        [Required]
        public string Senha { get; set; }
        public virtual List<Tuiti> Tuitis { get; set; }
    }
}
