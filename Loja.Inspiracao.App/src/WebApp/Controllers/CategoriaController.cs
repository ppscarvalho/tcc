using Microsoft.AspNetCore.Mvc;
using WebApp.Integracao.Application.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaAppService _categoriaAppService;

        public CategoriaController(ICategoriaAppService categoriaAppService)
        {
            _categoriaAppService = categoriaAppService;
        }

        // GET: CategoriaController
        public async Task<IActionResult> Index()
        {
            var categorias = await _categoriaAppService.ObterTodasCategorias();
            return View(categorias);
        }

        // GET: CategoriaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoriaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaViewModel categoriaViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                var result = await _categoriaAppService.SalvarCategoria(categoriaViewModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriaController/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var resp = await _categoriaAppService.ObterCategoriaPorId(id);
            return View(resp);
        }

        // POST: CategoriaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoriaViewModel categoriaViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                var result = await _categoriaAppService.AlterarCategoria(categoriaViewModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriaController/Delete/5
        public ActionResult Delete(Guid? id)
        {
            return View();
        }

        // POST: CategoriaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
