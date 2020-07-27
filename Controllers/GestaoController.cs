using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using aspnetcore_supermercado.Data;
using aspnetcore_supermercado.DTO;

namespace aspnetcore_supermercado.Controllers
{
    //[Authorize]
    public class GestaoController : Controller
    {
        private readonly ApplicationDbContext database;

        public GestaoController(ApplicationDbContext database){
            this.database = database;
        }
        public IActionResult Index(){
            return View();
        }

        public IActionResult Categorias(){
            var categorias = database.Categorias.Where(categoria => categoria.Status == true).ToList();
            return View(categorias);
        }

        public IActionResult NovaCategoria(){
            return View();
        }

        public IActionResult EditarCategoria(int id){
            var categoria = database.Categorias.First(categoria => categoria.Id == id);

            CategoriaDTO categoriaView = new CategoriaDTO();
            categoriaView.Id = categoria.Id;
            categoriaView.Nome = categoria.Nome;

            return View(categoriaView);
        }

        public IActionResult Fornecedores(){
            var fornecedores = database.Fornecedores.Where(fornecedor => fornecedor.Status == true).ToList();
            return View(fornecedores);
        }

        public IActionResult NovoFornecedor(){
            return View();
        }

        public IActionResult EditarFornecedor(int id){
            var fornecedor = database.Fornecedores.First(fornecedor => fornecedor.Id == id);

            FornecedorDTO fornecedorView = new FornecedorDTO();
            fornecedorView.Id = fornecedor.Id;
            fornecedorView.Nome = fornecedor.Nome;
            fornecedorView.Email = fornecedor.Email;
            fornecedorView.Telefone = fornecedor.Telefone;

            return View(fornecedorView);
        }

        public IActionResult Produtos(){
            var produtos = database.Produtos.Include(produto => produto.Categoria).Include(produto => produto.Fornecedor).Where(produto => produto.Status == true).ToList();
            return View(produtos);
        }

        public IActionResult NovoProduto(){
            ViewBag.ListaDeCategorias = database.Categorias.Where(categoria => categoria.Status == true).ToList();
            ViewBag.ListaDeFornecedores = database.Fornecedores.Where(fornecedor => fornecedor.Status == true).ToList();
            return View();
        }

        public IActionResult EditarProduto(int id){
            var produto = database.Produtos.Include(produto => produto.Categoria).Include(produto => produto.Fornecedor).First(produto => produto.Id == id);

            ProdutoDTO produtoView = new ProdutoDTO();
            produtoView.Id = produto.Id;
            produtoView.Nome = produto.Nome;
            produtoView.PrecoDeCusto = produto.PrecoDeCusto;
            produtoView.PrecoDeVenda = produto.PrecoDeVenda;
            produtoView.CategoriaId = produto.Categoria.Id;
            produtoView.FornecedorId = produto.Fornecedor.Id;
            produtoView.Medicao = produto.Medicao;
            
            ViewBag.ListaDeCategorias = database.Categorias.Where(categoria => categoria.Status == true).ToList();
            ViewBag.ListaDeFornecedores = database.Fornecedores.Where(fornecedor => fornecedor.Status == true).ToList();
            
            return View(produtoView);
        }

        public IActionResult Promocoes(){
            var promocoes = database.Promocoes.Include(promocao => promocao.Produto).Where(promocao => promocao.Status == true).ToList();
            return View(promocoes);
        }

        public IActionResult NovaPromocao(){
            ViewBag.ListaDeProdutos = database.Produtos.Where(produto => produto.Status == true).ToList();
            return View();
        }

        public IActionResult EditarPromocao(int id){
            var promocao = database.Promocoes.Include(promocao => promocao.Produto).First(promocao => promocao.Id == id);
            
            PromocaoDTO promocaoView = new PromocaoDTO();
            promocaoView.Id = promocao.Id;
            promocaoView.Nome = promocao.Nome;
            promocaoView.ProdutoId = promocao.Produto.Id;
            promocaoView.Desconto = promocao.Desconto;  

            ViewBag.ListaDeProdutos = database.Produtos.Where(produto => produto.Status == true).ToList();

            return View(promocaoView);
        }

        public IActionResult Estoque(){
            var estoques = database.Estoques.Include(estoque => estoque.Produto).ToList();
            return View(estoques);
        }

        public IActionResult NovoEstoque(){
            ViewBag.ListaDeProdutos = database.Produtos.Where(produto => produto.Status == true).ToList();
            return View();
        }

        public IActionResult EditarEstoque(){
            return Content("");
        }

        public IActionResult Vendas(){
            var vendas = database.Vendas.ToList();
            return View(vendas);
        }

        [HttpPost]
        public IActionResult RelatorioDeVendas(){
            return Ok(database.Vendas.ToList());
        }
    }
}