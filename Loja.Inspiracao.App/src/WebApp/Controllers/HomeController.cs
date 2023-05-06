using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;
/**********************************************************************************
/  Curso: Pós-Graduação em ARQUITETURA DE SOFTWARE DISTRIBUÍDO
/  Disciplina: PROJETO INTEGRADO - ARQUITETURA DE SOFTWARE DISTRIBUÍDO (2022) 
/  Professor Orientador:  Pedro Alves
/  Aluno: Pedro Paulo Santos Carvalho
/  Data/Hora:  06/05/2023 15h00
************************************************************************************/

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}