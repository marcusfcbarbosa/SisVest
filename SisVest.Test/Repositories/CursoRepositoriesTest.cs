using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SisVest.DomaninModel.Abstract;
using SisVest.DomaninModel.Concrete;
using SisVest.DomaninModel.Entities;

namespace SisVest.Test.Repositories
{
    [TestClass]
    public class CursoRepositoriesTest
    {
        /// <summary>
        /// Repositorio de Curso
        /// </summary>
        private ICursoRepository icursoRepository;
        /// <summary>
        /// Contexto da base 
        /// </summary>
        private VestContext vestContext = new VestContext();
        /// <summary>
        /// 
        /// </summary>
        private Curso curso1, curso2, curso3;

        [TestInitialize]
        public void InicializaTeste()
        {
            icursoRepository = new EFCursoRepository(vestContext);
        }


        [TestMethod]
        public void LimparLista()
        {
            var cursosParaRemover = from c in vestContext.Cursos select c;
            if (cursosParaRemover.Count() > 0)
            {
                foreach (var a in cursosParaRemover)
                {
                    vestContext.Cursos.Remove(a);
                }
                vestContext.SaveChanges();
            }
        }

        /// <summary>
        /// NaoPodeInserirCursoComAMesmaDescricao
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]//Teste esperando uma exceção
        public void NaoPodeInserirCursoComAMesmaDescricao()
        {
            ///A A A (Ambiente, Ação, Assertiva)
            ///Ambiente
            curso1 = new Curso
           {
               Descricao = "Engenharia de computação " + Convert.ToString(DateTime.Now),
               Vagas = 25
           };
            ///Ação
            icursoRepository.InserirCurso(curso1);

            curso2 = new Curso
            {
                Descricao = "Engenharia de computação " + Convert.ToString(DateTime.Now),
                Vagas = 10
            };
            icursoRepository.InserirCurso(curso2);
        }

        [TestMethod]
        public void AlterarCursoTest()
        {
            ///Ambiente
            curso3 = new Curso
            {
                Descricao = "Analise de sistemas",
                Vagas = 50
            };
            ///Ação
            icursoRepository.InserirCurso(curso3);
            var atualiza = icursoRepository.RetornaCurso(curso3.ID);
            atualiza.Vagas = 10;
            atualiza.Descricao = "Analise de sistemas " + Convert.ToString(DateTime.Now);
            icursoRepository.AtualizaCurso(atualiza);
            //Assert
            Assert.AreEqual(curso3, atualiza);
        }
    }
}
