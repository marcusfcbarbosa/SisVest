using SisVest.DomaninModel.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisVest.DomaninModel.Entities;
using System.Data.Entity.Validation;


namespace SisVest.DomaninModel.Concrete
{
    public class EFCandidatoRepository : ICandidatoRepository
    {
        VestContext vestContext = new VestContext();

        /// <summary>
        /// Setando manualmente o contexto dentro do Repositorio
        /// </summary>
        /// <param name="vestContext"></param>
        public EFCandidatoRepository(VestContext vestContext)
        {
            this.vestContext = vestContext;
        }

        public IQueryable<Candidato> Candidatos
        {
            get { return vestContext.Candidatos.AsQueryable(); }
        }

        public void RealizaInscricao(Candidato candidato)
        {
            var retorno = from a in Candidatos
                          where a.Cpf == candidato.Cpf || a.Email == candidato.Email
                          select a;
            if (retorno.Count() > 0)
            {
                throw new InvalidOperationException("CPF ou e-mail já inscrito");
            }
            else
            {
                try
                {
                    vestContext.Candidatos.Add(candidato);
                    vestContext.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    //Estoura as validaçoes que foram feitas no model
                    StringBuilder msgErro = new StringBuilder();
                    var erros = vestContext.GetValidationErrors();
                    //já informa quais campos que são do tipo [Required]
                    //e que não foram preenchidos
                    foreach (var erro in erros)
                    {
                        foreach (var detalheErro in erro.ValidationErrors)
                        {
                            msgErro.Append(detalheErro.ErrorMessage);
                            msgErro.Append('\n');
                        }
                    }
                    vestContext.Entry(candidato).State = System.Data.Entity.EntityState.Detached;
                    throw new InvalidOperationException(msgErro.ToString());
                }
            }
        }

        public void AtualizaCadastro(Candidato candidato)
        {
            var atualizaCandidato = vestContext.Candidatos.Where(x => x.ID == candidato.ID).FirstOrDefault();
            atualizaCandidato.Nome = candidato.Nome;
            atualizaCandidato.Senha = candidato.Senha;
            atualizaCandidato.Sexo = candidato.Sexo;
            atualizaCandidato.Telefone = candidato.Telefone;
            vestContext.SaveChanges();
        }

        public void Excluir(int idCandidato)
        {
            vestContext.Candidatos.Remove(vestContext.Candidatos.Where(x => x.ID == idCandidato).FirstOrDefault());
        }

        public void Aprovar(int idCandidato)
        {
            var candidato = vestContext.Candidatos.Where(x => x.ID == idCandidato).FirstOrDefault();
            var totalVagasCurso = vestContext.Cursos.Where(x => x.ID == candidato.Curso.ID).Select(x => new { x.Vagas }).FirstOrDefault();

            var resultado = (from cur in vestContext.Cursos
                             from cand in cur.CandidatosList
                             where cur.ID == candidato.Curso.ID && cand.Aprovado
                             select cand).Count();

            if (resultado == totalVagasCurso.Vagas) {
                throw new InvalidOperationException("O curso já está lotado e não pode mais receber aprovação");
            }

            candidato.Aprovado = true;
            vestContext.SaveChanges();
        }

        public Candidato Retornar(int idCandidato)
        {
            return vestContext.Candidatos.Where(x => x.ID == idCandidato).FirstOrDefault();
        }

        public IList<Candidato> RetornarPorVestibularPorCurso(int idVestibular, int idCurso)
        {
            throw new NotImplementedException();
        }

        public IList<Candidato> RetornarTodos()
        {
            return vestContext.Candidatos.ToList();
        }
    }
}
