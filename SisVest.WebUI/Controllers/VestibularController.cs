﻿using SisVest.DomaninModel.Abstract;
using SisVest.DomaninModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SisVest.WebUI.Models;

namespace SisVest.WebUI.Controllers
{
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

        public ActionResult Index()
        {
            return View(vestibularModel.RetornaTodos());
        }

        public ActionResult Edit(int idVestibular)
        {
            return View(vestibularModel.RetornaVestibular(idVestibular));
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