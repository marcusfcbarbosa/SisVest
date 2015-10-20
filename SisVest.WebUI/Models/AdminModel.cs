using SisVest.DomaninModel.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SisVest.WebUI.Models
{
    public class AdminModel
    {
        private IAdminRepository adminRepository;

        public AdminModel(IAdminRepository repository)
        {
            adminRepository = repository;
        }

        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Login obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [DataType(DataType.Password)]
        public String Senha { get; set; }

        [Required(ErrorMessage = "Nome de tratamento é obrigatório")]
        public String NomeTratamento { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório")]
        public String Email { get; set; }


        public IList<AdminModel> RetornaTodos()
        {
            var result = adminRepository.RetornaTodos();
            List<AdminModel> adminModelList = new List<AdminModel>();
            foreach (var admin in result)
            {
                try
                {
                    adminModelList.Add(new AdminModel(adminRepository)
                    {
                        Email = admin.Email,
                        ID = admin.ID,
                        Login = admin.Login,
                        NomeTratamento = admin.NomeTratamento,
                        Senha = admin.Senha
                    });
                }
                catch
                {
                    continue;
                }
            }
            return adminModelList;
        }

        public AdminModel RetornaAdmin(int idAdmin)
        {
            return RetornaTodos().Where(x => x.ID == idAdmin).FirstOrDefault();
        }
    }
}