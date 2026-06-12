using FluentResults;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Aplicacao;

public class ServicoFuncionario
{
    private readonly IRepositorioFuncionario repositorioFuncionario;

    public ServicoFuncionario(IRepositorioFuncionario repositorioFuncionario)
    {
        this.repositorioFuncionario = repositorioFuncionario;
    }

    public Result Cadastrar(CadastrarFuncionarioDto dto)
    {
        if (ExisteFuncionarioComCpf(dto.Cpf))
            return Falha(nameof(dto.Cpf), "Ja existe um funcionario com este CPF.");

        Funcionario novoFuncionario = new Funcionario(dto.Nome, dto.Telefone, dto.Cpf);

        Result resultadoValidacao = ValidarEntidade(novoFuncionario);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioFuncionario.Cadastrar(novoFuncionario);

        return Result.Ok();
    }

    public Result Editar(EditarFuncionarioDto dto)
    {
        if (ExisteFuncionarioComCpf(dto.Cpf, dto.Id))
            return Falha(nameof(dto.Cpf), "Ja existe um funcionario com este CPF.");

        Funcionario funcionarioAtualizado = new Funcionario(dto.Nome, dto.Telefone, dto.Cpf);

        Result resultadoValidacao = ValidarEntidade(funcionarioAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioFuncionario.Editar(dto.Id, funcionarioAtualizado);

        if (!conseguiuEditar)
            return Result.Fail("Funcionario nao encontrado.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(id);

        if (funcionario == null)
            return Result.Fail("Funcionario nao encontrado.");

        repositorioFuncionario.Excluir(id);

        return Result.Ok();
    }

    public List<ListarFuncionariosDto> SelecionarTodos()
    {
        return repositorioFuncionario
            .SelecionarTodos()
            .Select(f => new ListarFuncionariosDto(f.Id, f.Nome, f.Telefone, f.Cpf))
            .ToList();
    }

    public Result<DetalhesFuncionarioDto> SelecionarPorId(Guid id)
    {
        Funcionario? funcionario = repositorioFuncionario.SelecionarPorId(id);

        if (funcionario == null)
            return Result.Fail("Funcionario nao encontrado.");

        return Result.Ok(new DetalhesFuncionarioDto(
            funcionario.Id,
            funcionario.Nome,
            funcionario.Telefone,
            funcionario.Cpf
        ));
    }

    private bool ExisteFuncionarioComCpf(string cpf, Guid? idIgnorado = null)
    {
        string cpfNormalizado = NormalizarDigitos(cpf);

        return repositorioFuncionario
            .SelecionarTodos()
            .Any(f =>
                f.Id != idIgnorado &&
                NormalizarDigitos(f.Cpf) == cpfNormalizado
            );
    }

    private static string NormalizarDigitos(string valor)
    {
        return new string(valor.Where(char.IsDigit).ToArray());
    }

    private static Result ValidarEntidade(Funcionario funcionario)
    {
        List<string> erros = funcionario.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}
