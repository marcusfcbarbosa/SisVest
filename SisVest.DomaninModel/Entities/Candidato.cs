using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisVest.DomaninModel.Entities
{
    [Table("tbCandidato")]
    public class Candidato
    {
        [Key]
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Nome é Obrigatório")]
        public string Nome { get; set; }

        public string Telefone { get; set; }

        [Required(ErrorMessage = "E-mail Obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Data de Nascimento Obrigatório")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Senha é obrigatoria")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Sexo é obrigatorio")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "CPF obrigatorio")]
        public String Cpf { get; set; }

        /// <summary>
        /// Por estar usando objetos e trabalhar com EF, ele necessita ser virtual
        /// para poder sobreescrever
        /// </summary>
        public virtual Vestibular Vestibular { get; set; }

        /// <summary>
        /// Por estar usando objetos e trabalhar com EF, ele necessita ser virtual
        /// para poder sobreescrever
        /// </summary>
        [Required(ErrorMessage = "Curso obrigatorio")]
        public virtual Curso Curso { get; set; }


        public bool Aprovado { get; set; }

        /// <summary>
        /// Faz a comparação de qualquer objeto feito com o metodo Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var candidatoParm = (Candidato)obj;
            if (this.ID == candidatoParm.ID || this.Cpf == candidatoParm.Cpf || this.Email == candidatoParm.Email)
            {
                return true;
            }
            return false;
        }

    }
}
