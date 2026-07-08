using System.Text.Json.Serialization;

namespace ControleDeMedicamentosWeb.Dominio.Modulos.ModuloEstoque;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$tipo")]
[JsonDerivedType(typeof(RequisicaoEntrada), "entrada")]
[JsonDerivedType(typeof(RequisicaoSaida), "saida")]
public abstract class RequisicaoBase
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public DateTime DataCriacao { get; set; } = DateTime.Now;
}