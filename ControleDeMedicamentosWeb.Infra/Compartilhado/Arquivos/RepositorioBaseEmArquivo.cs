using ControleDeMedicamentosWeb.Dominio.Compartilhado;

namespace ControleDeMedicamentosWeb.Infra.Compartilhado.Arquivos;

public abstract class RepositorioBaseEmArquivo<T> : IRepositorio<T> where T : EntidadeBase<T>
{
    protected readonly ContextoJson contexto;

    protected RepositorioBaseEmArquivo(ContextoJson contexto)
    {
        this.contexto = contexto;
    }

    protected abstract List<T> CarregarRegistros();

    public void Cadastrar(T entidade)
    {
        CarregarRegistros().Add(entidade);
        contexto.Salvar();
    }

    public bool Editar(Guid idSelecionado, T entidadeAtualizada)
    {
        T? entidade = SelecionarPorId(idSelecionado);
        if (entidade == null)
            return false;

        entidade.Atualizar(entidadeAtualizada);
        contexto.Salvar();

        return true;
    }

    public bool Excluir(Guid idSelecionado)
    {
        T? entidade = SelecionarPorId(idSelecionado);
        if (entidade == null)
            return false;

        CarregarRegistros().Remove(entidade);
        contexto.Salvar();

        return true;
    }

    public T? SelecionarPorId(Guid idSelecionado)
    {
        return CarregarRegistros().FirstOrDefault(r => r.Id == idSelecionado);
    }

    public List<T> SelecionarTodos()
    {
        return CarregarRegistros().ToList();
    }

    public List<T> Filtrar(Predicate<T> filtro)
    {
        return CarregarRegistros().FindAll(filtro);
    }
}