using ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra.Arquivos;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Infra;

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
