using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Biblioteca.Modelos;
using System.Data.Entity.Validation;

namespace Tuiter.Tests.Model
{
    [TestClass]
    public class UsuarioTest
    {
        Usuario UsuarioDeTeste
        {
            get
            {
                return new Usuario()
                {
                    Apelido = "teste",
                    Email = "teste@email.com",
                    Nome = "Teste da Silva",
                    Senha = "teste123"
                };
            }
        }

        [TestMethod]
        public void PossoCriarUmUsuarioEPersistiLo()
        {
            var usuario = UsuarioDeTeste;
            var contexto = new ContextoDoTuiter();
            contexto.Usuarios.Add(usuario);
            contexto.SaveChanges();
            Assert.IsNotNull(contexto.Usuarios.Where(u => u.Apelido == "teste"));
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void OUsuarioPrecisaTerUmApelido()
        {
            var usuario = UsuarioDeTeste;
            usuario.Apelido = null;
            var contexto = new ContextoDoTuiter();
            contexto.Usuarios.Add(usuario);
            contexto.SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void OUsuarioPrecisaTerUmNome()
        {
            var usuario = UsuarioDeTeste;
            usuario.Nome = null;
            var contexto = new ContextoDoTuiter();
            contexto.Usuarios.Add(usuario);
            contexto.SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void OUsuarioPrecisaTerUmEmail()
        {
            var usuario = UsuarioDeTeste;
            usuario.Email = null;
            var contexto = new ContextoDoTuiter();
            contexto.Usuarios.Add(usuario);
            contexto.SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void OUsuarioPrecisaTerUmaSenha()
        {
            var usuario = UsuarioDeTeste;
            usuario.Senha = null;
            var contexto = new ContextoDoTuiter();
            contexto.Usuarios.Add(usuario);
            contexto.SaveChanges();
        }

        [TestMethod]
        public void OUsuarioPodeCriarUmTuiti()
        {
            var usuario = UsuarioDeTeste;
            var contexto = new ContextoDoTuiter();
            contexto.Usuarios.Add(usuario);
            contexto.SaveChanges();

            var usuarioDoBanco = contexto.Usuarios.Single(u => u.Apelido == "teste");
            usuarioDoBanco.Tuitis.Add(new Tuiti {
            Texto = "novo tuiti", DataDeCriacao = DateTime.Now
            });
            contexto.SaveChanges();

            var usuarioDeVerificacao = contexto.Usuarios.Single(u => u.Apelido == "teste");
            Assert.AreEqual(1, usuarioDeVerificacao.Tuitis.Count);
        }

    }
}
