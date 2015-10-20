using SisVest.DomaninModel.Abstract;
using SisVest.DomaninModel.Entities;
using SisVest.WebUI.Infraestrutura.Filter;
using SisVest.WebUI.Infraestrutura.Provider.Abstract;
using SisVest.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SisVest.WebUI.Controllers
{
    [CustomAutenticacao("administrador")]
    public class AdminController : Controller
    {
        private IAdminRepository adminRepository;
        private IAutenticacaoProvider autenticacaoProvider;

        public AdminController(IAdminRepository repository, IAutenticacaoProvider autenticacaoProviderParam)
        {
            adminRepository = repository;
            autenticacaoProvider = autenticacaoProviderParam;
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
            try {
                adminRepository.Alterar(new Admin
                {
                    ID = adminModel.ID,
                    Email = adminModel.Email,
                    Login = adminModel.Login,
                    NomeTratamento = adminModel.NomeTratamento,
                    Senha = adminModel.Senha
                });
                TempData["Mensagem"] = "Administrador editado com sucesso!!";

            }catch(Exception ex){
                TempData["Mensagem"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int idAdmin)
        {
            return View(new AdminModel(adminRepository).RetornaAdmin(idAdmin));
        }

        public ActionResult Delete(int idAdmin)
        {
            try
            {
                if (autenticacaoProvider.UsuarioAutenticado.Login != adminRepository.Retornar(idAdmin).Login)
                {
                    adminRepository.Excluir(idAdmin);
                    TempData["Mensagem"] = "Administrador excluído com sucesso!!";
                }
                else {
                    TempData["Mensagem"] = "Não se pode excluir o usuario com o que esta autenticado no sistema";
                }
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}