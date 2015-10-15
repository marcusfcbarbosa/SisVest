using Ninject;
using SisVest.DomaninModel.Concrete;
using SisVest.WebUI.Infraestrutura.Provider.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject.Syntax;
using SisVest.DomaninModel.Abstract;
using SisVest.WebUI.Models;
using SisVest.WebUI.Infraestrutura.Provider.Abstract;

namespace SisVest.WebUI.Infraestrutura
{
    public class NinjectDependencyResolver : IDependencyResolver
    {

        private IKernel kernel;

        public IKernel Kernel { get { return kernel; } }

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        public IBindingToSyntax<T> Bind<T>()
        {
            return kernel.Bind<T>();
        }
        private void AddBindings()
        {
            Bind<ICursoRepository>().To<EFCursoRepository>();
            Bind<IVestibularRepository>().To<EFVestibularRepository>();
            Bind<ICandidatoRepository>().To<EFCandidatoRepository>();
            Bind<IAdminRepository>().To<EFAdminRepository>();
            Bind<VestContext>().ToSelf();
            // as dependencias de CursoModel
            Bind<CursoModel>().ToSelf();
            Bind<VestibularModel>().ToSelf();
            // providers de autenticação
            Bind<IAutenticacaoProvider>().To<CustomAutenticacaoProvider>();
        }

        //resolvendo a injeção de depencia
        //Quando ele recupera o serviço, ele recupera os tipos que foram passados
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }


        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}