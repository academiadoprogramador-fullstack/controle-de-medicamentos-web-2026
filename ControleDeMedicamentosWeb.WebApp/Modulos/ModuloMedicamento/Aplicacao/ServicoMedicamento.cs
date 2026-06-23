using FluentResults;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Aplicacao;

public class ServicoMedicamento
{
    private readonly IRepositorioMedicamento repositorioMedicamento;
    private readonly IRepositorioFornecedor repositorioFornecedor;
    private readonly ILogger<ServicoMedicamento> logger;

    public ServicoMedicamento(
        IRepositorioMedicamento repositorioMedicamento,
        IRepositorioFornecedor repositorioFornecedor,
        ILogger<ServicoMedicamento> logger
    )
    {
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFornecedor = repositorioFornecedor;
        this.logger = logger;
    }

    public Result Cadastrar(CadastrarMedicamentoDto dto)
    {
        Fornecedor? fornecedorSelecionado = repositorioFornecedor.SelecionarPorId(dto.FornecedorId);

        if (fornecedorSelecionado == null)
        {
            logger.LogWarning(
                "Cadastro de medicamento recusado porque o fornecedor não foi encontrado. FornecedorId: {FornecedorId}",
                dto.FornecedorId
            );

            return Falha(nameof(dto.FornecedorId), "Selecione um fornecedor valido.");
        }

        if (ExisteMedicamentoComMesmoNomeNoFornecedor(dto.Nome, dto.FornecedorId))
        {
            logger.LogWarning(
                "Cadastro de medicamento recusado por nome duplicado no fornecedor. FornecedorId: {FornecedorId}",
                dto.FornecedorId
            );

            return Falha(nameof(dto.Nome), "Ja existe um medicamento com este nome neste fornecedor.");
        }

        Medicamento novoMedicamento = new Medicamento(
            dto.Nome,
            dto.Descricao,
            fornecedorSelecionado
        );

        Result resultadoValidacao = ValidarEntidade(novoMedicamento);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioMedicamento.Cadastrar(novoMedicamento);

        logger.LogInformation(
            "Medicamento cadastrado. MedicamentoId: {MedicamentoId}, FornecedorId: {FornecedorId}",
            novoMedicamento.Id,
            fornecedorSelecionado.Id
        );

        return Result.Ok();
    }

    public Result Editar(EditarMedicamentoDto dto)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(dto.Id);

        if (medicamento == null)
        {
            logger.LogWarning(
                "Edição de medicamento recusada porque o registro não foi encontrado. MedicamentoId: {MedicamentoId}",
                dto.Id
            );

            return Result.Fail("Medicamento nao encontrado.");
        }

        Fornecedor? fornecedorSelecionado = repositorioFornecedor.SelecionarPorId(dto.FornecedorId);

        if (fornecedorSelecionado == null)
        {
            logger.LogWarning(
                "Edição de medicamento recusada porque o fornecedor não foi encontrado. MedicamentoId: {MedicamentoId}, FornecedorId: {FornecedorId}",
                dto.Id,
                dto.FornecedorId
            );

            return Falha(nameof(dto.FornecedorId), "Selecione um fornecedor valido.");
        }

        if (ExisteMedicamentoComMesmoNomeNoFornecedor(dto.Nome, dto.FornecedorId, dto.Id))
        {
            logger.LogWarning(
                "Edição de medicamento recusada por nome duplicado no fornecedor. MedicamentoId: {MedicamentoId}, FornecedorId: {FornecedorId}",
                dto.Id,
                dto.FornecedorId
            );

            return Falha(nameof(dto.Nome), "Ja existe um medicamento com este nome neste fornecedor.");
        }

        Medicamento medicamentoAtualizado = new Medicamento(
            dto.Nome,
            dto.Descricao,
            fornecedorSelecionado
        );

        Result resultadoValidacao = ValidarEntidade(medicamentoAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioMedicamento.Editar(dto.Id, medicamentoAtualizado);

        if (!conseguiuEditar)
        {
            logger.LogWarning(
                "Edição de medicamento não persistida. MedicamentoId: {MedicamentoId}",
                dto.Id
            );

            return Result.Fail("Medicamento nao encontrado.");
        }

        logger.LogInformation(
            "Medicamento editado. MedicamentoId: {MedicamentoId}, FornecedorId: {FornecedorId}",
            dto.Id,
            fornecedorSelecionado.Id
        );

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(id);

        if (medicamento == null)
        {
            logger.LogWarning(
                "Exclusão de medicamento recusada porque o registro não foi encontrado. MedicamentoId: {MedicamentoId}",
                id
            );

            return Result.Fail("Medicamento nao encontrado.");
        }

        bool conseguiuExcluir = repositorioMedicamento.Excluir(id);

        if (!conseguiuExcluir)
        {
            logger.LogWarning(
                "Exclusão de medicamento não persistida. MedicamentoId: {MedicamentoId}",
                id
            );

            return Result.Fail("Medicamento nao encontrado.");
        }

        logger.LogInformation("Medicamento excluído. MedicamentoId: {MedicamentoId}", id);

        return Result.Ok();
    }

    public List<ListarMedicamentosDto> SelecionarTodos()
    {
        return repositorioMedicamento
            .SelecionarTodos()
            .Select(m => new ListarMedicamentosDto(
                m.Id,
                m.Nome,
                m.Descricao,
                m.QuantidadeEmEstoque,
                m.Fornecedor.Id,
                m.Fornecedor.Nome
            ))
            .ToList();
    }

    public Result<DetalhesMedicamentoDto> SelecionarPorId(Guid id)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(id);

        if (medicamento == null)
        {
            logger.LogDebug(
                "Medicamento não encontrado durante consulta. MedicamentoId: {MedicamentoId}",
                id
            );

            return Result.Fail("Medicamento nao encontrado.");
        }

        return Result.Ok(new DetalhesMedicamentoDto(
            medicamento.Id,
            medicamento.Nome,
            medicamento.Descricao,
            medicamento.QuantidadeEmEstoque,
            medicamento.Fornecedor.Id,
            medicamento.Fornecedor.Nome
        ));
    }

    public List<OpcaoFornecedorDto> SelecionarFornecedores()
    {
        return repositorioFornecedor
            .SelecionarTodos()
            .Select(f => new OpcaoFornecedorDto(f.Id, f.Nome))
            .ToList();
    }

    private bool ExisteMedicamentoComMesmoNomeNoFornecedor(string nome, Guid fornecedorId, Guid? idIgnorado = null)
    {
        return repositorioMedicamento
            .SelecionarTodos()
            .Any(m =>
                m.Id != idIgnorado &&
                m.Fornecedor.Id == fornecedorId &&
                string.Equals(m.Nome, nome, StringComparison.OrdinalIgnoreCase)
            );
    }

    private static Result ValidarEntidade(Medicamento medicamento)
    {
        List<string> erros = medicamento.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}
