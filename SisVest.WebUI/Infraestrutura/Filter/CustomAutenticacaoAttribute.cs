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

        //A dependencia não será injetada por Construtor
        public CustomAutenticacaoAttribute(string grupo)
        {
            grupoEscolhido = grupo;
        }

        //apenas para negar o acesso
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (autenticacaoProvider.Autenticado && autenticacaoProvider.UsuarioAutenticado.Grupo == grupoEscolhido)
            //if (!autenticacaoProvider.Autenticado && autenticacaoProvider.UsuarioAutenticado.Grupo == grupoEscolhido)
            //if (!autenticacaoProvider.Autenticado)
            {
                return true;
            }
            return false;
        }
    }
}