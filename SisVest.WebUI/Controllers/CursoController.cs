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

        private CursoModel cursoModel;

        /// <summary>
        /// A injeção de depencia agora é feita pelo repositorio
        /// não mais pelo VestContext
        /// </summary>
        /// <param name="cursoRepository"></param>
        public CursoController(ICursoRepository cursoRepository, CursoModel cursoModelParam)
        {
            repository = cursoRepository;
            cursoModel = cursoModelParam;
        }

        // GET: Curso
        public ActionResult Index()
        {
            //Referencia uma instancia do Model

            return View(cursoModel.RetornaTodos());
            //return View(repository.RetornaTodos());
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
                repository.InserirCurso(new Curso
                {
                    Descricao = curso.Descricao,
                    Vagas = curso.Vagas
                });
                TempData["Mensagem"] = "Curso inserido com sucesso !!";
                //redirecionando para a Action Index
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return RedirectToAction("Index");
            }
        }


        public ActionResult Edit(int idCurso)
        {
            return View(cursoModel.RetornaCursoModel(idCurso));
        }

        [HttpPost]
        public ActionResult Edit(CursoModel curso)
        {
            try
            {
                repository.AtualizaCurso(new Curso
                {
                    Descricao = curso.Descricao,
                    Vagas = curso.Vagas,
                    ID = curso.ID
                });
                TempData["Mensagem"] = "Curso atualizado com sucesso !!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int idCurso)
        {
            return View(cursoModel.RetornaCursoModel(idCurso));
        }
        public ActionResult Deletar(int idCurso)
        {
            try
            {
                repository.Excluir(idCurso);
                TempData["Mensagem"] = "Curso removido com sucesso !!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(int idCurso)
        {
            return View(cursoModel.RetornaCursoModel(idCurso));
        }

    }
}