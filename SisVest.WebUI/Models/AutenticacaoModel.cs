using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SisVest.WebUI.Models
{
    public class AutenticacaoModel
    {
        [Required(ErrorMessage="Login obrigatório")]
        public string Login { get;set;}

        [Required(ErrorMessage="Senha obrigatório")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        /// <summary>
        /// Grupo do candidato e do Admin
        /// </summary>
        [HiddenInput]
        public String Grupo { get; set; }

    }
}