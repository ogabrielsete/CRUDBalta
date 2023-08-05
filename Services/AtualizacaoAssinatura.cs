using Microsoft.EntityFrameworkCore;
using TesteBalta.Data;
using TesteBalta.Repositorio;
using static System.Formats.Asn1.AsnWriter;

namespace TesteBalta.Services
{
    public class AtualizacaoAssinatura : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public AtualizacaoAssinatura(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    // Este código recupera uma lista de alunos ativos da tabela `AssinaturaDB`.
                    var alunosAtivo = context.AssinaturaDB.ToList();
                    foreach (var item in alunosAtivo)
                    {
                        // Para cada aluno ativo, o código recupera todas as assinaturas do aluno.
                        var assinaturas = context.AssinaturaDB.Where(x => x.Id == item.Id);
                        foreach (var ass in assinaturas)
                        {
                            if (ass.Inicio < DateTime.Now && ass.Termino > DateTime.Now)
                            {
                                ass.Ativo = true;
                            }
                            else
                            {
                                ass.Ativo = false;
                            }
                            context.AssinaturaDB.Update(ass);
                            context.SaveChanges();
                        }
                    }
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken); // Aguarde uma hora antes de executar novamente
            }
        }
    }
}
