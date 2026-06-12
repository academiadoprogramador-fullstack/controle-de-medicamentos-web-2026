using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Dominio;

public class RequisicaoSaida : RequisicaoBase
{
    public Paciente Paciente { get; set; } = null!;
    public List<MedicamentoPrescrito> MedicamentosPrescritos { get; set; } = new List<MedicamentoPrescrito>();

    public RequisicaoSaida()
    {
    }

    public RequisicaoSaida(
        Paciente paciente,
        List<MedicamentoPrescrito> medicamentosPrescritos
    ) : this()
    {
        Paciente = paciente;
        MedicamentosPrescritos = medicamentosPrescritos;

        foreach (MedicamentoPrescrito medPresc in MedicamentosPrescritos)
            medPresc.Medicamento.RegistrarRequisicao(this);
    }
}
