using SisVest.WebUI.Infraestrutura.Provider.Abstract;
using SisVest.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }

        [HttpPost]
        public ActionResult Entrar(AutenticacaoModel autenticacaoModel)
        {

            if (ModelState.IsValid)
            {
                string msgErro;

                if (autenticacaoProvider.Autenticar(autenticacaoModel, out msgErro, "administrador"))
                {
                    //Transfere para o index de CursoController. ActionResult , Controller 
                    //caso nao tenha nenhum endereço redireciona para Index
                    //return Redirect(ReturnUrl == String.Empty ? Url.Action("Index", "Curso") : ReturnUrl);
                    return Redirect(Url.Action("Index", "Curso"));

                }
                ModelState.AddModelError("", msgErro);
            }
            return View();
        }
    }
}