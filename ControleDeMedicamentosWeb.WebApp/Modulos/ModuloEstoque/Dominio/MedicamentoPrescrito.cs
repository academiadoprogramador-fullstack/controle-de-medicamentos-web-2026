using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Dominio;

public class MedicamentoPrescrito
{
    public Medicamento Medicamento { get; set; } = null!;
    public uint Quantidade { get; set; } = 0;

    public MedicamentoPrescrito()
    {
    }

    public MedicamentoPrescrito(Medicamento medicamento, uint quantidade) : this()
    {
        Medicamento = medicamento;
        Quantidade = quantidade;
    }
}
