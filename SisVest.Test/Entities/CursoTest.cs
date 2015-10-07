using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SisVest.DomaninModel.Entities;

namespace SisVest.Test.Entities
{
    [TestClass]
    public class CursoTest
    {
        Curso curso1, curso2, curso3;

        [TestInitialize]
        public void InicializaOsTestes() {

            curso1 = new Curso { 
                ID=1,
                Descricao="Engenharia de computação",
                Vagas = 25
            };

            curso2 = new Curso
            {
                ID = 1,
                Descricao = "Engenharia de computação",
                Vagas = 25
            };

            curso3 = new Curso
            {
                ID = 2,
                Descricao = "Engenharia de Alimentos",
                Vagas = 28
            };
        }

        [TestMethod]
        public void GarantiQueDoisCursosComMesmoId()
        {
            Assert.AreEqual(curso1.ID,curso2.ID);
        }

        [TestMethod]
        public void GarantiQueDoisCursosComMesmaDescricao()
        {
            Assert.AreEqual(curso2.Descricao, curso2.Descricao);
        }

        [TestMethod]
        public void GarantiQueDoisCursosNaoSaoIguais()
        {
            Assert.AreNotEqual(curso1, curso3);
        }

        [TestMethod]
        public void GarantiQueDoisCursosSaoIguais()
        {
            Assert.AreEqual(curso1, curso2);
        }
    }
}