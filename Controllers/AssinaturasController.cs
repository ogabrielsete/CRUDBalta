using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TesteBalta.Models;
using TesteBalta.Models.ViewModels;
using TesteBalta.Repositorio;

namespace TesteBalta.Controllers
{
    public class AssinaturasController : Controller
    {
        private readonly IAssinaturasRepositorio _assinaturasRepositorio;
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly IMapper _mapper;
        public AssinaturasController(IAssinaturasRepositorio assinaturasRepositorio,
                                    IAlunoRepositorio alunoRepositorio, IMapper mapper)
        {
            _assinaturasRepositorio = assinaturasRepositorio;
            _alunoRepositorio = alunoRepositorio;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            List<Assinatura> listarAssinaturas = _assinaturasRepositorio.GetAll();

            return View(listarAssinaturas);
        }

        public IActionResult Criar()
        {
            var dropDownAlunos = _alunoRepositorio.MostrarAlunos();
            ViewBag.Alunos = new SelectList(dropDownAlunos, "Id", "Nome");

            
            return View();
        }

        public IActionResult Editar(int id)
        {
            var dropDownAlunos = _alunoRepositorio.MostrarAlunos();
            ViewBag.Alunos = new SelectList(dropDownAlunos, "Id", "Nome");

            Assinatura assinatura = _assinaturasRepositorio.GetById(id);
            var assinVM = _mapper.Map<Assinatura, AssinaturaViewModel>(assinatura);
            return View(assinVM);
        }

        public IActionResult Deletar(int id)
        {
            try
            {
                Assinatura assinatura = _assinaturasRepositorio.GetById(id);

                _assinaturasRepositorio.Deletar(id);
                return RedirectToAction("Index", TempData["MensagemSucesso"] = "Assinatura excluída com sucesso!");
            }
            catch (Exception error)
            {

                return RedirectToAction("Index", TempData["MensagemErro"] = $"Ocorreu um erro ao tentar excluir a assinatura. Detalhe: {error.Message}");
            }
        }


        [HttpPost]
        public IActionResult Criar(AssinaturaViewModel assinaturaVM)
        {
            var dropDownAlunos = _alunoRepositorio.MostrarAlunos();

            if (ModelState.IsValid)
            {
                var assinatura = _mapper.Map<Assinatura>(assinaturaVM);


                // Definir a data de término corretamente para evitar assinaturas no passado
                if (assinatura.Termino < DateTime.Now)
                {
                    ModelState.AddModelError("Termino", "A data de término deve ser no futuro.");
                    dropDownAlunos = _alunoRepositorio.MostrarAlunos();
                    ViewBag.Alunos = new SelectList(dropDownAlunos, "Id", "Nome");
                    return View(assinaturaVM);
                }

                var ass = _assinaturasRepositorio.GetAllByAlunoId(assinatura.AlunoId);
                if (ass.Count == 0)
                {
                    assinatura.Ativo = true;
                }
                else
                {
                    var lastData = ass.Last().Termino;
                    var lastName = ass.Last().Id.ToString();

                    assinatura.Ativo = false;
                    assinatura.Inicio = lastData;
                    if (assinatura.Termino <= lastData)
                    {
                        TempData["MensagemErro"] = $"A data termino deve ser maior que {lastData} do contrato {lastName}.";
                        RedirectToAction("Index");
                    }
                }
                TempData["MensagemSucesso"] = "Assinatura adicionada com sucesso!";
                _assinaturasRepositorio.Adicionar(assinatura);
                return RedirectToAction("Index");
            }
            dropDownAlunos = _alunoRepositorio.MostrarAlunos();
            ViewBag.Alunos = new SelectList(dropDownAlunos, "Id", "Nome");
            return View(assinaturaVM);

        }

        [HttpPost]
        public IActionResult Editar(AssinaturaViewModel assinaturaVM)
        {

            var dropDownAlunos = _alunoRepositorio.MostrarAlunos();

            if (ModelState.IsValid)
            {
                var assinatura = _mapper.Map<Assinatura>(assinaturaVM);


                // Definir a data de término corretamente para evitar assinaturas no passado
                if (assinatura.Termino < DateTime.Now)
                {
                    ModelState.AddModelError("Termino", "A data de término deve ser no futuro.");
                    dropDownAlunos = _alunoRepositorio.MostrarAlunos();
                    ViewBag.Alunos = new SelectList(dropDownAlunos, "Id", "Nome", "Sobrenome");
                    return View(assinaturaVM);
                }

                var ass = _assinaturasRepositorio.GetAllByAlunoId(assinatura.AlunoId);
                if (ass.Count == 0)
                {
                    assinatura.Ativo = true;
                }
                else
                {
                    var lastData = ass.Last().Termino;
                    var lastName = ass.Last().Id.ToString();

                    assinatura.Ativo = false;
                    assinatura.Inicio = lastData;
                    if (assinatura.Termino <= lastData)
                    {
                        TempData["MensagemErro"] = $"A data termino deve ser maior que {lastData} do contrato {lastName}.";
                        RedirectToAction("Index");
                    }
                }
                TempData["MensagemSucesso"] = "Assinatura editada com sucesso!";
                _assinaturasRepositorio.Editar(assinatura);
                return RedirectToAction("Index");
            }
            return View(assinaturaVM);
        }
    }
}

