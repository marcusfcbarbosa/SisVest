using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SisVest.WebUI.Models
{

    /// <summary>
    /// Classe util, apenas na aplicaçao
    /// </summary>
    public class CursoModel
    {
        
        public int ID { get; set; }
        public string Descricao { get; set; }

        public int Vagas { get; set; }

        public int TotalCandidatosAprovados { get; set; }

        public int TotalCandidatos { get; set; }
    }
}