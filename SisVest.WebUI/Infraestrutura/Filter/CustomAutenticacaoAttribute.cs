using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SisVest.WebUI.Infraestrutura.Filter
{
    public class CustomAutenticacaoAttribute : AuthorizeAttribute
    {

        //apenas para negar o acesso
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return false;
        }
    }
}