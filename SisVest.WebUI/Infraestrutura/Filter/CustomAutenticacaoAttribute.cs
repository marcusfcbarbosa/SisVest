using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SisVest.WebUI.Infraestrutura.Provider.Abstract;
using Ninject;
namespace SisVest.WebUI.Infraestrutura.Filter
{
    public class CustomAutenticacaoAttribute : AuthorizeAttribute
    {
        //Para que ele tenha um acesso externo. {get;set;}
        //E para que tenha a sua instancia recebida por injeção, um decorator será dado a ele decorate [Inject]
        [Inject]
        public IAutenticacaoProvider autenticacaoProvider { get; set; }

        public String grupoEscolhido;
        private String msgErro = String.Empty;
        //A dependencia não será injetada por Construtor
        public CustomAutenticacaoAttribute(string grupo)
        {
            grupoEscolhido = grupo;
        }

        #region"Métodos que comunicam com o TempData do Controller"
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            
        }
        /// <summary>
        /// Retorna uma mensagem de erro a view pelo TempData do Controller
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            filterContext.Controller.TempData["Mensagem"] = msgErro;
        }

        #endregion

        //apenas para negar o acesso
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {


            if (autenticacaoProvider.Autenticado)
            {

                if (!String.IsNullOrEmpty(grupoEscolhido))
                {
                    if (autenticacaoProvider.UsuarioAutenticado.Grupo != grupoEscolhido)
                    {
                        msgErro = "Você não tem permissão para acessar essa pagina, com suas credenciais";
                        return false;
                    }
                }
                return true;
            }
            else
            {

                msgErro = "Você precisa estar autenticado para acessar essa página";
                return false;
            }

            //if (autenticacaoProvider.Autenticado && autenticacaoProvider.UsuarioAutenticado.Grupo == grupoEscolhido)
            //{
            //    return true;
            //}
            //return false;
        }
    }
}