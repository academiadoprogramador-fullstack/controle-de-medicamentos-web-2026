using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloPaciente;
using ControleDeMedicamentosWeb.Infra.Compartilhado.Sql;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ControleDeMedicamentosWeb.Infra.Modulos.ModuloPaciente;

public sealed class RepositorioPacienteEmSql(ISqlConnectionFactory connectionFactory)
    : IRepositorioPaciente
{
    private const string InserirSql = """
        INSERT INTO dbo.TBPaciente (Id, Nome, Telefone, Cpf)
        VALUES (@Id, @Nome, @Telefone, @Cpf)
        """;

    private const string EditarSql = """
        UPDATE dbo.TBPaciente
        SET Nome = @Nome, Telefone = @Telefone, Cpf = @Cpf
        WHERE Id = @Id
        """;

    private const string ExcluirSql = """
        DELETE FROM dbo.TBPaciente WHERE Id = @Id
        """;

    private const string SelecionarPorIdSql = """
        SELECT Id, Nome, Telefone, Cpf
        FROM dbo.TBPaciente
        WHERE Id = @Id
        """;

    private const string SelecionarTodosSql = """
        SELECT Id, Nome, Telefone, Cpf
        FROM dbo.TBPaciente
        """;

    public void Cadastrar(Paciente paciente)
    {
        using SqlConnection connection = connectionFactory.CreateConnection();
        connection.Execute(InserirSql, paciente);
    }

    public bool Editar(Guid idSelecionado, Paciente pacienteAtualizado)
    {
        using SqlConnection connection = connectionFactory.CreateConnection();
        int linhasAfetadas = connection.Execute(EditarSql, new
        {
            pacienteAtualizado.Nome,
            pacienteAtualizado.Telefone,
            pacienteAtualizado.Cpf,
            Id = idSelecionado
        });

        return linhasAfetadas > 0;
    }

    public bool Excluir(Guid idSelecionado)
    {
        using SqlConnection connection = connectionFactory.CreateConnection();
        int linhasAfetadas = connection.Execute(ExcluirSql, new { Id = idSelecionado });

        return linhasAfetadas > 0;
    }

    public Paciente? SelecionarPorId(Guid idSelecionado)
    {
        using SqlConnection connection = connectionFactory.CreateConnection();
        return connection.QueryFirstOrDefault<Paciente>(SelecionarPorIdSql, new { Id = idSelecionado });
    }

    public List<Paciente> SelecionarTodos()
    {
        using SqlConnection connection = connectionFactory.CreateConnection();
        return connection.Query<Paciente>(SelecionarTodosSql).ToList();
    }

    public List<Paciente> Filtrar(Predicate<Paciente> filtro)
    {
        using SqlConnection connection = connectionFactory.CreateConnection();
        return connection.Query<Paciente>(SelecionarTodosSql).Where(p => filtro(p)).ToList();
    }
}