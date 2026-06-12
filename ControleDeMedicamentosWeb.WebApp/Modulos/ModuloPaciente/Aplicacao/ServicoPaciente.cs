using FluentResults;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Aplicacao;

public class ServicoPaciente
{
    private readonly IRepositorioPaciente repositorioPaciente;

    public ServicoPaciente(IRepositorioPaciente repositorioPaciente)
    {
        this.repositorioPaciente = repositorioPaciente;
    }

    public Result Cadastrar(CadastrarPacienteDto dto)
    {
        if (ExistePacienteComCartaoSus(dto.CartaoSus))
            return Falha(nameof(dto.CartaoSus), "Ja existe um paciente com este cartao SUS.");

        Paciente novoPaciente = new Paciente(dto.Nome, dto.Telefone, dto.Cpf, dto.CartaoSus);

        Result resultadoValidacao = ValidarEntidade(novoPaciente);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioPaciente.Cadastrar(novoPaciente);

        return Result.Ok();
    }

    public Result Editar(EditarPacienteDto dto)
    {
        if (ExistePacienteComCartaoSus(dto.CartaoSus, dto.Id))
            return Falha(nameof(dto.CartaoSus), "Ja existe um paciente com este cartao SUS.");

        Paciente pacienteAtualizado = new Paciente(dto.Nome, dto.Telefone, dto.Cpf, dto.CartaoSus);

        Result resultadoValidacao = ValidarEntidade(pacienteAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioPaciente.Editar(dto.Id, pacienteAtualizado);

        if (!conseguiuEditar)
            return Result.Fail("Paciente nao encontrado.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Paciente? paciente = repositorioPaciente.SelecionarPorId(id);

        if (paciente == null)
            return Result.Fail("Paciente nao encontrado.");

        repositorioPaciente.Excluir(id);

        return Result.Ok();
    }

    public List<ListarPacientesDto> SelecionarTodos()
    {
        return repositorioPaciente
            .SelecionarTodos()
            .Select(p => new ListarPacientesDto(p.Id, p.Nome, p.Telefone, p.Cpf, p.CartaoSus))
            .ToList();
    }

    public Result<DetalhesPacienteDto> SelecionarPorId(Guid id)
    {
        Paciente? paciente = repositorioPaciente.SelecionarPorId(id);

        if (paciente == null)
            return Result.Fail("Paciente nao encontrado.");

        return Result.Ok(new DetalhesPacienteDto(
            paciente.Id,
            paciente.Nome,
            paciente.Telefone,
            paciente.Cpf,
            paciente.CartaoSus
        ));
    }

    private bool ExistePacienteComCartaoSus(string cartaoSus, Guid? idIgnorado = null)
    {
        string cartaoSusNormalizado = NormalizarDigitos(cartaoSus);

        return repositorioPaciente
            .SelecionarTodos()
            .Any(p =>
                p.Id != idIgnorado &&
                NormalizarDigitos(p.CartaoSus) == cartaoSusNormalizado
            );
    }

    private static string NormalizarDigitos(string valor)
    {
        return new string(valor.Where(char.IsDigit).ToArray());
    }

    private static Result ValidarEntidade(Paciente paciente)
    {
        List<string> erros = paciente.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}
