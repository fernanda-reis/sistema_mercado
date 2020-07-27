using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using aspnetcore_supermercado.DTO;
using aspnetcore_supermercado.Models;
using aspnetcore_supermercado.Data;

namespace aspnetcore_supermercado.Controllers
{
    public class VendasController : Controller
    {
        private readonly ApplicationDbContext database;

        public VendasController(ApplicationDbContext database){
            this.database = database;
        }
        [HttpPost]
        public IActionResult GerarVenda([FromBody] VendaDTO dados){

            //Registras venda
            Venda venda = new Venda();
            venda.Total = dados.total;
            venda.Troco = dados.troco;
            //troco menor/igual a 1 cent
            venda.ValorPago = dados.troco <= 0.01f ? dados.total : dados.total + dados.troco;
            venda.Data = DateTime.Now;

            database.Vendas.Add(venda);
            database.SaveChanges();

            //Registrar saídas de produtos
            List<Saida> saidas = new List<Saida>();
            foreach (var saida in dados.produtos) {
                Saida s = new Saida();
                s.Quantidade = saida.quantidade;
                s.ValorDeVenda = saida.subtotal;
                s.Venda = venda;
                s.Produto = database.Produtos.First(produto => produto.Id == saida.produto);
                s.Data = DateTime.Now;

                saidas.Add(s);
            }

            database.AddRange(saidas);
            database.SaveChanges();
            
            return Ok(new{msg="Venda processada com sucesso!"});
        }
    }
}