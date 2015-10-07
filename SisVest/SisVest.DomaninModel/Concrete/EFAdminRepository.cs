using SisVest.DomaninModel.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisVest.DomaninModel.Entities;

namespace SisVest.DomaninModel.Concrete
{
    public class EFAdminRepository : IAdminRepository
    {
        VestContext vestContext;

        /// <summary>
        /// Injetando a dependencia manualmente
        /// </summary>
        public EFAdminRepository(VestContext context)
        {
            vestContext = context;
        }

        public void InsereAdmin(Admin admin)
        {
            if (vestContext.Admins.Where(x => x.Email == admin.Email || x.Login == admin.Login).FirstOrDefault() != null)
            {
                throw new InvalidOperationException("Email ou login já persistido");
            }
            else {
                vestContext.Admins.Add(admin);
                vestContext.SaveChanges();
            }
        }

        public void Excluir(int idAdmin)
        {
            vestContext.Admins.Remove(vestContext.Admins.Where(x=>x.ID== idAdmin).FirstOrDefault());
            vestContext.SaveChanges();
        }

        public Admin Retornar(int idAdmin)
        {
            return vestContext.Admins.Where(x => x.ID == idAdmin).FirstOrDefault();
        }

        public void Alterar(Admin admin)
        {
            var retorno = vestContext.Admins.Where(x => x.ID == admin.ID).FirstOrDefault();
            retorno.Login = admin.Login;
            retorno.NomeTratamento = admin.NomeTratamento;
            retorno.Senha = admin.Senha;
            retorno.Email = admin.Email;
            vestContext.SaveChanges();
        }

        public IList<Admin> RetornaTodos()
        {
            return vestContext.Admins.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        public IQueryable<Admin> Admins
        {
            ///Retorna como uma queryable, objeto consultavel
            get { return vestContext.Admins.AsQueryable(); }
        }
    }
}