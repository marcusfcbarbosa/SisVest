using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SisVest.WebUI.Infraestrutura.FilterProvider
{
    /// <summary>
    /// O FilterProvider, é quem provea autenticação dos filtros para toda a aplicação
    /// </summary>
    public class FilterProviderCustom : FilterAttributeFilterProvider
    {

        public override IEnumerable<System.Web.Mvc.Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(controllerContext, actionDescriptor);
            //mecanismo de resolução de depencia, aquele que foi instanciado no applicationStart
            var dependencyResolver = (NinjectDependencyResolver)DependencyResolver.Current;

            if (dependencyResolver != null) { 
                foreach(var filter in filters){
                    dependencyResolver.Kernel.Inject(filter.Instance);
                }
            }
            return filters;
        }

    }
}