using System.Security.Claims;
using GerenciamentoProducao.Interfaces;
using GerenciamentoProducao.Models;
using GerenciamentoProducao.Repositories;
using GerenciamentoProducaoo.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace GerenciamentoProducao.Controllers;
//[Authorize(Roles = "Administrador,Gerente")]
public class UsuarioController : Controller
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITipoUsuarioRepository _tipoUsuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository,
            ITipoUsuarioRepository tipoUsuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
        _tipoUsuarioRepository = tipoUsuarioRepository;
    }

    //INDEX LISTA
    public async Task<IActionResult> Index()
    {
        var lista = await _usuarioRepository.GetAllAtivosAsync();
        return View(lista);
    }


    //CREATE
    [HttpGet]
    //[Authorize(Roles = "Administrador,Gerente")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Create()
    {
        var vm = await CriarUsuarioViewModel();
        return View(vm);
    }
    [HttpPost]
    // POST: Usuario/Create
    public async Task<IActionResult> Create(UsuarioViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            var vm = await CriarUsuarioViewModel(viewModel);
            return View(vm);
        }

        var usuario = new Usuario
        {
            NomeUsuario = viewModel.NomeUsuario,
            Email = viewModel.Email,
            Senha = viewModel.Senha,
            Telefone = viewModel.Telefone,
            IdTipoUsuario = viewModel.IdTipoUsuario,
            Ativo = true
        };

        await _usuarioRepository.AddAsync(usuario);
        return RedirectToAction(nameof(Index));
    }


    // EDIT Usuario
    //[Authorize(Roles = "Administrador,Gerente")]
    public async Task<IActionResult> Edit(int id)
    {
        if (id <= 0) return NotFound();

        var usuario = await _usuarioRepository.GetById(id);
        if (usuario == null) return NotFound();

        var vm = new UsuarioViewModel
        {
            IdUsuario = usuario.IdUsuario,
            NomeUsuario = usuario.NomeUsuario,
            Email = usuario.Email,
            Senha = usuario.Senha,
            Telefone=usuario.Telefone,
            IdTipoUsuario = usuario.IdTipoUsuario,
            TiposUsuario = (await _tipoUsuarioRepository.GetAllAsync()).Select(t => new SelectListItem
            {
                Value = t.IdTipoUsuario.ToString(),
                Text = t.NomeTipoUsuario
            })
        };

        return View(vm);
    }

    [HttpPost]
    //[Authorize(Roles = "Administrador,Gerente")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UsuarioViewModel viewModel)
    {
        if (id != viewModel.IdUsuario) return NotFound();

        if (!ModelState.IsValid)
        {
            viewModel = await CriarUsuarioViewModel(viewModel);
            return View(viewModel);
        }

        var usuario = await _usuarioRepository.GetById(id);
        if (usuario == null) return NotFound();

        usuario.NomeUsuario = viewModel.NomeUsuario;
        usuario.Email = viewModel.Email;
        usuario.Senha = viewModel.Senha;
        usuario.Telefone = viewModel.Telefone;
        usuario.IdTipoUsuario = viewModel.IdTipoUsuario;

        await _usuarioRepository.UpdateAsync(usuario);
        return RedirectToAction(nameof(Index));
    }


    //DELETE Usuario
    //[Authorize(Roles = "Administrador")]
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0) return NotFound();

        var usuario = await _usuarioRepository.GetById(id);
        if (usuario == null) return NotFound();

        var vm = new UsuarioViewModel
        {
            IdUsuario = usuario.IdUsuario,
            NomeUsuario = usuario.NomeUsuario,
            Email = usuario.Email,
            Senha = usuario.Senha,
            Telefone = usuario.Telefone,
            IdTipoUsuario = usuario.IdTipoUsuario,
            Ativo = usuario.Ativo
            // TiposUsuario pode ser preenchido se necessário para a view
        };

        return View(vm);
    }

    [HttpPost, ActionName("Delete")]
    //[Authorize(Roles = "Administrador")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _usuarioRepository.InativarAsync(id);
        return RedirectToAction(nameof(Index));
    }

    //[Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Inativos()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();
        var inativos = usuarios.Where(u => !u.Ativo)
                               .OrderByDescending(u => u.IdUsuario)
                               .ToList();
        return View(inativos);
    }
    //[Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Ativar(int id)
    {
        if (id <= 0) return NotFound();

        var usuario = await _usuarioRepository.GetById(id);
        if (usuario == null) return NotFound();

        usuario.Ativo = true;
        await _usuarioRepository.UpdateAsync(usuario);

        return RedirectToAction(nameof(Inativos));
    }




    // -------- Apoio --------
    private async Task<UsuarioViewModel> CriarUsuarioViewModel(UsuarioViewModel? model = null)
    {
        var tipos = await _tipoUsuarioRepository.GetAllAsync();
        return new UsuarioViewModel
        {
            IdUsuario = model?.IdUsuario ?? 0,
            NomeUsuario = model?.NomeUsuario,
            Email = model?.Email,
            Senha = model?.Senha,
            Telefone = model?.Telefone,
            IdTipoUsuario = model?.IdTipoUsuario ?? 0,
            TiposUsuario = tipos.Select(t => new SelectListItem
            {
                Value = t.IdTipoUsuario.ToString(),
                Text = t.NomeTipoUsuario
            })
        };
    }


}
