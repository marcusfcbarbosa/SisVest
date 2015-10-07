using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisVest.DomaninModel.Entities;

namespace SisVest.DomaninModel.Abstract
{
    public interface ICursoRepository
    {
        /// <summary>
        /// Para consultas LINQ
        /// </summary>
        IQueryable<Curso> Cursos { get; }

        /// <summary>
        /// Insere um novo curso
        /// </summary>
        /// <param name="curso"></param>
        void InserirCurso(Curso curso);

        /// <summary>
        /// Retorna o curso com base no ID
        /// </summary>
        /// <param name="idCurso"></param>
        /// <returns></returns>
        Curso RetornaCurso(int idCurso);

        /// <summary>
        /// Atualiza o Curso
        /// </summary>
        /// <param name="curso"></param>
        void AtualizaCurso(Curso curso);

        /// <summary>
        /// Exclui com base no ID
        /// </summary>
        /// <param name="idCurso"></param>
        void Excluir(int idCurso);

        /// <summary>
        /// Retorna todos os cursos
        /// </summary>
        /// <returns></returns>
        IList<Curso> RetornaTodos();

    }
}