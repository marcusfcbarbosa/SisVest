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
    public class EFVestibularRepository : IVestibularRepository
    {
        VestContext vestContext;

        public EFVestibularRepository(VestContext vestContext)
        {
            this.vestContext = vestContext;
        }

        public IQueryable<Vestibular> Vestibulares
        {
            get { return vestContext.Vestibulares.AsQueryable(); }
        }

        public void Inserir(Vestibular vestibular)
        {
            var retorno = from v in Vestibulares
                          where v.Descricao == vestibular.Descricao
                          select v;
            if (retorno.Count() > 0)
            {
                throw new InvalidOperationException("Vestibular com a mesma descrição");
            }
            else
            {
                try
                {
                    vestContext.Vestibulares.Add(vestibular);
                    vestContext.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
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
                    vestContext.Entry(vestibular).State = System.Data.Entity.EntityState.Detached;
                    throw new InvalidOperationException(msgErro.ToString());
                    

                }
            }
        }

        public void Alterar(Vestibular vestibular)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Exclui Vestibular com base no ID
        /// </summary>
        /// <param name="idVestibular"></param>
        public void excluir(int idVestibular)
        {
            vestContext.Vestibulares.Remove(vestContext.Vestibulares.Where(x => x.ID == idVestibular).FirstOrDefault());
        }

        /// <summary>
        /// Retorna vestibular com base nos candidatos
        /// </summary>
        /// <param name="idVestibular"></param>
        /// <returns></returns>
        public IList<Candidato> RetornarCandidatosPorVestibular(int idVestibular)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retorna o Vestibular com base na PK
        /// </summary>
        /// <param name="idVestibular"></param>
        /// <returns></returns>
        public Vestibular Retorna(int idVestibular)
        {
            return Vestibulares.Where(x => x.ID == idVestibular).FirstOrDefault();
        }
    }
}