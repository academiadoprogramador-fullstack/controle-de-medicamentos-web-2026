using FluentResults;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Aplicacao;

public class ServicoEstoque
{
    private readonly IRepositorioRequisicao repositorioRequisicao;
    private readonly IRepositorioMedicamento repositorioMedicamento;
    private readonly IRepositorioFuncionario repositorioFuncionario;
    private readonly IRepositorioPaciente repositorioPaciente;
    private readonly ILogger<ServicoEstoque> logger;

    public ServicoEstoque(
        IRepositorioRequisicao repositorioRequisicao,
        IRepositorioMedicamento repositorioMedicamento,
        IRepositorioFuncionario repositorioFuncionario,
        IRepositorioPaciente repositorioPaciente,
        ILogger<ServicoEstoque> logger
    )
    {
        this.repositorioRequisicao = repositorioRequisicao;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFuncionario = repositorioFuncionario;
        this.repositorioPaciente = repositorioPaciente;
        this.logger = logger;
    }

    public Result RegistrarEntrada(RegistrarEntradaDto dto)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(dto.MedicamentoId);

        if (medicamento == null)
        {
            logger.LogWarning(
                "Entrada de estoque recusada porque o medicamento não foi encontrado. MedicamentoId: {MedicamentoId}",
                dto.MedicamentoId
            );

            return Result.Fail("Medicamento nao encontrado.");
        }

        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(dto.FuncionarioId);

        if (funcionario == null)
        {
            logger.LogWarning(
                "Entrada de estoque recusada porque o funcionário não foi encontrado. MedicamentoId: {MedicamentoId}, FuncionarioId: {FuncionarioId}",
                dto.MedicamentoId,
                dto.FuncionarioId
            );

            return Falha(nameof(dto.FuncionarioId), "Selecione um funcionario valido.");
        }

        if (dto.Quantidade == 0)
            return Falha(nameof(dto.Quantidade), "A quantidade deve ser maior que zero.");

        RequisicaoEntrada requisicaoEntrada = new RequisicaoEntrada(funcionario, medicamento, dto.Quantidade);

        repositorioRequisicao.Cadastrar(requisicaoEntrada);

        logger.LogInformation(
            "Entrada de estoque registrada. RequisicaoId: {RequisicaoId}, MedicamentoId: {MedicamentoId}, FuncionarioId: {FuncionarioId}, Quantidade: {Quantidade}",
            requisicaoEntrada.Id,
            medicamento.Id,
            funcionario.Id,
            dto.Quantidade
        );

        return Result.Ok();
    }

    public Result RegistrarSaida(RegistrarSaidaDto dto)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(dto.MedicamentoId);

        if (medicamento == null)
        {
            logger.LogWarning(
                "Saída de estoque recusada porque o medicamento não foi encontrado. MedicamentoId: {MedicamentoId}",
                dto.MedicamentoId
            );

            return Result.Fail("Medicamento nao encontrado.");
        }

        Paciente? paciente = repositorioPaciente.SelecionarPorId(dto.PacienteId);

        if (paciente == null)
        {
            logger.LogWarning(
                "Saída de estoque recusada porque o paciente não foi encontrado. MedicamentoId: {MedicamentoId}, PacienteId: {PacienteId}",
                dto.MedicamentoId,
                dto.PacienteId
            );

            return Falha(nameof(dto.PacienteId), "Selecione um paciente valido.");
        }

        if (dto.Quantidade == 0)
            return Falha(nameof(dto.Quantidade), "A quantidade deve ser maior que zero.");

        if (dto.Quantidade > medicamento.QuantidadeEmEstoque)
        {
            logger.LogWarning(
                "Saída de estoque recusada por saldo insuficiente. MedicamentoId: {MedicamentoId}, QuantidadeSolicitada: {QuantidadeSolicitada}, QuantidadeDisponivel: {QuantidadeDisponivel}",
                medicamento.Id,
                dto.Quantidade,
                medicamento.QuantidadeEmEstoque
            );

            return Falha(nameof(dto.Quantidade), "A quantidade solicitada excede o estoque disponivel.");
        }

        MedicamentoPrescrito medicamentoPrescrito = new MedicamentoPrescrito(medicamento, dto.Quantidade);
        RequisicaoSaida requisicaoSaida = new RequisicaoSaida(paciente, [medicamentoPrescrito]);

        repositorioRequisicao.Cadastrar(requisicaoSaida);

        logger.LogInformation(
            "Saída de estoque registrada. RequisicaoId: {RequisicaoId}, MedicamentoId: {MedicamentoId}, PacienteId: {PacienteId}, Quantidade: {Quantidade}",
            requisicaoSaida.Id,
            medicamento.Id,
            paciente.Id,
            dto.Quantidade
        );

        return Result.Ok();
    }

    public Result<DetalhesEstoqueMedicamentoDto> SelecionarMedicamento(Guid medicamentoId)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(medicamentoId);

        if (medicamento == null)
        {
            logger.LogDebug(
                "Medicamento não encontrado durante consulta de estoque. MedicamentoId: {MedicamentoId}",
                medicamentoId
            );

            return Result.Fail("Medicamento nao encontrado.");
        }

        return Result.Ok(new DetalhesEstoqueMedicamentoDto(
            medicamento.Id,
            medicamento.Nome,
            medicamento.Descricao,
            medicamento.Fornecedor.Nome,
            medicamento.QuantidadeEmEstoque
        ));
    }

    public List<OpcaoFuncionarioDto> SelecionarFuncionarios()
    {
        return repositorioFuncionario
            .SelecionarTodos()
            .Select(f => new OpcaoFuncionarioDto(f.Id, f.Nome))
            .ToList();
    }

    public List<OpcaoPacienteDto> SelecionarPacientes()
    {
        return repositorioPaciente
            .SelecionarTodos()
            .Select(p => new OpcaoPacienteDto(p.Id, p.Nome))
            .ToList();
    }

    public List<ListarRequisicoesEntradaDto> SelecionarEntradas(Guid medicamentoId)
    {
        return repositorioRequisicao
            .SelecionarRequisicoesEntrada()
            .Where(r => r.Medicamento.Id == medicamentoId)
            .Select(r => new ListarRequisicoesEntradaDto(r.Id, r.DataCriacao, r.Funcionario.Nome, r.Quantidade))
            .ToList();
    }

    public List<ListarRequisicoesSaidaDto> SelecionarSaidas(Guid medicamentoId)
    {
        return repositorioRequisicao
            .SelecionarRequisicoesSaida()
            .Where(r => r.MedicamentosPrescritos.Any(m => m.Medicamento.Id == medicamentoId))
            .Select(r => new ListarRequisicoesSaidaDto(
                r.Id,
                r.DataCriacao,
                r.Paciente.Nome,
                (uint)r.MedicamentosPrescritos
                    .Where(m => m.Medicamento.Id == medicamentoId)
                    .Sum(m => (long)m.Quantidade)
            ))
            .ToList();
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}
