using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using aspnetcore_supermercado.Data;
using aspnetcore_supermercado.DTO;
using aspnetcore_supermercado.Models;

namespace aspnetcore_supermercado.Controllers
{
    public class ProdutosController : Controller
    {

        private readonly ApplicationDbContext database;

        public ProdutosController(ApplicationDbContext database){
            this.database = database;
        }

        [HttpPost]
        public IActionResult Salvar(ProdutoDTO produtoTemporario){
            if(ModelState.IsValid){
                Produto produto = new Produto();
                produto.Nome = produtoTemporario.Nome;
                produto.Categoria = database.Categorias.First(categoria => categoria.Id == produtoTemporario.CategoriaId);
                produto.Fornecedor = database.Fornecedores.First(fornecedor => fornecedor.Id == produtoTemporario.FornecedorId);
                produto.PrecoDeCusto = produtoTemporario.PrecoDeCusto;
                produto.PrecoDeVenda = produtoTemporario.PrecoDeVenda;
                produto.Medicao = produtoTemporario.Medicao;
                produto.Status = true;

                database.Produtos.Add(produto);
                database.SaveChanges();

                return RedirectToAction("Produtos", "Gestao");
            } else {
                ViewBag.ListaDeCategorias = database.Categorias.Where(categoria => categoria.Status == true).ToList();
                ViewBag.ListaDeFornecedores = database.Fornecedores.Where(fornecedor => fornecedor.Status == true).ToList();
                return View("../Gestao/NovoProduto");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(ProdutoDTO produtoTemporario){
            if(ModelState.IsValid){
                var produto = database.Produtos.First(produto => produto.Id == produtoTemporario.Id);

                produto.Nome = produtoTemporario.Nome;
                produto.Categoria = database.Categorias.First(categoria => categoria.Id == produtoTemporario.CategoriaId);
                produto.Fornecedor = database.Fornecedores.First(fornecedor => fornecedor.Id == produtoTemporario.FornecedorId);
                produto.PrecoDeCusto = produtoTemporario.PrecoDeCusto;
                produto.PrecoDeVenda = produtoTemporario.PrecoDeVenda;
                produto.Medicao = produtoTemporario.Medicao;

                database.SaveChanges();
                return RedirectToAction("Produtos", "Gestao");
            } else {
                return View("../Gestao/EditarFornecedores");
            } 
        }

        [HttpPost]
        public IActionResult Deletar(int id){
            if(id > 0 ){
                var produto = database.Produtos.First(produto => produto.Id == id);
                produto.Status = false;
                database.SaveChanges();
            }

            return RedirectToAction("Produtos", "Gestao");
        }

        [HttpPost]
        public IActionResult Produto(int id){
            if(id > 0){
                var produto = database.Produtos.Where(produto => produto.Status == true).Include(produto => produto.Categoria).Include(produto => produto.Fornecedor).First(produto => produto.Id == id);

                if(produto != null) {
                                
                    var estoque = database.Estoques.First(estoque => estoque.Produto.Id == produto.Id);
                    
                    if(estoque == null){
                        produto = null;
                    }
                }
                            
                if(produto != null) {
                    Promocao promocao;
                    try {
                        promocao = database.Promocoes.First(promocao => promocao.Produto.Id == produto.Id && produto.Status == true);
                    } catch(Exception e){
                        promocao = null;
                    }

                    if(promocao != null){
                        produto.PrecoDeVenda -= (produto.PrecoDeVenda * (promocao.Desconto/100));
                    }

                    Response.StatusCode = 200; //ok
                    return Json(produto);    
                } else {
                    Response.StatusCode = 404; //falha
                    return Json(null);
                }
            } else {
                Response.StatusCode = 404; //falha
                return Json(null);
            }
        }
    
    }
}
