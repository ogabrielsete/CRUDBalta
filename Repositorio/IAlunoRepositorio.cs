using TesteBalta.Models;

namespace TesteBalta.Repositorio
{
    public interface IAlunoRepositorio
    {
        Aluno MostrarPorId(int id);
        List<Aluno> MostrarAlunos();
        Aluno Adicionar(Aluno aluno);
        Aluno Editar (Aluno aluno);
        bool Apagar(int id);
    }
}
