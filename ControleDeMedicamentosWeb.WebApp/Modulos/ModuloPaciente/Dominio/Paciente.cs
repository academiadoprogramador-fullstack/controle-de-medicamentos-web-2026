using System.Text.RegularExpressions;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Dominio;

public class Paciente : EntidadeBase<Paciente>
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string CartaoSus { get; set; } = string.Empty;

    public Paciente() { }

    public Paciente(string nome, string telefone, string cpf, string cartaoSus) : this()
    {
        Nome = nome;
        Telefone = telefone;
        Cpf = cpf;
        CartaoSus = cartaoSus;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        if (!Regex.IsMatch(Telefone, @"^\(\d{2}\) \d{4,5}-\d{4}$"))
            erros.Add("O campo \"Telefone\" deve estar no formato (DDD) 90000-0000.");

        if (!Regex.IsMatch(Cpf, @"^\d{11}$|^\d{3}\.\d{3}\.\d{3}-\d{2}$"))
            erros.Add("O campo \"CPF\" deve conter 11 digitos.");

        if (!Regex.IsMatch(CartaoSus, @"^\d{15}$"))
            erros.Add("O campo \"Cartao SUS\" deve conter 15 digitos.");

        return erros;
    }

    public override void Atualizar(Paciente entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Telefone = entidadeAtualizada.Telefone;
        Cpf = entidadeAtualizada.Cpf;
        CartaoSus = entidadeAtualizada.CartaoSus;
    }
}
