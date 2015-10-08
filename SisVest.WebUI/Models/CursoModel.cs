using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SisVest.DomaninModel.Abstract;
namespace SisVest.WebUI.Models
{

    /// <summary>
    /// Classe util, apenas na aplicaçao
    /// </summary>
    public class CursoModel
    {
        public CursoModel() { }
        
        private ICursoRepository repository;

        public CursoModel(ICursoRepository cursoRepository){
            repository = cursoRepository;
        }

        public int ID { get; set; }
        public string Descricao { get; set; }

        public int Vagas { get; set; }

        public int? TotalCandidatosAprovados { get; set; }

        public int? TotalCandidatos { get; set; }


        public IList<CursoModel> RetornaTodos() { 

            var result = repository.Cursos.ToList();

            List<CursoModel> cursoModelList = new List<CursoModel>();

            foreach (var curso in result)
            {
                try
                {
                    cursoModelList.Add(new CursoModel(repository)
                    {
                        ID = curso.ID,
                        Vagas = curso.Vagas,
                        Descricao = curso.Descricao,
                        TotalCandidatos = 10,
                        TotalCandidatosAprovados = 9
                    });
                    /*
                     TotalCandidatos = curso.CandidatosList.Count,
                        TotalCandidatosAprovados = repository.CandidatosAprovados(curso.ID).Count()
                     */
                }
                catch {
                    continue;
                }
            }
            return cursoModelList;
        }

        public CursoModel RetornaCursoModel(int idCurso)
        {
            return RetornaTodos().Where(x => x.ID == idCurso).FirstOrDefault();
        }

    }
}