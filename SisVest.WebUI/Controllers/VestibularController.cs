using SisVest.DomaninModel.Abstract;
using SisVest.DomaninModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SisVest.WebUI.Models;
using SisVest.WebUI.Infraestrutura.Filter;


namespace SisVest.WebUI.Controllers
{
    
    [Authorize]
    public class VestibularController : Controller
    {
        private IVestibularRepository repository;

        List<VestibularModel> vestibularModelList = new List<VestibularModel>();

        private VestibularModel vestibularModel;

        public VestibularController(IVestibularRepository vestibularRepository, VestibularModel vestibularModelParam)
        {
            repository = vestibularRepository;
            vestibularModel = vestibularModelParam;
        }

        /// <summary>
        /// Para ter acesso a esse Index, precisa ser autenticado, autenticação pelo Nivel de Action
        /// </summary>
        /// <returns></returns>
        [CustomAutenticacao]
        public ActionResult Index()
        {
            return View(vestibularModel.RetornaTodos());
        }

        public ActionResult Edit(int idVestibular)
        {
            return View(vestibularModel.RetornaVestibular(idVestibular));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(VestibularModel vestibular)
        {
            try
            {
                repository.Inserir(new Vestibular
                {
                    Descricao = vestibular.Descricao,
                    DataProva = vestibular.DataProva,
                    DataInicio = vestibular.DataInicio,
                    DataFim = vestibular.DataFim
                });
                TempData["Mensagem"] = " Vestibular Inserido com sucesso!! ";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(VestibularModel vestibularModel)
        {
            repository.Alterar(new Vestibular
            {
                ID = vestibularModel.ID,
                Descricao = vestibularModel.Descricao,
                DataProva = vestibularModel.DataProva,
                DataInicio = vestibularModel.DataInicio,
                DataFim = vestibularModel.DataFim
            });
            TempData["Mensagem"] = "Vestibular Alterado com sucesso!!";
            return RedirectToAction("Index");
        }

        public ActionResult Details(int idVestibular)
        {
            return View(vestibularModel.RetornaVestibular(idVestibular));
        }

        public ActionResult Delete(int idVestibular)
        {
            return View(vestibularModel.RetornaVestibular(idVestibular));
        }

        public ActionResult Deletar(int idVestibular)
        {
            try
            {
                repository.excluir(idVestibular);
                TempData["Mensagem"] = "Vestibular Excluido com sucesso!!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}