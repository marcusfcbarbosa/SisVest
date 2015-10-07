using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SisVest.DomaninModel.Abstract;
using SisVest.DomaninModel.Concrete;
using SisVest.DomaninModel.Entities;

namespace SisVest.Test.Repositories
{
    [TestClass]
    public class AdminRepositoriesTest
    {
        /// <summary>
        /// Repositorio de Admin
        /// </summary>
        private IAdminRepository adminRepository;

        /// <summary>
        /// Contexto da base 
        /// </summary>
        private VestContext vestContext = new VestContext();

        private Admin adminInserir, adminInserir2, adminInserir3;

        [TestInitialize]
        public void InicializaTeste()
        {
            adminRepository = new EFAdminRepository(vestContext);
        }
         //<summary>
         //Após encerrado o teste ele é executado
         //</summary>
        [TestMethod]
        public void LimparLista()
        {
            var adminsParaRemover = from a in vestContext.Admins select a;
            if (adminsParaRemover.Count() > 0)
            {
                foreach (var a in adminsParaRemover)
                {
                    vestContext.Admins.Remove(a);
                }
                vestContext.SaveChanges();
            }
        }
        
        [TestMethod]
        public void PodeConsultarLinqUsandoRepositorio()
        {
            ///A A A

            ///Ambiente
            var adminInserir = new Admin
            {
                Email = "marcus@inmov.net",
                Login = "Marcus",
                NomeTratamento = "Marquinhos",
                Senha = "123456"
            };

            vestContext.Admins.Add(adminInserir);
            vestContext.SaveChanges();
            ///Ação
            var admins = vestContext.Admins;
            var retorno = (from a in admins where a.Login.Equals(adminInserir.Login) select a).FirstOrDefault();

            ///Assertivas
            Assert.IsInstanceOfType(admins, typeof(IQueryable<Admin>));
            Assert.AreEqual(retorno, adminInserir);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]//Teste esperando uma exceção
        public void NaoPodeInserirAdminComMesmoEmailTest()
        {
            ///A A A (Ambiente, Ação, Assertiva)
            ///Ambiente
            var adminInserir2 = new Admin
            {
                Email = "marcus@inmov.net",
                Login = "Marcus 2",
                NomeTratamento = "Marquinhos 2",
                Senha = "123456"
            };
            ///Ação
            adminRepository.InsereAdmin(adminInserir2);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]//Teste esperando uma exceção
        public void NaoPodeInserirAdminComMesmoLoginTest()
        {
            ///A A A (Ambiente, Ação, Assertiva)
            ///Ambiente
            var adminInserir2 = new Admin
            {
                Email = "marcus@inmov.net",
                Login = "Marcus 3",
                NomeTratamento = "Marquinhos 2",
                Senha = "123456"
            };
            //Ação
            //Que precisa levantar uma excessão
            adminRepository.InsereAdmin(adminInserir2);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]//Teste esperando uma exceção
        public void PodeAlterarTest()
        {
            var adminInserir3 = new Admin
            {
                Email = "marcus@inmov.net",
                Login = "Marcus 3",
                NomeTratamento = "Marquinhos 2",
                Senha = "123456"
            };
            var teste = adminInserir3;
            adminRepository.InsereAdmin(adminInserir3);
            //Ação
            var ret = adminRepository.Retornar(adminInserir3.ID);
            ret.Email = "marcus@inmov.net 43354453423";
            ret.Login = "Marcus 3435";
            ret.NomeTratamento = "Marquinhos 3435";
            ret.Senha = "123456";
            adminRepository.Alterar((Admin)ret);
            //Assert
            Assert.AreEqual(adminInserir3, ret);
        }


    }
}