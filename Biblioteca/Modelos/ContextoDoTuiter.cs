using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Biblioteca.Modelos
{
    public class ContextoDoTuiter : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tuiti> Tuitis { get; set; }
    }
}
