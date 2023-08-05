using TesteBalta.Data;
using TesteBalta.Models;

namespace TesteBalta.Repositorio
{
    public class AlunoRepositorio : IAlunoRepositorio
    {
        private readonly AppDbContext _context;
        public AlunoRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public Aluno MostrarPorId(int id)
        {
            return _context.AlunoDB.FirstOrDefault(x => x.Id == id);
        }

        public List<Aluno> MostrarAlunos()
        {
            return _context.AlunoDB.ToList();
        }

        public Aluno Adicionar(Aluno aluno)
        {
            _context.AlunoDB.Add(aluno);
            _context.SaveChanges();
            return aluno;
        }

        public Aluno Editar(Aluno aluno)
        {
            Aluno alunoDB = MostrarPorId(aluno.Id);

            if (alunoDB == null) throw new Exception("Houve erro na atualização do Aluno");

            alunoDB.Nome = aluno.Nome;
            alunoDB.Sobrenome = aluno.Sobrenome;
            alunoDB.CPF = aluno.CPF;
            alunoDB.DataNascimento = aluno.DataNascimento;
            alunoDB.Telefone = aluno.Telefone;
            alunoDB.ImagemAluno = aluno.ImagemAluno;

            //if (!string.IsNullOrEmpty(aluno.ImagemAluno))
            //{
            //    alunoDB.ImagemAluno = aluno.ImagemAluno;
            //}

            _context.AlunoDB.Update(alunoDB);
            _context.SaveChanges();

            return alunoDB;
        }
        public bool Apagar(int id)
        {
            Aluno alunoDB = MostrarPorId(id);
            if (alunoDB == null) throw new Exception("Houve erro na exclusão do aluno");

            _context.AlunoDB.Remove(alunoDB);
            _context.SaveChanges();
            return true;

        }

    }
}
