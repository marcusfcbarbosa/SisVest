using SisVest.DomaninModel.Abstract;
using SisVest.DomaninModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SisVest.WebUI.Controllers
{
    public class CursoController : Controller
    {

        private ICursoRepository repository;

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
            return View(repository.Cursos.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Curso curso)
        {
            try
            {
                repository.InserirCurso(curso);
                TempData["Mensagem"] = "Curso inserido com sucesso !!";
                return View("Index", repository.Cursos.ToList());
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View("Index", repository.Cursos.ToList());
            }
        }

        public ActionResult Alterar(int idCurso)
        {
            return View(repository.RetornaCurso(idCurso));
        }

        [HttpPost]
        public ActionResult Alterar(Curso curso)
        {
            try
            {
                repository.AtualizaCurso(curso);
                TempData["Mensagem"] = "Curso atualizado com sucesso !!";
                return View("Index", repository.Cursos.ToList());
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View("Index", repository.Cursos.ToList());
            }

        }

        public ActionResult Delete(int idCurso)
        {
            return View(repository.RetornaCurso(idCurso));
        }

        [HttpPost]
        public ActionResult Delete(Curso curso)
        {
            try
            {
                repository.Excluir(curso.ID);
                TempData["Mensagem"] = "Curso removido com sucesso !!";
                return View("Index", repository.Cursos.ToList());
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View("Index", repository.Cursos.ToList());
            }
        }

        public ActionResult Detalhe(int idCurso)
        {
            return View(repository.RetornaCurso(idCurso));

        }
        //[HttpPost]
        //public ActionResult Alterar(int idCurso)
        //{
        //    return View(repository.RetornaCurso(idCurso));
        //}

        public ActionResult Excluir(int idCurso)
        {
            return View(repository.RetornaCurso(idCurso));
        }
    }
}