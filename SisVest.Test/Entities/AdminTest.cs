using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SisVest.DomaninModel.Entities;

namespace SisVest.Test.Entities
{
    [TestClass]
    public class AdminTest
    {

        public Admin admin1, admin2, admin3;

        /// <summary>
        /// Para cada vez que execuar um teste, é iniciado o Inicializer    
        /// </summary>
        [TestInitialize]
        public void InicializaTeste() {
            admin1 = new Admin
            {
                ID = 1,
                Email = "marcus@inmnov.net",
                Login = "marcusfcbarbosa",
                NomeTratamento = "markinho",
                Senha = "123456"
            };
            
            admin2 = new Admin
            {
                ID = 1,
                Email = "marcus@inmnov.net",
                Login = "marcusfcbarbosa",
                NomeTratamento = "markinho 2",
                Senha = "123456"
            };

            admin3 = new Admin
            {
                ID = 1,
                Email = "marcus@inmnov",
                Login = "marcusfcbarbosa",
                NomeTratamento = "markinho 2",
                Senha = "123456"
            };
        }

        [TestMethod]
        public void GarantirQueDoisAdminsSaoIguaisQuandoTemMesmoID()
        {
            Assert.AreEqual(admin1, admin2);
            Assert.AreEqual(admin1.ID, admin2.ID);
        }

        [TestMethod]
        public void GarantirQueDoisAdminsSaoIguaisQuandoTemMesmoLogin()
        {
            Assert.AreEqual(admin1, admin2);
        }

        [TestMethod]
        public void GarantirQueDoisAdminsSaoIguaisQuandoTemMesmoEmail()
        {
            Assert.AreEqual(admin2, admin3);
        }
    }
}
