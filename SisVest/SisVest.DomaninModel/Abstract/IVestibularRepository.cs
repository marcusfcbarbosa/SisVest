using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisVest.DomaninModel.Entities;

namespace SisVest.DomaninModel.Abstract
{
    public interface IVestibularRepository
    {
        /// <summary>
        /// Para consultas de vestbular
        /// </summary>
        IQueryable<Vestibular> Vestibulares { get; }

        /// <summary>
        /// Insere um novo Vestibular
        /// </summary>
        /// <param name="vestibular"></param>
        void Inserir(Vestibular vestibular);

        /// <summary>
        /// Altera os registros do vestibular
        /// </summary>
        /// <param name="vestibular"></param>
        void Alterar(Vestibular vestibular);

        /// <summary>
        /// Exclui o Vestibular com base no ID
        /// </summary>
        /// <param name="idVestibular"></param>
        void excluir(int idVestibular);
        /// <summary>
        /// Retorna todos os candidatos por vestibular
        /// </summary>
        /// <param name="idVestibular"></param>
        /// <returns></returns>
        IList<Candidato> RetornarCandidatosPorVestibular(int idVestibular);

        /// <summary>
        /// Retorno o Vestibular com base no ID
        /// </summary>
        /// <param name="idVestibular"></param>
        /// <returns></returns>
        Vestibular Retorna(int idVestibular);

    }
}