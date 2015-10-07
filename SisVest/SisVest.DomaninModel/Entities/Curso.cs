﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SisVest.DomaninModel.Entities
{
    [Table("tbCurso")]
    public class Curso
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public int Vagas { get; set; }

        /// <summary>
        /// Por estar usando objetos e trabalhar com EF, ele necessita ser virtual
        /// para poder sobreescrever
        /// </summary>
        public virtual ICollection<Candidato> CandidatosList { get; set; }

        /// <summary>
        /// Faz a comparação de qualquer objeto feito com o metodo Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var cursoParm = (Curso)obj;
            if (this.ID == cursoParm.ID || this.Descricao == cursoParm.Descricao)
            {
                return true;
            }
            return false;
        }

    }
}