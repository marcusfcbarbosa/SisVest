using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisVest.DomaninModel.Entities
{
    /// <summary>
    /// Classe que guarda os usuários que terão permissão de acesso administrativo ao sistema
    /// </summary>
    [Table("tbAdmin")]
    public class Admin
    {
        [Key]
        public int ID { get; set; }

        public string Login { get; set; }

        public String Senha { get; set; }

        public String NomeTratamento { get; set; }

        public String Email { get; set; }


        /// <summary>
        /// Faz a comparação de qualquer objeto feito com o metodo Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var adminParm = (Admin)obj;
            if (this.ID == adminParm.ID || this.Login == adminParm.Login || this.Email == adminParm.Email)
            {
                return true;
            }
            return false;
        }
    }
}