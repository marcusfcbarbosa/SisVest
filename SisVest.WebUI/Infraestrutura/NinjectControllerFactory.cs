using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using SisVest.DomaninModel;
using SisVest.DomaninModel.Abstract;
using SisVest.DomaninModel.Concrete;
using SisVest.WebUI.Models;

namespace SisVest.WebUI.Infraestrutura
{


    /// <summary>
    /// Para criar um Controller Factory customizado, deve-se herdar
    /// DefaultControllerFactory
    /// </summary>
    public class NinjectControllerFactory : DefaultControllerFactory
    {

        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {

            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        /// <summary>
        /// Para que seja possivel pegar a instancia 
        /// do nosso Controller
        /// Dessa forma a dependencia que meu controller precisar, o ninject as irá adicionar
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        #region "Mapeamento das Dependencias"

        /// <summary>
        /// Mapeamento das dependencias dos Controllers, vincula as classes concretas com a Interface
        /// </summary>
        private void AddBindings()
        {
            ninjectKernel.Bind<ICursoRepository>().To<EFCursoRepository>();
            ninjectKernel.Bind<IVestibularRepository>().To<EFVestibularRepository>();
            ninjectKernel.Bind<ICandidatoRepository>().To<EFCandidatoRepository>();
            ninjectKernel.Bind<IAdminRepository>().To<EFAdminRepository>();
            ninjectKernel.Bind<VestContext>().ToSelf();
            //Explicitando as dependencias de CursoModel
            ninjectKernel.Bind<CursoModel>().ToSelf();
            ninjectKernel.Bind<VestibularModel>().ToSelf();
        }
        #endregion
    }
}