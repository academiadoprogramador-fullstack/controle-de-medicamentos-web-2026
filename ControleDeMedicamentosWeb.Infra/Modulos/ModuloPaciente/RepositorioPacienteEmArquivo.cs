using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloPaciente;
using ControleDeMedicamentosWeb.Infra.Compartilhado.Arquivos;

namespace ControleDeMedicamentosWeb.Infra.Modulos.ModuloPaciente;

public class RepositorioPacienteEmArquivo :
    RepositorioBaseEmArquivo<Paciente>, IRepositorioPaciente
{
    public RepositorioPacienteEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Paciente> CarregarRegistros()
    {
        return contexto.Pacientes;
    }
}