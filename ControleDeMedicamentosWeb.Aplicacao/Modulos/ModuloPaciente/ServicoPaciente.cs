using FluentResults;
using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloPaciente;

namespace ControleDeMedicamentosWeb.Aplicacao.Modulos.ModuloPaciente;

public class ServicoPaciente
{
    private readonly IRepositorioPaciente repositorioPaciente;

    public ServicoPaciente(IRepositorioPaciente repositorioPaciente)
    {
        this.repositorioPaciente = repositorioPaciente;
    }

    public Result Cadastrar(CadastrarPacienteDto dto)
    {
        Paciente paciente = new(dto.Nome, dto.Telefone, dto.Cpf, dto.CartaoSus);

        List<string> erros = paciente.Validar();
        if (erros.Count > 0)
            return Result.Fail(erros);

        repositorioPaciente.Cadastrar(paciente);

        return Result.Ok();
    }

    public Result Editar(EditarPacienteDto dto)
    {
        Paciente? paciente = repositorioPaciente.SelecionarPorId(dto.Id);
        if (paciente == null)
            return Result.Fail("Paciente não encontrado.");

        Paciente pacienteAtualizado = new(dto.Nome, dto.Telefone, dto.Cpf, dto.CartaoSus);

        List<string> erros = pacienteAtualizado.Validar();
        if (erros.Count > 0)
            return Result.Fail(erros);

        repositorioPaciente.Editar(dto.Id, pacienteAtualizado);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        bool excluido = repositorioPaciente.Excluir(id);
        if (!excluido)
            return Result.Fail("Paciente não encontrado.");

        return Result.Ok();
    }

    public List<ListarPacientesDto> SelecionarTodos()
    {
        return repositorioPaciente.SelecionarTodos()
            .Select(p => new ListarPacientesDto(
                p.Id,
                p.Nome,
                p.Telefone,
                p.Cpf,
                p.CartaoSus
            ))
            .ToList();
    }

    public DetalhesPacienteDto? SelecionarPorId(Guid id)
    {
        Paciente? p = repositorioPaciente.SelecionarPorId(id);
        if (p == null) return null;

        return new DetalhesPacienteDto(
            p.Id,
            p.Nome,
            p.Telefone,
            p.Cpf,
            p.CartaoSus
        );
    }
}