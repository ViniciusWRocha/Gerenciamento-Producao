using GerenciamentoProducao.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoProducaoo.ViewModel
{
    public class ProducaoViewModel
    {
        public int IdProducao { get; set; }
        public string NomeProducao { get; set; }      
        public DateTime DataProducao { get; set; }      
        public bool Produzido { get; set; }       
        public string? Descricao { get; set; }
        public bool EhLiberado { get; set; }


        public IEnumerable<SelectListItem>? Usuario { get; set; }

        public IEnumerable<SelectListItem>? FamiliaCaixilho { get; set; }

     
    }
}
