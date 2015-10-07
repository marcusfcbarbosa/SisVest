using SisVest.DomaninModel.Abstract;
using SisVest.DomaninModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SisVest.WebUI.Models;

namespace SisVest.WebUI.Controllers
{
    public class CursoController : Controller
    {

        private ICursoRepository repository;
        List<CursoModel> cursoModelList = new List<CursoModel>();
        /// <summary>
        /// A injeção de depencia agora é feita pelo repositorio
        /// não mais pelo VestContext
        /// </summary>
        /// <param name="cursoRepository"></param>
        public CursoController(ICursoRepository cursoRepository)
        {
            repository = cursoRepository;
        }

        // GET: Curso
        public ActionResult Index()
        {
            var result = repository.Cursos.ToList();
            foreach (var curso in result)
            {
                cursoModelList.Add(new CursoModel
                {
                    ID = curso.ID,
                    Vagas = curso.Vagas,
                    Descricao = curso.Descricao,
                    TotalCandidatosAprovados = repository.CandidatosAprovados(curso.ID).Count(),
                    TotalCandidatos = curso.CandidatosList.Count
                });
            }
            return View(cursoModelList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CursoModel curso)
        {
            try
            {
                repository.InserirCurso(new Curso { 
                    Descricao = curso.Descricao,
                    Vagas = curso.Vagas
                });
                TempData["Mensagem"] = "Curso inserido com sucesso !!";
                AtualizaLista();
                return View("Index", cursoModelList);
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View("Index", cursoModelList);
            }
        }

        public ActionResult Edit(int idCurso)
        {
            return View(repository.RetornaCurso(idCurso));
        }
        public void AtualizaLista() {
            var result = repository.Cursos.ToList();

            foreach (var curso in result)
            {
                cursoModelList.Add(new CursoModel
                {
                    ID = curso.ID,
                    Vagas = curso.Vagas,
                    Descricao = curso.Descricao,
                    TotalCandidatosAprovados = repository.CandidatosAprovados(curso.ID).Count(),
                    TotalCandidatos = curso.CandidatosList.Count
                });
            }
        
        }

        [HttpPost]
        public ActionResult Edit(CursoModel curso)
        {
            try
            {
                Curso c = new Curso();
                c.Vagas = curso.Vagas;
                c.Descricao = curso.Descricao;
                c.ID = curso.ID;
                repository.AtualizaCurso(c);
                AtualizaLista();
                TempData["Mensagem"] = "Curso atualizado com sucesso !!";
                return View("Index", cursoModelList);
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View("Index", cursoModelList);
            }
        }

        public ActionResult Delete(int idCurso)
        {
            AtualizaLista();
            return View(repository.RetornaCurso(idCurso));
        }

        [HttpPost]
        public ActionResult Delete(Curso curso)
        {
            try
            {
                repository.Excluir(curso.ID);
                AtualizaLista();
                TempData["Mensagem"] = "Curso removido com sucesso !!";
                return View("Index", cursoModelList);
            }
            catch (Exception ex)
            {
                AtualizaLista();
                TempData["Mensagem"] = ex.Message;
                return View("Index", cursoModelList);
            }
        }

        public ActionResult Details(int idCurso)
        {
            AtualizaLista();
            return View(cursoModelList.Where(x => x.ID == idCurso).FirstOrDefault());
        }
    }
}