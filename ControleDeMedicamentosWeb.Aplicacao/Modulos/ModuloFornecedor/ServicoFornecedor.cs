using FluentResults;
using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloFornecedor;

namespace ControleDeMedicamentosWeb.Aplicacao.Modulos.ModuloFornecedor;

public class ServicoFornecedor
{
    private readonly IRepositorioFornecedor repositorioFornecedor;

    public ServicoFornecedor(IRepositorioFornecedor repositorioFornecedor)
    {
        this.repositorioFornecedor = repositorioFornecedor;
    }

    public Result Cadastrar(CadastrarFornecedorDto dto)
    {
        Fornecedor fornecedor = new(dto.Nome, dto.Telefone, dto.Cnpj);

        List<string> erros = fornecedor.Validar();
        if (erros.Count > 0)
            return Result.Fail(erros);

        repositorioFornecedor.Cadastrar(fornecedor);

        return Result.Ok();
    }

    public Result Editar(EditarFornecedorDto dto)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(dto.Id);
        if (fornecedor == null)
            return Result.Fail("Fornecedor não encontrado.");

        Fornecedor fornecedorAtualizado = new(dto.Nome, dto.Telefone, dto.Cnpj);

        List<string> erros = fornecedorAtualizado.Validar();
        if (erros.Count > 0)
            return Result.Fail(erros);

        repositorioFornecedor.Editar(dto.Id, fornecedorAtualizado);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        bool excluido = repositorioFornecedor.Excluir(id);
        if (!excluido)
            return Result.Fail("Fornecedor não encontrado.");

        return Result.Ok();
    }

    public List<ListarFornecedoresDto> SelecionarTodos()
    {
        return repositorioFornecedor.SelecionarTodos()
            .Select(f => new ListarFornecedoresDto(
                f.Id,
                f.Nome,
                f.Telefone,
                f.Cnpj
            ))
            .ToList();
    }

    public DetalhesFornecedorDto? SelecionarPorId(Guid id)
    {
        Fornecedor? f = repositorioFornecedor.SelecionarPorId(id);
        if (f == null) return null;

        return new DetalhesFornecedorDto(
            f.Id,
            f.Nome,
            f.Telefone,
            f.Cnpj
        );
    }
}