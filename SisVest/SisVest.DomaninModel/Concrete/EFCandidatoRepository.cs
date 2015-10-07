using SisVest.DomaninModel.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisVest.DomaninModel.Entities;


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
            else {
                vestContext.Candidatos.Add(candidato);
                vestContext.SaveChanges();
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
            var aprovaCandidato = vestContext.Candidatos.Where(x => x.ID == idCandidato).FirstOrDefault();
            aprovaCandidato.Aprovado = true;
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
