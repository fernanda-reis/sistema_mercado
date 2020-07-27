using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using aspnetcore_supermercado.Data;
using aspnetcore_supermercado.DTO;
using aspnetcore_supermercado.Models;

namespace aspnetcore_supermercado.Controllers
{
    public class CategoriasController : Controller
    {

        private readonly ApplicationDbContext database;

        public CategoriasController(ApplicationDbContext database){
            this.database = database;
        }


        [HttpPost]
        public IActionResult Salvar(CategoriaDTO categoriaTemporaria){
            if (ModelState.IsValid){
                Categoria categoria = new Categoria();
                categoria.Nome = categoriaTemporaria.Nome;
                categoria.Status = true;

                database.Categorias.Add(categoria);
                database.SaveChanges();

                return RedirectToAction("Categorias", "Gestao");
            } else {
                return View("../Gestao/NovaCategoria");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(CategoriaDTO categoriaTemporaria){
            if(ModelState.IsValid){
                var categoria = database.Categorias.First(categoria => categoria.Id == categoriaTemporaria.Id);
                categoria.Nome = categoriaTemporaria.Nome;
                database.SaveChanges();
                return RedirectToAction("Categorias", "Gestao");
            } else {
                return View("../Gestao/EditarCategoria");
            }
        }

        [HttpPost]
        public IActionResult Deletar(int id){
            if (id > 0) {
                var categoria = database.Categorias.First(categoria => categoria.Id == id);
                categoria.Status = false;
                database.SaveChanges();                
            }
            return RedirectToAction("Categorias", "Gestao");
        }
    }
}