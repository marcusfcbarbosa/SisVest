using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SisVest.DomaninModel.Entities;

namespace SisVest.Test.Entities
{
    [TestClass]
    public class CandidatoTest
    {
        public Candidato cand1, cand2, cand3;
        
        [TestInitialize]
        public void InicializaTeste() {

            cand1 = new Candidato
            {
                Cpf="35848857873",
                DataNascimento = DateTime.Now,
                Email="marcus@inmov.net",
                Nome="Marcus",
                Senha="123456",
                Sexo="M",
                Telefone="9 86602829"
            };

            cand2 = new Candidato
            {
                Cpf = "35848857873",
                DataNascimento = DateTime.Now,
                Email = "marcus@inmov.net",
                Nome = "Marcus",
                Senha = "123456",
                Sexo = "M",
                Telefone = "9 86602829"
            };
        }

        [TestMethod]
        public void GarantirDoisCandidatosComOMesmoId()
        {
            Assert.AreEqual(cand1.ID, cand2.ID);
        }

        [TestMethod]
        public void GarantirDoisCandidatosComOMesmoCPF()
        {
            Assert.AreEqual(cand1.Cpf, cand2.Cpf);
        }
        [TestMethod]
        public void GarantirDoisCandidatosComOMesmoEmail()
        {
            Assert.AreEqual(cand1.Email, cand2.Email);
        }


    }
}
