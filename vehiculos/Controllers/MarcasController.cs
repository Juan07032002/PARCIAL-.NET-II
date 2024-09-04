using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vehiculos.Data;
using vehiculos.Models;
using vehiculos.Repository.Interface;

namespace vehiculos.Controllers
{
    public class MarcasController : Controller
    {
        private readonly IMarcaRepository _marcaRepository;

        public MarcasController(IMarcaRepository marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _marcaRepository.GetAll());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var marca = await _marcaRepository.GetById(id.Value);
            if (marca == null) return NotFound();

            return View(marca);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Marca marca)
        {
            if (!ModelState.IsValid) return View(marca);

            await _marcaRepository.Add(marca);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var marca = await _marcaRepository.GetById(id.Value);
            if (marca == null) return NotFound();

            return View(marca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Marca marca)
        {
            if (id != marca.MarcaId) return NotFound();

            if (!ModelState.IsValid) return View(marca);

            try
            {
                await _marcaRepository.Update(marca);
            }
            catch
            {
                if (!await _marcaRepository.Exists(marca.MarcaId)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var marca = await _marcaRepository.GetById(id.Value);
            if (marca == null) return NotFound();

            return View(marca);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _marcaRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
