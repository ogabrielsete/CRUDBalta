using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TesteBalta.Mappings;
using TesteBalta.Models;
using TesteBalta.Models.ViewModels;
using TesteBalta.Repositorio;

namespace TesteBalta.Controllers
{
    public class AlunosController : Controller
    {
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly IAssinaturasRepositorio _assinaturasRepositorio;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        public AlunosController(IAlunoRepositorio alunoRepositorio, IAssinaturasRepositorio assinaturasRepositorio,
                                IWebHostEnvironment webHostEnvironment,
                                IMapper mapper)
        {
            
            _alunoRepositorio = alunoRepositorio;
            _assinaturasRepositorio = assinaturasRepositorio;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            List<Aluno> aluno = _alunoRepositorio.MostrarAlunos();
            return View(aluno);
        }
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            Aluno aluno = _alunoRepositorio.MostrarPorId(id);

            var alunoVM = _mapper.Map<Aluno, AlunoViewModel>(aluno);
            return View(alunoVM);
        }
        public IActionResult Deletar(int id)
        {
            try
            {
                Aluno aluno = _alunoRepositorio.MostrarPorId(id);            
                
                _alunoRepositorio.Apagar(id);
                return RedirectToAction("Index", TempData["MensagemSucesso"] = "Aluno excluído com sucesso!");
            }
            catch (Exception error)
            {

                return RedirectToAction("Index", TempData["MensagemErro"] = $"Ocorreu um erro ao tentar excluir o aluno. Detalhe: {error.Message}");
            }
        }

        public IActionResult Detalhes(int id)
        {
            var detalhesAluno = _alunoRepositorio.MostrarPorId(id);

            detalhesAluno.Assinaturas = _assinaturasRepositorio.GetAllByAlunoId(id);
            return View(detalhesAluno);
        }

        [HttpPost]
        public IActionResult Criar(AlunoViewModel alunoVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    alunoVM.CPF = alunoVM.CPF.Replace(".", "").Replace("-", "");
                    alunoVM.Telefone = alunoVM.Telefone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

                    if (!ValidarCPF(alunoVM.CPF))
                    {
                        ModelState.AddModelError("CPF", "CPF inválido");
                        return View("Criar", alunoVM);
                    }

                    // Utilize o AutoMapper para converter AlunoViewModel em Aluno
                    var student = _mapper.Map<Aluno>(alunoVM);

                    string fileName = UploadFile(alunoVM);
                    student.ImagemAluno = fileName;

                    _alunoRepositorio.Adicionar(student);
                    TempData["MensagemSucesso"] = "Aluno adicionado com sucesso";
                    return RedirectToAction("Index");
                }

                return View(alunoVM);
            }
            catch (Exception error)
            {
                TempData["MensagemErro"] = $"Não conseguimos cadastrar o aluno, tente novamente. Detalhe do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }

        private string UploadFile(AlunoViewModel alunoVM)
        {
            string fileName = null;
            if (alunoVM.ImagemAluno != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "imgs");
                fileName = Guid.NewGuid().ToString() + "_" + alunoVM.ImagemAluno.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    alunoVM.ImagemAluno.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        [HttpPost]
        public IActionResult Editar(AlunoViewModel alunoVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    alunoVM.CPF = alunoVM.CPF.Replace(".", "").Replace("-", "");
                    alunoVM.Telefone = alunoVM.Telefone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

                    if (!ValidarCPF(alunoVM.CPF))
                    {
                        ModelState.AddModelError("CPF", "CPF inválido");
                        return View("Criar", alunoVM);
                    }

                    var student = _mapper.Map<Aluno>(alunoVM);

                    string fileName = UploadFile(alunoVM);
                    student.ImagemAluno = fileName;

                    _alunoRepositorio.Editar(student);
                    TempData["MensagemSucesso"] = "Aluno alterado com sucesso";
                    return RedirectToAction("Index");
                }

                return View(alunoVM);
            }
            catch (Exception error)
            {
                TempData["MensagemErro"] = $"Não conseguimos alterar o aluno, tente novamente. Detalhe do erro: {error.Message}";
                return RedirectToAction("Index");
            }
         
        }


        public bool ValidarCPF(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", ""); // Remove os caracteres especiais do CPF

            // Verifica se o CPF tem 11 dígitos e se não é uma sequência de dígitos repetidos
            if (cpf.Length != 11 || new string(cpf[0], 11) == cpf)
                return false;

            int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            // Calcula o primeiro dígito verificador
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * multiplicadores1[i];

            int digitoVerificador1 = (11 - soma % 11) % 10;

            // Verifica se o primeiro dígito verificador está correto
            if (int.Parse(cpf[9].ToString()) != digitoVerificador1)
                return false;

            // Calcula o segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * multiplicadores2[i];

            int digitoVerificador2 = (11 - soma % 11) % 10;

            // Verifica se o segundo dígito verificador está correto
            if (int.Parse(cpf[10].ToString()) != digitoVerificador2)
                return false;

            return true; // CPF válido
        }

    }
}

