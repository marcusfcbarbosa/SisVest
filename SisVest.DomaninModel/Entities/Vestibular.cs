using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace SisVest.DomaninModel.Entities
{
    [Table("tbVestibular")]
    public class Vestibular
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage="Descrição Obrigatório")]
        public String Descricao { get; set; }

        [Required(ErrorMessage = "Data de inicio Obrigatório")]
        public DateTime? DataInicio { get; set; }

        [Required(ErrorMessage = "Data fim Obrigatório")]
        public DateTime? DataFim { get; set; }

        [Required(ErrorMessage = "Data prova Obrigatório")]
        public DateTime? DataProva { get; set; }

        /// <summary>
        /// Por estar usando objetos e trabalhar com EF, ele necessita ser virtual
        /// para poder sobreescrever
        /// </summary>
        public virtual ICollection<Candidato> CandidatosList { get; set; }

        /// <summary>
        /// Método para as assertivas
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var vestibularParm = (Vestibular)obj;
            if (this.Descricao == vestibularParm.Descricao || this.DataInicio == vestibularParm.DataInicio || this.DataFim == vestibularParm.DataFim)
            {
                return true;
            }
            return false;
        }
    }
}
