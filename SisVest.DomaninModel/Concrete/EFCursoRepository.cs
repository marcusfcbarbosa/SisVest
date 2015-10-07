using SisVest.DomaninModel.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisVest.DomaninModel.Entities;

namespace SisVest.DomaninModel.Concrete
{
    /// <summary>
    /// Classe Concreta que é implementada pela Interface
    /// </summary>
    public class EFCursoRepository : ICursoRepository
    {
        VestContext vestContext;
        /// <summary>
        /// Injetando a dependencia manualmente
        /// </summary>
        public EFCursoRepository(VestContext context)
        {
            vestContext = context;
        }

        /// <summary>
        /// Retorna um objeto consultavel
        /// </summary>
        public IQueryable<Curso> Cursos
        {
            get { return vestContext.Cursos.AsQueryable(); }
        }

        /// <summary>
        /// Insere com uma validação inicial
        /// </summary>
        /// <param name="curso"></param>
        public void InserirCurso(Curso curso)
        {
            var retorno = from c in vestContext.Cursos
                          where c.Descricao == curso.Descricao
                          select c;
            if (retorno.Count() > 0)
            {
                throw new InvalidOperationException("Já existe um curso com essa mesma descrição");
            }
            else
            {
                vestContext.Cursos.Add(curso);
                vestContext.SaveChanges();
            }
        }

        /// <summary>
        /// Retorna o curso por ID
        /// </summary>
        /// <param name="idCurso"></param>
        /// <returns></returns>
        public Curso RetornaCurso(int idCurso)
        {
            return vestContext.Cursos.Where(x => x.ID == idCurso).FirstOrDefault();
        }

        /// <summary>
        /// Atualzia Curso
        /// </summary>
        /// <param name="curso"></param>
        public void AtualizaCurso(Curso curso)
        {
            var atualiza = vestContext.Cursos.Where(x => x.ID == curso.ID).FirstOrDefault();
            atualiza.Descricao = curso.Descricao;
            atualiza.Vagas = curso.Vagas;
            vestContext.SaveChanges();
        }

        /// <summary>
        /// Exclui o curso com base no ID
        /// </summary>
        /// <param name="idCurso"></param>
        public void Excluir(int idCurso)
        {
            vestContext.Cursos.Remove(vestContext.Cursos.Where(x => x.ID == idCurso).FirstOrDefault());
            vestContext.SaveChanges();
        }

        /// <summary>
        /// Retorna todos os cursos
        /// </summary>
        /// <returns></returns>
        public IList<Curso> RetornaTodos()
        {
            return vestContext.Cursos.ToList();
        }

        /// <summary>
        /// Retorno será um IQUERYABLE
        /// </summary>
        /// <param name="idCurso"></param>
        /// <returns></returns>
        public IQueryable<Candidato> CandidatosAprovados(int idCurso)
        {
            return (from cur in vestContext.Cursos
                    from cand in cur.CandidatosList
                    where cur.ID == idCurso && cand.Aprovado
                    select cand);
        }
    }
}
