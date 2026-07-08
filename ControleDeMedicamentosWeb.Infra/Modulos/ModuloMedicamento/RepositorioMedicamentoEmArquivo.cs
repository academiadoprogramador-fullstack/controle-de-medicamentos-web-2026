using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloMedicamento;
using ControleDeMedicamentosWeb.Infra.Compartilhado.Arquivos;

namespace ControleDeMedicamentosWeb.Infra.Modulos.ModuloMedicamento;

public class RepositorioMedicamentoEmArquivo :
    RepositorioBaseEmArquivo<Medicamento>, IRepositorioMedicamento
{
    public RepositorioMedicamentoEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Medicamento> CarregarRegistros()
    {
        return contexto.Medicamentos;
    }
}