using System.Text.RegularExpressions;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Dominio;

public class Fornecedor : EntidadeBase<Fornecedor>
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;

    public Fornecedor() { }

    public Fornecedor(string nome, string telefone, string cnpj) : this()
    {
        Nome = nome;
        Telefone = telefone;
        Cnpj = cnpj;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        if (!Regex.IsMatch(Telefone, @"^\(\d{2}\) \d{4,5}-\d{4}$"))
            erros.Add("O campo \"Telefone\" deve estar no formato (DDD) 90000-0000.");

        if (!Regex.IsMatch(Cnpj, @"^\d{14}$|^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$"))
            erros.Add("O campo \"CNPJ\" deve conter 14 digitos.");

        return erros;
    }

    public override void Atualizar(Fornecedor entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Telefone = entidadeAtualizada.Telefone;
        Cnpj = entidadeAtualizada.Cnpj;
    }
}
