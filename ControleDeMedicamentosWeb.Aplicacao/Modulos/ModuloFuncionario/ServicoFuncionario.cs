using FluentResults;
using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloFuncionario;

namespace ControleDeMedicamentosWeb.Aplicacao.Modulos.ModuloFuncionario;

public class ServicoFuncionario
{
    private readonly IRepositorioFuncionario repositorioFuncionario;

    public ServicoFuncionario(IRepositorioFuncionario repositorioFuncionario)
    {
        this.repositorioFuncionario = repositorioFuncionario;
    }

    public Result Cadastrar(CadastrarFuncionarioDto dto)
    {
        Funcionario funcionario = new(dto.Nome, dto.Telefone, dto.Cpf);

        List<string> erros = funcionario.Validar();
        if (erros.Count > 0)
            return Result.Fail(erros);

        repositorioFuncionario.Cadastrar(funcionario);

        return Result.Ok();
    }

    public Result Editar(EditarFuncionarioDto dto)
    {
        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(dto.Id);
        if (funcionario == null)
            return Result.Fail("Funcionário não encontrado.");

        Funcionario funcionarioAtualizado = new(dto.Nome, dto.Telefone, dto.Cpf);

        List<string> erros = funcionarioAtualizado.Validar();
        if (erros.Count > 0)
            return Result.Fail(erros);

        repositorioFuncionario.Editar(dto.Id, funcionarioAtualizado);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        bool excluido = repositorioFuncionario.Excluir(id);
        if (!excluido)
            return Result.Fail("Funcionário não encontrado.");

        return Result.Ok();
    }

    public List<ListarFuncionariosDto> SelecionarTodos()
    {
        return repositorioFuncionario.SelecionarTodos()
            .Select(f => new ListarFuncionariosDto(
                f.Id,
                f.Nome,
                f.Telefone,
                f.Cpf
            ))
            .ToList();
    }

    public DetalhesFuncionarioDto? SelecionarPorId(Guid id)
    {
        Funcionario? f = repositorioFuncionario.SelecionarPorId(id);
        if (f == null) return null;

        return new DetalhesFuncionarioDto(
            f.Id,
            f.Nome,
            f.Telefone,
            f.Cpf
        );
    }
}