using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisVest.DomaninModel.Entities;

namespace SisVest.DomaninModel.Abstract
{
    public interface ICandidatoRepository
    {
        /// <summary>
        /// Para consultas linq
        /// </summary>
        IQueryable<Candidato> Candidatos { get; }
        /// <summary>
        /// Realiza a inscrição do Candidato
        /// </summary>
        /// <param name="candidato"></param>
        void RealizaInscricao(Candidato candidato);

        /// <summary>
        /// Atualiza o cadastro
        /// </summary>
        /// <param name="candidato"></param>
        void AtualizaCadastro(Candidato candidato);

        /// <summary>
        /// Exclui o cadastro
        /// </summary>
        /// <param name="idCandidato"></param>
        void Excluir(int idCandidato);

        /// <summary>
        /// Aprovar um candidato
        /// </summary>
        /// <param name="idCandidato"></param>
        void Aprovar(int idCandidato);

        /// <summary>
        /// Retorna o candidato com base no idCandidato
        /// </summary>
        /// <param name="idCandidato"></param>
        /// <returns></returns>
        Candidato Retornar(int idCandidato);

        /// <summary>
        /// Retorna candidatos por vest e cursp
        /// </summary>
        /// <param name="idVestibular"></param>
        /// <param name="idCurso"></param>
        /// <returns></returns>
        IList<Candidato> RetornarPorVestibularPorCurso(int idVestibular, int idCurso);

        /// <summary>
        /// Retorna todos os candidatos
        /// </summary>
        /// <returns></returns>
        IList<Candidato> RetornarTodos();

    }
}