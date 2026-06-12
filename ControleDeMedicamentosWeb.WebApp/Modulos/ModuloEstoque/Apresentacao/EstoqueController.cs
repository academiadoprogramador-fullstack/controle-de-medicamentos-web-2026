using AutoMapper;
using FluentResults;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Apresentacao.Extensions;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Apresentacao;

public class EstoqueController(ServicoEstoque servicoEstoque, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult RegistrarEntrada(Guid medicamentoId)
    {
        Result<DetalhesEstoqueMedicamentoDto> resultado = servicoEstoque.SelecionarMedicamento(medicamentoId);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction("Listar", "Medicamento");
        }

        RegistrarEntradaViewModel entradaVm = CriarEntradaViewModel(resultado.Value, Guid.Empty, 0);

        return View(entradaVm);
    }

    [HttpPost]
    public ActionResult RegistrarEntrada(RegistrarEntradaViewModel entradaVm)
    {
        if (!ModelState.IsValid)
            return View(RecarregarEntrada(entradaVm));

        RegistrarEntradaDto dto = mapeador.Map<RegistrarEntradaDto>(entradaVm);
        Result resultado = servicoEstoque.RegistrarEntrada(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(RecarregarEntrada(entradaVm));
        }

        return RedirectToAction(nameof(Historico), new { medicamentoId = entradaVm.MedicamentoId });
    }

    [HttpGet]
    public ActionResult RegistrarSaida(Guid medicamentoId)
    {
        Result<DetalhesEstoqueMedicamentoDto> resultado = servicoEstoque.SelecionarMedicamento(medicamentoId);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction("Listar", "Medicamento");
        }

        RegistrarSaidaViewModel saidaVm = CriarSaidaViewModel(resultado.Value, Guid.Empty, 0);

        return View(saidaVm);
    }

    [HttpPost]
    public ActionResult RegistrarSaida(RegistrarSaidaViewModel saidaVm)
    {
        if (!ModelState.IsValid)
            return View(RecarregarSaida(saidaVm));

        RegistrarSaidaDto dto = mapeador.Map<RegistrarSaidaDto>(saidaVm);
        Result resultado = servicoEstoque.RegistrarSaida(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(RecarregarSaida(saidaVm));
        }

        return RedirectToAction(nameof(Historico), new { medicamentoId = saidaVm.MedicamentoId });
    }

    [HttpGet]
    public ActionResult Historico(Guid medicamentoId)
    {
        Result<DetalhesEstoqueMedicamentoDto> resultado = servicoEstoque.SelecionarMedicamento(medicamentoId);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction("Listar", "Medicamento");
        }

        DetalhesEstoqueMedicamentoDto medicamento = resultado.Value;
        List<ListarRequisicoesEntradaViewModel> entradas = mapeador
            .Map<List<ListarRequisicoesEntradaViewModel>>(servicoEstoque.SelecionarEntradas(medicamentoId));
        List<ListarRequisicoesSaidaViewModel> saidas = mapeador
            .Map<List<ListarRequisicoesSaidaViewModel>>(servicoEstoque.SelecionarSaidas(medicamentoId));

        HistoricoEstoqueViewModel historicoVm = new HistoricoEstoqueViewModel(
            medicamento.MedicamentoId,
            medicamento.MedicamentoNome,
            medicamento.MedicamentoDescricao,
            medicamento.FornecedorNome,
            medicamento.QuantidadeEmEstoque,
            entradas,
            saidas
        );

        return View(historicoVm);
    }

    private RegistrarEntradaViewModel RecarregarEntrada(RegistrarEntradaViewModel entradaVm)
    {
        Result<DetalhesEstoqueMedicamentoDto> resultado = servicoEstoque.SelecionarMedicamento(entradaVm.MedicamentoId);

        if (resultado.IsFailed)
            return entradaVm with { Funcionarios = SelecionarFuncionarios() };

        return CriarEntradaViewModel(resultado.Value, entradaVm.FuncionarioId, entradaVm.Quantidade);
    }

    private RegistrarSaidaViewModel RecarregarSaida(RegistrarSaidaViewModel saidaVm)
    {
        Result<DetalhesEstoqueMedicamentoDto> resultado = servicoEstoque.SelecionarMedicamento(saidaVm.MedicamentoId);

        if (resultado.IsFailed)
            return saidaVm with { Pacientes = SelecionarPacientes() };

        return CriarSaidaViewModel(resultado.Value, saidaVm.PacienteId, saidaVm.Quantidade);
    }

    private RegistrarEntradaViewModel CriarEntradaViewModel(
        DetalhesEstoqueMedicamentoDto medicamento,
        Guid funcionarioId,
        uint quantidade
    )
    {
        return new RegistrarEntradaViewModel(
            medicamento.MedicamentoId,
            medicamento.MedicamentoNome,
            medicamento.MedicamentoDescricao,
            medicamento.FornecedorNome,
            medicamento.QuantidadeEmEstoque,
            funcionarioId,
            quantidade,
            SelecionarFuncionarios()
        );
    }

    private RegistrarSaidaViewModel CriarSaidaViewModel(
        DetalhesEstoqueMedicamentoDto medicamento,
        Guid pacienteId,
        uint quantidade
    )
    {
        return new RegistrarSaidaViewModel(
            medicamento.MedicamentoId,
            medicamento.MedicamentoNome,
            medicamento.MedicamentoDescricao,
            medicamento.FornecedorNome,
            medicamento.QuantidadeEmEstoque,
            pacienteId,
            quantidade,
            SelecionarPacientes()
        );
    }

    private List<OpcaoFuncionarioViewModel> SelecionarFuncionarios()
    {
        return mapeador.Map<List<OpcaoFuncionarioViewModel>>(servicoEstoque.SelecionarFuncionarios());
    }

    private List<OpcaoPacienteViewModel> SelecionarPacientes()
    {
        return mapeador.Map<List<OpcaoPacienteViewModel>>(servicoEstoque.SelecionarPacientes());
    }
}
