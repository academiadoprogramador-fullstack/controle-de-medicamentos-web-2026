using System.Text.Json.Serialization;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Dominio;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$tipo")]
[JsonDerivedType(typeof(RequisicaoEntrada), "entrada")]
[JsonDerivedType(typeof(RequisicaoSaida), "saida")]
public abstract class RequisicaoBase
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public DateTime DataCriacao { get; set; } = DateTime.Now;
}
