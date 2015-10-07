using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SisVest.DomaninModel.Abstract;
using SisVest.DomaninModel.Concrete;
using SisVest.DomaninModel.Entities;


namespace SisVest.Test.Repositories
{
    [TestClass]
    public class VestibularRepositoryTest
    {

        /// <summary>
        /// Rrespostiorio de Vestibular
        /// </summary>
        private IVestibularRepository ivestibularRepository;

        /// <summary>
        /// Contexto
        /// </summary>
        private VestContext vestContext = new VestContext();

        /// <summary>
        /// 
        /// </summary>
        private Vestibular vest1, vest2, vest3;

        /// <summary>
        /// Inicializador dos testes
        /// </summary>
        [TestInitialize]
        public void InicializaTeste()
        {
            ivestibularRepository = new EFVestibularRepository(vestContext);
        }


        [TestMethod]
        public void LimparLista()
        {
            var vestibularParaRemover = from v in vestContext.Vestibulares select v;
            if (vestibularParaRemover.Count() > 0)
            {
                foreach (var a in vestibularParaRemover)
                {
                    vestContext.Vestibulares.Remove(a);
                }
                vestContext.SaveChanges();
            }
        }


        //Teste esperando uma exceção
        [TestMethod]
        //[ExpectedException(typeof(InvalidOperationException))]
        public void InsereVestibularTest()
        {
            ///Ambiente
            ///Insere o Vestibular
            vest1 = new Vestibular
            {
                DataFim = DateTime.Now,
                DataInicio = DateTime.Now.AddDays(-10),
                DataProva = DateTime.Now.AddDays(-10),
                Descricao = "Engenharia de Computação"
            };
            ivestibularRepository.Inserir(vest1);
            vest2 = new Vestibular
            {
                DataFim = DateTime.Now,
                DataInicio = DateTime.Now.AddDays(-10),
                DataProva = DateTime.Now.AddDays(-10),
                Descricao = "Analise de sistemas"
            };
            ivestibularRepository.Inserir(vest2);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NaoPodeInsereVestibularSemInformarDatasTest()
        {
            vest2 = new Vestibular
          {
              Descricao = "Analise de sistemas " + Convert.ToString(DateTime.Now.AddSeconds(-30))
          };
            ivestibularRepository.Inserir(vest2);

        }

        [TestMethod]
        public void RetornaVestibularPorID()
        {
            ///Ambiente
            vest2 = new Vestibular
            {
                DataFim = DateTime.Now,
                DataInicio = DateTime.Now.AddDays(-10),
                DataProva = DateTime.Now.AddDays(-10),
                Descricao = "Analise de sistemas " + Convert.ToString(DateTime.Now.AddSeconds(-30))
            };
            vest3 = new Vestibular
            {
                DataFim = DateTime.Now,
                DataInicio = DateTime.Now.AddDays(-10),
                DataProva = DateTime.Now.AddDays(-10),
                Descricao = "Analise de sistemas " + Convert.ToString(DateTime.Now.AddSeconds(-10))
            };
            ivestibularRepository.Inserir(vest3);
            ivestibularRepository.Inserir(vest2);
            //Ação e Assertiva
            var v1 = ivestibularRepository.Retorna(vest3.ID);
            var v2 = ivestibularRepository.Retorna(vest2.ID);

            Assert.IsNotNull(v1);
            Assert.IsNotNull(v2);
        }
    }
}