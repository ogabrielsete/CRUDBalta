using TesteBalta.Data;
using TesteBalta.Models;

namespace TesteBalta.Repositorio
{
    public class AssinaturaRepositorio : IAssinaturasRepositorio
    {
        private readonly AppDbContext _context;
        public AssinaturaRepositorio(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public Assinatura GetById(int id)
        {
            return _context.AssinaturaDB.FirstOrDefault(x => x.Id == id);
        }

        public List<Assinatura> GetAllByAlunoId(int id)
        {
            return _context.AssinaturaDB.Where(x => x.AlunoId == id).ToList();
        }

        public List<Assinatura> GetAll()
        {
            return _context.AssinaturaDB.ToList();
        }


        public Assinatura Adicionar(Assinatura assinatura)
        {
            _context.AssinaturaDB.Add(assinatura);
            _context.SaveChanges();
            return assinatura;
        }

        public Assinatura Editar(Assinatura assinatura)
        {
            Assinatura assDB = GetById(assinatura.Id);
            if (assDB == null) throw new Exception("Houve um erro na atualização da assinatura");
            assDB.DataCriacao = assinatura.DataCriacao;          
            assDB.AlunoId = assinatura.AlunoId;
            assDB.Inicio   = assinatura.Inicio;
            assDB.Termino = assinatura.Termino;

            _context.AssinaturaDB.Update(assDB);
            _context.SaveChanges();

            return assDB;
        }

        public bool Deletar(int id)
        {
            Assinatura assDB = GetById(id);
            if (assDB == null) throw new Exception("Houve um erro na exclusão da assinatura");

            _context.AssinaturaDB.Remove(assDB);
            _context.SaveChanges();
            return true;
        }
    }
}
