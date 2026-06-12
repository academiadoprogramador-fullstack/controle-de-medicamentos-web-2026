using AutoMapper;
using FluentResults;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Apresentacao.Extensions;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Apresentacao;

public class PacienteController(ServicoPaciente servicoPaciente, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarPacientesDto> dtos = servicoPaciente.SelecionarTodos();
        List<ListarPacientesViewModel> listarVms = mapeador.Map<List<ListarPacientesViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarPacienteViewModel cadastrarVm = new CadastrarPacienteViewModel(
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarPacienteViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarPacienteDto dto = mapeador.Map<CadastrarPacienteDto>(cadastrarVm);
        Result resultado = servicoPaciente.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesPacienteDto> resultado = servicoPaciente.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        EditarPacienteViewModel editarVm = mapeador.Map<EditarPacienteViewModel>(resultado.Value);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarPacienteViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarPacienteDto dto = mapeador.Map<EditarPacienteDto>(editarVm);
        Result resultado = servicoPaciente.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesPacienteDto> resultado = servicoPaciente.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        ExcluirPacienteViewModel excluirVm = mapeador.Map<ExcluirPacienteViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirPacienteViewModel excluirVm)
    {
        Result resultado = servicoPaciente.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }
}
