using SisVest.WebUI.Infraestrutura.Provider.Abstract;
using SisVest.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SisVest.WebUI.Controllers
{

    public class AutenticacaoController : Controller
    {
        private IAutenticacaoProvider autenticacaoProvider;

        public AutenticacaoController(IAutenticacaoProvider autenticacaoProviderParam)
        {
            autenticacaoProvider = autenticacaoProviderParam;
        }

        // GET: Autenticacao
        public ActionResult Entrar()
        {
            ViewBag.Autenticado = autenticacaoProvider.Autenticado;
            return View(autenticacaoProvider.UsuarioAutenticado);
        }

        [HttpPost]
        public ActionResult Entrar(AutenticacaoModel autenticacaoModel, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                string msgErro;
                if (autenticacaoProvider.Autenticar(autenticacaoModel, out msgErro, "administrador"))
                {
                    //Transfere para o index de CursoController. ActionResult , Controller 
                    //caso nao tenha nenhum endereço redireciona para Index
                    FormsAuthentication.SetAuthCookie(autenticacaoModel.Login, false);
                    return Redirect(ReturnUrl ?? Url.Action("Index", "Admin"));
                }
                TempData["Mensagem"] = msgErro;
                return RedirectToAction("Entrar");
            }
            return View();
        }

        public ActionResult Sair()
        {
            autenticacaoProvider.Desautenticar();
            return RedirectToAction("Entrar");
        }
    }
}