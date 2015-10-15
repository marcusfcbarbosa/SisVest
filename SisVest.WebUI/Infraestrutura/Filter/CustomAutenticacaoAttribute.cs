﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SisVest.WebUI.Infraestrutura.Provider.Abstract;
namespace SisVest.WebUI.Infraestrutura.Filter
{
    public class CustomAutenticacaoAttribute : AuthorizeAttribute
    {
        //A principio essa injeção não irá funcionar
        public IAutenticacaoProvider autenticacaoProvider { get; set; }

        //apenas para negar o acesso
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!autenticacaoProvider.Autenticado && autenticacaoProvider.UsuarioAutenticado.Grupo == "administrador")
            {
                return true;
            }
            return false;
        }
    }
}