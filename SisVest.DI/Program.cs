using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisVest.DomaninModel.Abstract;
using SisVest.DomaninModel.Concrete;
using Ninject;

namespace SisVest.DI
{
    class Program
    {
        static void Main(string[] args)
        {
            //IKernel ninjectKernel = new StandardKernel();
            ////Sempre mapeando uma interface com a classe concreta
            //ninjectKernel.Bind<ICursoRepository>().To<EFCursoRepository>();
            //ninjectKernel.Bind<VestContext>().ToSelf();
            ////Dizer como esta ligado a interface com a sua dependencia
            //ICursoRepository cursoRepository = ninjectKernel.Get<ICursoRepository>();
            ////Injeto as dependencias manualmente
            ////ICursoRepository cursoRepository = new EFCursoRepository(new VestContext);
            //foreach (var curso in cursoRepository.Cursos.ToList())
            //{
            //    Console.Write(String.Format("Curso {0} total de vagas {1}", curso.Descricao, curso.Vagas));
            //}
            //Console.ReadKey();
        }
    }
}
