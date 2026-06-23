using FluentResults;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Aplicacao;

public class ServicoFornecedor
{
    private readonly IRepositorioFornecedor repositorioFornecedor;
    private readonly ILogger<ServicoFornecedor> logger;

    public ServicoFornecedor(
        IRepositorioFornecedor repositorioFornecedor,
        ILogger<ServicoFornecedor> logger
    )
    {
        this.repositorioFornecedor = repositorioFornecedor;
        this.logger = logger;
    }

    public Result Cadastrar(CadastrarFornecedorDto dto)
    {
        if (ExisteFornecedorComCnpj(dto.Cnpj))
        {
            logger.LogWarning("Cadastro de fornecedor recusado por CNPJ duplicado.");

            return Falha(nameof(dto.Cnpj), "Ja existe um fornecedor com este CNPJ.");
        }

        Fornecedor novoFornecedor = new Fornecedor(dto.Nome, dto.Telefone, dto.Cnpj);

        Result resultadoValidacao = ValidarEntidade(novoFornecedor);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioFornecedor.Cadastrar(novoFornecedor);

        logger.LogInformation(
            "Fornecedor cadastrado. FornecedorId: {FornecedorId}",
            novoFornecedor.Id
        );

        return Result.Ok();
    }

    public Result Editar(EditarFornecedorDto dto)
    {
        if (ExisteFornecedorComCnpj(dto.Cnpj, dto.Id))
        {
            logger.LogWarning(
                "Edição de fornecedor recusada por CNPJ duplicado. FornecedorId: {FornecedorId}",
                dto.Id
            );

            return Falha(nameof(dto.Cnpj), "Ja existe um fornecedor com este CNPJ.");
        }

        Fornecedor fornecedorAtualizado = new Fornecedor(dto.Nome, dto.Telefone, dto.Cnpj);

        Result resultadoValidacao = ValidarEntidade(fornecedorAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioFornecedor.Editar(dto.Id, fornecedorAtualizado);

        if (!conseguiuEditar)
        {
            logger.LogWarning(
                "Edição de fornecedor recusada porque o registro não foi encontrado. FornecedorId: {FornecedorId}",
                dto.Id
            );

            return Result.Fail("Fornecedor nao encontrado.");
        }

        logger.LogInformation(
            "Fornecedor editado. FornecedorId: {FornecedorId}",
            dto.Id
        );

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(id);

        if (fornecedor == null)
        {
            logger.LogWarning(
                "Exclusão de fornecedor recusada porque o registro não foi encontrado. FornecedorId: {FornecedorId}",
                id
            );

            return Result.Fail("Fornecedor nao encontrado.");
        }

        repositorioFornecedor.Excluir(id);

        logger.LogInformation("Fornecedor excluído. FornecedorId: {FornecedorId}", id);

        return Result.Ok();
    }

    public List<ListarFornecedoresDto> SelecionarTodos()
    {
        return repositorioFornecedor
            .SelecionarTodos()
            .Select(f => new ListarFornecedoresDto(f.Id, f.Nome, f.Telefone, f.Cnpj))
            .ToList();
    }

    public Result<DetalhesFornecedorDto> SelecionarPorId(Guid id)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(id);

        if (fornecedor == null)
        {
            logger.LogDebug(
                "Fornecedor não encontrado durante consulta. FornecedorId: {FornecedorId}",
                id
            );

            return Result.Fail("Fornecedor nao encontrado.");
        }

        return Result.Ok(new DetalhesFornecedorDto(
            fornecedor.Id,
            fornecedor.Nome,
            fornecedor.Telefone,
            fornecedor.Cnpj
        ));
    }

    private bool ExisteFornecedorComCnpj(string cnpj, Guid? idIgnorado = null)
    {
        string cnpjNormalizado = NormalizarCnpj(cnpj);

        return repositorioFornecedor
            .SelecionarTodos()
            .Any(f =>
                f.Id != idIgnorado &&
                NormalizarCnpj(f.Cnpj) == cnpjNormalizado
            );
    }

    private static string NormalizarCnpj(string cnpj)
    {
        return new string(cnpj.Where(char.IsDigit).ToArray());
    }

    private static Result ValidarEntidade(Fornecedor fornecedor)
    {
        List<string> erros = fornecedor.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}
