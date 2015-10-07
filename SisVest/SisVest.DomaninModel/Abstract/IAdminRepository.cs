using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisVest.DomaninModel.Entities;

namespace SisVest.DomaninModel.Abstract
{
    public interface IAdminRepository
    {
        /// <summary>
        /// Para consultas Linq
        /// </summary>
        IQueryable<Admin> Admins { get; }

        /// <summary>
        /// Inserção de um novo administrador
        /// </summary>
        /// <param name="admin"></param>
        void InsereAdmin(Admin admin);

        /// <summary>
        /// Exclui com base no ID
        /// </summary>
        /// <param name="idAdmin"></param>
        void Excluir(int idAdmin);

        /// <summary>
        /// Retorna o Admin com base no ID
        /// </summary>
        /// <param name="idAdmin"></param>
        /// <returns></returns>
        Admin Retornar(int idAdmin);

        /// <summary>
        /// Altera o Admin
        /// </summary>
        /// <param name="admin"></param>
        void Alterar(Admin admin);

        /// <summary>
        /// Retorna todos
        /// </summary>
        /// <returns></returns>
        IList<Admin> RetornaTodos();


    }
}