using System.Text.Json;
using System.Text.Json.Serialization;
using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloEstoque;
using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloFornecedor;
using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloFuncionario;
using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloMedicamento;
using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloPaciente;

namespace ControleDeMedicamentosWeb.Infra.Compartilhado.Arquivos;

public sealed class ContextoJson
{
    private readonly string caminhoArquivo;

    public List<Fornecedor> Fornecedores { get; set; } = new List<Fornecedor>();
    public List<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
    public List<Medicamento> Medicamentos { get; set; } = new List<Medicamento>();
    public List<Paciente> Pacientes { get; set; } = new List<Paciente>();
    public List<RequisicaoBase> Requisicoes { get; set; } = new List<RequisicaoBase>();

    public ContextoJson()
    {
        string caminhoAppData = Environment
            .GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        string caminhoDiretorio = Path.Combine(caminhoAppData, "ControleDeMedicamentosWeb");

        Directory.CreateDirectory(caminhoDiretorio);

        caminhoArquivo = Path.Combine(caminhoDiretorio, "dados.json");

        Carregar();
    }

    public void Salvar()
    {
        JsonSerializerOptions options = new()
        {
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            Converters = { new JsonStringEnumConverter() }
        };

        string json = JsonSerializer.Serialize(this, options);
        File.WriteAllText(caminhoArquivo, json);
    }

    private void Carregar()
    {
        if (!File.Exists(caminhoArquivo))
            return;

        string json = File.ReadAllText(caminhoArquivo);

        JsonSerializerOptions options = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            Converters = { new JsonStringEnumConverter() }
        };

        ContextoJson? carregado = JsonSerializer.Deserialize<ContextoJson>(json, options);

        if (carregado == null)
            return;

        Fornecedores = carregado.Fornecedores;
        Funcionarios = carregado.Funcionarios;
        Medicamentos = carregado.Medicamentos;
        Pacientes = carregado.Pacientes;
        Requisicoes = carregado.Requisicoes;
    }
}