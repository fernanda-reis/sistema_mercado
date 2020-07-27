using Microsoft.AspNetCore.Mvc;
using aspnetcore_supermercado.Models;
using aspnetcore_supermercado.Data;

namespace aspnetcore_supermercado.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly ApplicationDbContext database;

        public EstoqueController(ApplicationDbContext database){
            this.database = database;
        }

        [HttpPost]
        public IActionResult Salvar(Estoque estoqueTemporario){
             database.Estoques.Add(estoqueTemporario);
             database.SaveChanges();
             return RedirectToAction("Estoque", "Gestao");
            
        }
    }
}