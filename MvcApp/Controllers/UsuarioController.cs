using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biblioteca.Modelos;

namespace MvcApp.Controllers
{ 
    public class UsuarioController : Controller
    {
        private ContextoDoTuiter db = new ContextoDoTuiter();

        //
        // GET: /Usuario/

        public ActionResult CarregarUsuarioPorApelido(string apelido)
        {
            var usuario = db.Usuarios.SingleOrDefault(u => u.Apelido == apelido);
            return View("Home", usuario);
        }

        public ViewResult Details(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            return View(usuario);
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(usuario);
        }
        
        public ActionResult Edit(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }
 
        public ActionResult Delete(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}