using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcApp.Models;
using Biblioteca.Modelos;

namespace MvcApp.Controllers
{

    [Authorize]
    public class ContaController : Controller
    {
        private ContextoDoTuiter contexto;

        public ContaController(){
            contexto = new ContextoDoTuiter();
        }

        //
        // GET: /Account/LogOn

        [AllowAnonymous]
        public ActionResult Entrar()
        {
            return ContextDependentView();
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult JsonEntrar(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (ValidarUsuario(model.Email, model.Senha))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, model.Lembrar);
                    return Json(new { success = true, redirect = returnUrl });
                }
                else
                {
                    ModelState.AddModelError("", "O e-mail ou senha estão incorretos");
                }
            }

            // If we got this far, something failed
            return Json(new { errors = ModelState.ToArray() });
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Entrar(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (ValidarUsuario(model.Email, model.Senha))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, model.Lembrar);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "O e-mail ou senha estão incorretos");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Registrar()
        {
            return ContextDependentView();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult JsonRegistrar(Usuario model)
        {
            if (ModelState.IsValid)
            {
                try
                { 
                    CriarUsuario(model);
                    FormsAuthentication.SetAuthCookie(model.Email, createPersistentCookie: false);
                    return Json(new { success = true });
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            // If we got this far, something failed
            return Json(new { errors = ModelState.ToArray() });
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Registrar(Usuario model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CriarUsuario(model);
                    FormsAuthentication.SetAuthCookie(model.Email, createPersistentCookie: false);
                    return RedirectToAction("Index", "Home");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private ActionResult ContextDependentView()
        {
            string actionName = ControllerContext.RouteData.GetRequiredString("action");
            if (Request.QueryString["content"] != null)
            {
                ViewBag.FormAction = "Json" + actionName;
                return PartialView();
            }
            else
            {
                ViewBag.FormAction = actionName;
                return View();
            }
        }

        private bool ValidarUsuario(string email, string senha)
        {
            var usuario = contexto.Usuarios.Single(u => u.Email == email);
            if (usuario != null)
            {
                return usuario.Senha == senha;
            }
            else
            {
                return false;
            }
        }

        private void CriarUsuario(Usuario Usuario)
        {
            contexto.Usuarios.Add(Usuario);
            contexto.SaveChanges();
        }
    }
}
