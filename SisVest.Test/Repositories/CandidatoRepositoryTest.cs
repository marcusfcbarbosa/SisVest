using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SisVest.DomaninModel.Abstract;
using SisVest.DomaninModel.Concrete;
using SisVest.DomaninModel.Entities;


namespace SisVest.Test.Repositories
{
    [TestClass]
    public class CandidatoRepositoryTest
    {
        private ICandidatoRepository candidatoRepository;
        private VestContext vestContext = new VestContext();
        private Candidato candidatoInserir, candidatoInserir2,
            candidatoInserir3,
            candidatoInserir4,
            candidatoInserir5,
            candidatoInserir6,
            candidatoInserir7,
            candidatoInserir8;

        private Vestibular vestibularInserir;
        private Curso cursoInserir;

        [TestInitialize]
        public void InicializaTest()
        {
            var candidatosParaRemover = from c in vestContext.Candidatos select c;
            if (candidatosParaRemover.Count() > 0)
            {
                foreach (var a in candidatosParaRemover)
                {
                    vestContext.Candidatos.Remove(a);
                }
                vestContext.SaveChanges();
            }


            //Insere o Vestibular
            vestibularInserir = new Vestibular
            {
                DataFim = DateTime.Now,
                DataInicio = DateTime.Now.AddDays(-10),
                DataProva = DateTime.Now.AddDays(-10),
                Descricao = "Tecnio em Redes"
            };
            vestContext.Vestibulares.Add(vestibularInserir);
            vestContext.SaveChanges();

            //Insere o Curso
            cursoInserir = new Curso
            {
                Descricao = "Computação",
                Vagas = 5
            };
            ///Ação
            vestContext.Cursos.Add(cursoInserir);
            vestContext.SaveChanges();

            ///Aqui é feita uma injeção manual
            candidatoRepository = new EFCandidatoRepository(vestContext);

            candidatoInserir = new Candidato
            {
                Curso = cursoInserir,
                DataNascimento = new DateTime(1986, 5, 5),
                Cpf = "35848857873",
                Email = "marcus@inmov.net",
                Senha = "123456",
                Sexo = "Masculino",
                Telefone = "11 9 86602829",
                Nome = "Marcus",
                Vestibular = vestibularInserir
            };
            vestContext.Candidatos.Add(candidatoInserir);
            vestContext.SaveChanges();
        }

        //[TestMethod]
        //public void PodeExcluirCandidatoTest()
        //{
        //    //Ambiente
        //    candidatoRepository.Excluir(candidatoInserir.ID);
        //    //Ação
        //    var result = (from c in candidatoRepository.Candidatos
        //                  where c.ID == candidatoInserir.ID
        //                  select c);
        //    //Assertivas
        //    Assert.AreEqual(0, result.Count());
        //}

        [TestMethod]
        public void PodeConsultarLingUsandoRepositorioTest()
        {
            //Ambiente
            vestContext.Candidatos.Add(candidatoInserir);
            vestContext.SaveChanges();
            //Ação
            var candidatos = candidatoRepository.Candidatos;
            var retorno = (from c in candidatos
                           where c.ID == candidatoInserir.ID
                           select c).FirstOrDefault();
            //Assertivas
            Assert.IsInstanceOfType(candidatos, typeof(IQueryable<Candidato>));
            Assert.AreEqual(retorno, candidatoInserir);
            Assert.AreEqual(cursoInserir, retorno.Curso);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoRealizarInscricaoDeCandidatoSemEmailTest()
        {
            //Ambiente
            candidatoInserir.Email = null;
            //Ação
            candidatoRepository.RealizaInscricao(candidatoInserir);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NaoPodeInserirCandidatoComMesmoCpfjJaCadastradoTest()
        {

            candidatoInserir2 = new Candidato
            {
                Curso = cursoInserir,
                DataNascimento = new DateTime(1986, 5, 29),
                Cpf = "35848857873",
                Email = "marcus@inmov.net",
                Senha = "123456",
                Sexo = "Masculino",
                Telefone = "11 9 86602829",
                Nome = "Marcus",
                Vestibular = vestibularInserir
            };
            candidatoRepository.RealizaInscricao(candidatoInserir2);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NaoPodeInserirCandidatoComMesmoEmailJaCadastradoTest()
        {
            candidatoInserir3 = new Candidato
            {
                Curso = cursoInserir,
                DataNascimento = new DateTime(1986, 5, 29),
                Cpf = "21015511872",
                Email = candidatoInserir.Email,
                Senha = "123456",
                Sexo = "Masculino",
                Telefone = "11 9 86602829",
                Nome = "Marcus",
                Vestibular = vestibularInserir
            };
            candidatoRepository.RealizaInscricao(candidatoInserir3);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NaoPodeInserirCandidatoSemCursoTest()
        {
            candidatoInserir4 = new Candidato
            {
                Curso = null,
                DataNascimento = new DateTime(1986, 5, 29),
                Cpf = "42677270404",
                Email = "email@teste.com",
                Senha = "123456",
                Sexo = "Masculino",
                Telefone = "11 9 86602829",
                Nome = "Marcus",
                Vestibular = vestibularInserir
            };
            candidatoRepository.RealizaInscricao(candidatoInserir4);
        }



        [TestMethod]
        public void PodeAprovarCandidtoTest()
        {
            cursoInserir.Vagas = 3;
            vestContext.SaveChanges();
            //Criando o 2º Candidato
            candidatoInserir5 = new Candidato
            {
                Curso = cursoInserir,
                DataNascimento = new DateTime(1986, 5, 29),
                Cpf = "54721050992",
                Email = "marcus2@inmov.net5",
                Senha = "123456",
                Sexo = "Masculino 5",
                Telefone = "11 9 86602829 5",
                Nome = "Marcus 5",
                Vestibular = vestibularInserir
            };

            candidatoInserir6 = new Candidato
            {
                Curso = cursoInserir,
                DataNascimento = new DateTime(1986, 5, 29),
                Cpf = "11975453603",
                Email = "marcus2@inmov.net6",
                Senha = "123456",
                Sexo = "Masculino 6",
                Telefone = "11 9 86602829 6",
                Nome = "Marcus 6",
                Vestibular = vestibularInserir
            };

            candidatoInserir7 = new Candidato
            {
                Curso = cursoInserir,
                DataNascimento = new DateTime(1986, 5, 29),
                Cpf = "41736464612",
                Email = "marcus2@inmov.net7",
                Senha = "123456",
                Sexo = "Masculino 7",
                Telefone = "11 9 86602829 7",
                Nome = "Marcus 7",
                Vestibular = vestibularInserir
            };

            //candidatoInserir8 = new Candidato
            //{
            //    Curso = cursoInserir,
            //    DataNascimento = new DateTime(1986, 29, 5),
            //    Cpf = "75239823103",
            //    Email = "marcus2@inmov.net8",
            //    Senha = "123456",
            //    Sexo = "Masculino 8",
            //    Telefone = "11 9 86602829 8",
            //    Nome = "Marcus 8",
            //    Vestibular = vestibularInserir
            //};

            //Ação
            candidatoRepository.RealizaInscricao(candidatoInserir5);
            candidatoRepository.RealizaInscricao(candidatoInserir6);
            candidatoRepository.RealizaInscricao(candidatoInserir7);
            //candidatoRepository.RealizaInscricao(candidatoInserir8);

            candidatoRepository.Aprovar(candidatoInserir5.ID);
            candidatoRepository.Aprovar(candidatoInserir6.ID);
            candidatoRepository.Aprovar(candidatoInserir7.ID);
            //candidatoRepository.Aprovar(candidatoInserir8.ID);

            //Assert
            var result = (from curso in vestContext.Cursos
                          from candidato in curso.CandidatosList
                          where curso.ID == cursoInserir.ID && candidato.Aprovado == true
                          select candidato);

            Assert.AreEqual(3, result.Count());
            //Assert.IsTrue(result.ToList().Contains(candidatoInserir5));
            //Assert.IsTrue(result.ToList().Contains(candidatoInserir6));
            //Assert.IsTrue(result.ToList().Contains(candidatoInserir7));
        }


        [TestMethod]
        public void NaoPodeAprovarCandidatoEmCursoSemVagasTest()
        {
            cursoInserir.Vagas = 2;
            vestContext.SaveChanges();
            //Criando o 2º Candidato
            candidatoInserir5 = new Candidato
            {
                Curso = cursoInserir,
                DataNascimento = new DateTime(1986, 5, 29),
                Cpf = "54721050992",
                Email = "marcus2@inmov.net5",
                Senha = "123456",
                Sexo = "Masculino 5",
                Telefone = "11 9 86602829 5",
                Nome = "Marcus 5",
                Vestibular = vestibularInserir
            };

            candidatoInserir6 = new Candidato
            {
                Curso = cursoInserir,
                DataNascimento = new DateTime(1986, 5, 28),
                Cpf = "11975453603",
                Email = "marcus2@inmov.net6",
                Senha = "123456",
                Sexo = "Masculino 6",
                Telefone = "11 9 86602829 6",
                Nome = "Marcus 6",
                Vestibular = vestibularInserir
            };

            candidatoInserir7 = new Candidato
            {
                Curso = cursoInserir,
                DataNascimento = new DateTime(1986, 5, 27),
                Cpf = "41736464612",
                Email = "marcus2@inmov.net7",
                Senha = "123456",
                Sexo = "Masculino 7",
                Telefone = "11 9 86602829 7",
                Nome = "Marcus 7",
                Vestibular = vestibularInserir
            };
            //Ação
            candidatoRepository.RealizaInscricao(candidatoInserir5);
            candidatoRepository.RealizaInscricao(candidatoInserir6);
            //candidatoRepository.RealizaInscricao(candidatoInserir7);

            candidatoRepository.Aprovar(candidatoInserir5.ID);
            candidatoRepository.Aprovar(candidatoInserir6.ID);

            //Ao tentar aprovar o terceiro InvalidOperationException
            //candidatoRepository.Aprovar(candidatoInserir7.ID);
        }


    }
}