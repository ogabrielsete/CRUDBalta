using TesteBalta.Models;

namespace TesteBalta.Repositorio
{
    public interface IAssinaturasRepositorio
    {
        Assinatura GetById(int id);
        List<Assinatura> GetAll();
        Assinatura Adicionar(Assinatura assinatura);
        Assinatura Editar(Assinatura assinatura);
        public bool Deletar(int id);

        List<Assinatura> GetAllByAlunoId(int id);
    }
}
