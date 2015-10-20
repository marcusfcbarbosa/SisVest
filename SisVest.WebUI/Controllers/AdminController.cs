using SisVest.DomaninModel.Abstract;
using SisVest.DomaninModel.Entities;
using SisVest.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SisVest.WebUI.Controllers
{
    public class AdminController : Controller
    {

        private IAdminRepository adminRepository;

        public AdminController(IAdminRepository repository)
        {
            adminRepository = repository;
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View(adminRepository.Admins);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AdminModel adminModel)
        {
            try
            {
                adminRepository.InsereAdmin(new Admin
                {
                    Email = adminModel.Email,
                    Login = adminModel.Login,
                    NomeTratamento = adminModel.NomeTratamento,
                    Senha = adminModel.Senha
                });
                TempData["Mensagem"] = "Administrador inserido com sucesso !!";
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int idAdmin)
        {
            return View(new AdminModel(adminRepository).RetornaAdmin(idAdmin));
        }

        [HttpPost]
        public ActionResult Edit(AdminModel adminModel)
        {
            adminRepository.Alterar(new Admin
            {
                ID = adminModel.ID,
                Email = adminModel.Email,
                Login = adminModel.Login,
                NomeTratamento = adminModel.NomeTratamento,
                Senha = adminModel.Senha
            });
            return RedirectToAction("Index");
        }

        public ActionResult Details(int idAdmin) {
            return View(new AdminModel(adminRepository).RetornaAdmin(idAdmin));
        }
    }
}