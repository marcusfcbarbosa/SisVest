using SisVest.DomaninModel.Abstract;
using SisVest.WebUI.Infraestrutura.Provider.Abstract;
using SisVest.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SisVest.WebUI.Infraestrutura.Provider.Concrete
{
    public class CustomAutenticacaoProvider : IAutenticacaoProvider
    {

        private IAdminRepository adminRepository;


        public CustomAutenticacaoProvider(IAdminRepository repository)
        {
            adminRepository = repository;
        }

        /// <summary>
        /// Armazena a autenticação em uma sessão
        /// </summary>
        /// <param name="autenticacaoModel"></param>
        /// <param name="msgErro"></param>
        /// <returns></returns>
        public bool Autenticar(AutenticacaoModel autenticacaoModel, out string msgErro, string grupo = "administrador")
        {
            msgErro = String.Empty;
            var usuario = adminRepository.Admins.Where(x => x.Login == autenticacaoModel.Login).FirstOrDefault();

            if (usuario == null)
            {
                msgErro = "Login não pertenece a nenhum usuario";
                return false;
            }


            if (usuario.Senha != autenticacaoModel.Senha)
            {
                msgErro = "Senha Incorreta";
                return false;
            }

            HttpContext.Current.Session["autenticacao"] = new AutenticacaoModel
            {
                Grupo = "administrador",
                Login = autenticacaoModel.Login,
                Senha = autenticacaoModel.Senha,
                NomeTratamento = usuario.NomeTratamento
            };
            return true;
        }

        public void Desautenticar()
        {
            HttpContext.Current.Session.Remove("autenticacao");
        }

        public bool Autenticado
        {
            

            get
            {
                return

                    HttpContext.Current.Session["autenticacao"] != null

                    &&

                    HttpContext.Current.Session["autenticacao"].GetType() == typeof(AutenticacaoModel);
            }
        }

        public Models.AutenticacaoModel UsuarioAutenticado
        {
            get
            {
                if (Autenticado)
                {
                    return (AutenticacaoModel)HttpContext.Current.Session["autenticacao"];
                }
                else
                {
                    return null;
                }
            }
        }
    }
}