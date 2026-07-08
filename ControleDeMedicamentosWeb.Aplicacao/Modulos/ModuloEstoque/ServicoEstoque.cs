using FluentResults;
using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloEstoque;
using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloFuncionario;
using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloMedicamento;
using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloPaciente;

namespace ControleDeMedicamentosWeb.Aplicacao.Modulos.ModuloEstoque;

public class ServicoEstoque
{
    private readonly IRepositorioRequisicao repositorioRequisicao;
    private readonly IRepositorioMedicamento repositorioMedicamento;
    private readonly IRepositorioFuncionario repositorioFuncionario;
    private readonly IRepositorioPaciente repositorioPaciente;

    public ServicoEstoque(
        IRepositorioRequisicao repositorioRequisicao,
        IRepositorioMedicamento repositorioMedicamento,
        IRepositorioFuncionario repositorioFuncionario,
        IRepositorioPaciente repositorioPaciente
    )
    {
        this.repositorioRequisicao = repositorioRequisicao;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFuncionario = repositorioFuncionario;
        this.repositorioPaciente = repositorioPaciente;
    }

    public List<DetalhesEstoqueMedicamentoDto> SelecionarDetalhesEstoque()
    {
        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarTodos();

        return medicamentos.Select(m => new DetalhesEstoqueMedicamentoDto(
            m.Id,
            m.Nome,
            m.Descricao,
            m.Fornecedor.Nome,
            m.QuantidadeEmEstoque
        )).ToList();
    }

    public List<OpcaoFuncionarioDto> SelecionarOpcoesFuncionario()
    {
        return repositorioFuncionario.SelecionarTodos()
            .Select(f => new OpcaoFuncionarioDto(f.Id, f.Nome))
            .ToList();
    }

    public List<OpcaoPacienteDto> SelecionarOpcoesPaciente()
    {
        return repositorioPaciente.SelecionarTodos()
            .Select(p => new OpcaoPacienteDto(p.Id, p.Nome))
            .ToList();
    }

    public Result RegistrarEntrada(RegistrarEntradaDto dto)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(dto.MedicamentoId);
        if (medicamento == null)
            return Result.Fail("Medicamento não encontrado.");

        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(dto.FuncionarioId);
        if (funcionario == null)
            return Result.Fail("Funcionário não encontrado.");

        RequisicaoEntrada requisicao = new RequisicaoEntrada(funcionario, medicamento, dto.Quantidade);
        repositorioRequisicao.Cadastrar(requisicao);

        return Result.Ok();
    }

    public Result RegistrarSaida(RegistrarSaidaDto dto)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(dto.MedicamentoId);
        if (medicamento == null)
            return Result.Fail("Medicamento não encontrado.");

        Paciente? paciente = repositorioPaciente.SelecionarPorId(dto.PacienteId);
        if (paciente == null)
            return Result.Fail("Paciente não encontrado.");

        if (medicamento.QuantidadeEmEstoque < dto.Quantidade)
            return Result.Fail("Quantidade insuficiente em estoque.");

        List<MedicamentoPrescrito> medicamentosPrescritos = new()
        {
            new MedicamentoPrescrito(medicamento, dto.Quantidade)
        };

        RequisicaoSaida requisicao = new RequisicaoSaida(paciente, medicamentosPrescritos);
        repositorioRequisicao.Cadastrar(requisicao);

        return Result.Ok();
    }

    public List<ListarRequisicoesEntradaDto> SelecionarRequisicoesEntrada()
    {
        return repositorioRequisicao.SelecionarRequisicoesEntrada()
            .Select(r => new ListarRequisicoesEntradaDto(
                r.Id,
                r.DataCriacao,
                r.Funcionario.Nome,
                r.Quantidade
            ))
            .ToList();
    }

    public List<ListarRequisicoesSaidaDto> SelecionarRequisicoesSaida()
    {
        return repositorioRequisicao.SelecionarRequisicoesSaida()
            .Select(r => new ListarRequisicoesSaidaDto(
                r.Id,
                r.DataCriacao,
                r.Paciente.Nome,
                r.MedicamentosPrescritos.Sum(mp => (int)mp.Quantidade) > 0
                    ? (uint)r.MedicamentosPrescritos.Sum(mp => (int)mp.Quantidade)
                    : 0
            ))
            .ToList();
    }
}