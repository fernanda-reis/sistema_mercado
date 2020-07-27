using Microsoft.AspNetCore.Mvc;
using System.Linq;
using aspnetcore_supermercado.DTO;
using aspnetcore_supermercado.Models;
using aspnetcore_supermercado.Data;

namespace aspnetcore_supermercado.Controllers
{
    public class PromocoesController : Controller
    {
        private readonly ApplicationDbContext database;

        public PromocoesController(ApplicationDbContext database){
            this.database = database;
        }

        [HttpPost]
        public IActionResult Salvar(PromocaoDTO promocaoTemporaria){
            if(ModelState.IsValid){
                Promocao promocao = new Promocao();
                promocao.Nome = promocaoTemporaria.Nome;
                promocao.Produto = database.Produtos.First(produto => produto.Id == promocaoTemporaria.ProdutoId);
                promocao.Desconto = promocaoTemporaria.Desconto;
                promocao.Status = true;

                database.Promocoes.Add(promocao);
                database.SaveChanges();
                
                return RedirectToAction("Promocoes", "Gestao");
            } else {
                ViewBag.ListaDeProdutos = database.Produtos.Where(produto => produto.Status == true).ToList();
                return View("../Gestao/NovaPromocao");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(PromocaoDTO promocaoTemporaria){
            if(ModelState.IsValid){
                var promocao = database.Promocoes.First(promocao => promocao.Id == promocaoTemporaria.Id);
                promocao.Nome = promocaoTemporaria.Nome;
                promocao.Produto = database.Produtos.First(produto => produto.Id == promocaoTemporaria.ProdutoId);
                promocao.Desconto = promocaoTemporaria.Desconto;

                database.SaveChanges();

                return RedirectToAction("Promocoes", "Gestao");
            } else {
                return View("../Gestao/Promocoes");
            }
        }

        [HttpPost]
        public IActionResult Deletar(int id){
            if(id > 0){
                var promocao = database.Promocoes.First(promocao => promocao.Id == id);
                promocao.Status = false;
                database.SaveChanges();
            }
            return RedirectToAction("Promocoes", "Gestao");
        }
    }
}