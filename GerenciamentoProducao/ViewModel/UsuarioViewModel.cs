using GerenciamentoProducao.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoProducaoo.ViewModel;

public class UsuarioViewModel
{
    public int IdUsuario { get; set; }
    public string NomeUsuario { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Telefone { get; set; }
    public int IdTipoUsuario { get; set; }
    public bool Ativo { get; set; } = true;

    //colecao para popular o dropdownlist
    public IEnumerable<SelectListItem>? TiposUsuario { get; set; }

}
