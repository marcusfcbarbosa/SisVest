using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SisVest.DomaninModel.Abstract;
using Moq;
using SisVest.DomaninModel.Entities;

namespace SisVest.Test.Controller
{
    [TestClass]
    public class VestibularControllerTest
    {
        private Mock<IVestibularRepository> mockVestibular;

        [TestInitialize]
        public void TestInitialize()
        {
            mockVestibular = new Mock<IVestibularRepository>();

            //mockVestibular.Setup(a => a.Vestibulares).Returns(new[] {
            //  new Vestibular{
            //        ID = 1,
            //        Descricao = "Vestibular 2012 ",
            //        DataInicio = DateTime.Now.AddMonths(+4),
            //        DataFim = DateTime.Now.AddMonths(+6),
            //        DataProva= DateTime.Now.AddMonths(+7)
            //        },
            //  new Vestibular{

            //    ID = 2,
            //    Descricao = "Vestibular 2013 ",
            //    DataInicio = DateTime.Now.AddYears(+4),
            //    DataFim = DateTime.Now.AddYears(+6),
            //    DataProva= DateTime.Now.AddYears(+7)
            //} 
            //});
        }
    }
}
