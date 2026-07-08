using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloFuncionario;
using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloMedicamento;

namespace ControleDeMedicamentosWeb.Dominio.Modulos.ModuloEstoque;

public class RequisicaoEntrada : RequisicaoBase
{
    public Funcionario Funcionario { get; set; } = null!;
    public Medicamento Medicamento { get; set; } = null!;
    public uint Quantidade { get; set; } = 0;

    public RequisicaoEntrada()
    {
    }

    public RequisicaoEntrada(
        Funcionario funcionario,
        Medicamento medicamento,
        uint quantidade
    ) : this()
    {
        Funcionario = funcionario;
        Medicamento = medicamento;
        Quantidade = quantidade;

        Medicamento.RegistrarRequisicao(this);
    }
}