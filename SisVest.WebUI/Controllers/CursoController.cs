using SisVest.DomaninModel.Abstract;
using SisVest.DomaninModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SisVest.WebUI.Models;
using SisVest.WebUI.Infraestrutura.Provider.Abstract;
using SisVest.WebUI.Infraestrutura.Filter;

namespace SisVest.WebUI.Controllers
{
    /// <summary>
    /// Autenticação a Nivel de Controller
    /// </summary>
    [Authorize]
    public class CursoController : Controller
    {
        private ICursoRepository repository;

        List<CursoModel> cursoModelList = new List<CursoModel>();

        private CursoModel cursoModel;

        private IAutenticacaoProvider autenticacaoProvider;

        /// <summary>
        /// A injeção de depencia agora é feita pelo repositorio
        /// não mais pelo VestContext
        /// </summary>
        /// <param name="cursoRepository"></param>
        public CursoController(ICursoRepository cursoRepository, CursoModel cursoModelParam, IAutenticacaoProvider autenticacaoProviderParam)
        {
            repository = cursoRepository;
            cursoModel = cursoModelParam;
            autenticacaoProvider = autenticacaoProviderParam;
        }

        [TesteFiltro]
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
                ModelState["ID"].Errors.Clear();
                if (ModelState.IsValid)
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
                return View(curso);
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
                ModelState["ID"].Errors.Clear();
                if (ModelState.IsValid)
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
                return View(curso);
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