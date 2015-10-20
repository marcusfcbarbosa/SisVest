using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SisVest.DomaninModel.Abstract;
using System.ComponentModel.DataAnnotations;

namespace SisVest.WebUI.Models
{
    public class VestibularModel
    {

        private IVestibularRepository repository;

        public VestibularModel() { }
        public VestibularModel(IVestibularRepository vestRepository)
        {
            repository = vestRepository;
        }

        public int ID { get; set; }

        public String Descricao { get; set; }


        [Required(ErrorMessage = "Data de inicio Obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd//MM/yyyy}")]
        public DateTime? DataInicio { get; set; }


        [Required(ErrorMessage = "Data Fim Obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd//MM/yyyy}")]
        public DateTime? DataFim { get; set; }

        [Required(ErrorMessage = "Data Prova Obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd//MM/yyyy}")]
        public DateTime? DataProva { get; set; }



        public IList<VestibularModel> RetornaTodos()
        {
            var result = repository.Vestibulares.ToList();

            List<VestibularModel> vestibularModelList = new List<VestibularModel>();

            foreach (var vest in result)
            {
                try
                {
                    vestibularModelList.Add(new VestibularModel(repository)
                    {
                        ID = vest.ID,
                        Descricao = vest.Descricao,
                        DataProva = vest.DataProva,
                        DataInicio = vest.DataInicio,
                        DataFim = vest.DataFim
                    });
                }
                catch
                {
                    continue;
                }
            }
            return vestibularModelList;
        }

        public VestibularModel RetornaVestibular(int idVestibular) {
            return RetornaTodos().Where(x => x.ID == idVestibular).FirstOrDefault();
        }


    }
}