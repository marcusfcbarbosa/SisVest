using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SisVest.WebUI.Infraestrutura.Filter
{
    /// <summary>
    /// Clase de filtros de Action
    /// </summary>
    public class TesteFiltroAttribute :  FilterAttribute , IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Write("Executando Filtro apos encerrar a Action <br />");
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Write("Executando Filtro antes de iniciar a Action <br />");
        }
    }
}