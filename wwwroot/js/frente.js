
/* Declaração de variáveis */

var enderecoProduto = "https://localhost:5001/Produtos/Produto/"
var enderecoGerarVenda = "https://localhost:5001/Vendas/GerarVenda/"
var produto;
var compras = [];
var __totalVenda__ = 0;


/* Início */
$("#posvenda").hide();
atualizarTotal();

function atualizarTotal(){
    $("#totalVenda").html(__totalVenda__);

}
/* Funções */

function preencherFormulario(dadosProduto){
    $("#nomeProduto").val(dadosProduto.nome);
    $("#categoriaProduto").val(dadosProduto.categoria.nome);
    $("#fornecedorProduto").val(dadosProduto.fornecedor.nome);
    $("#precoProduto").val(dadosProduto.precoDeVenda);
}

function limparFormulario(){
    $("#nomeProduto").val("");
    $("#categoriaProduto").val("");
    $("#fornecedorProduto").val("");
    $("#precoProduto").val("");
    $("#qtdProduto").val("");
    $("#codProduto").val("");

}

function preencherTabela(produtoTabela, quantidade){

    //clonagem de produto para que o valor dele
    //não se perca dentro do array vendas
    var produtoTemp = {}
    Object.assign(produtoTemp, produto);
    var venda = {
        produto: produtoTemp,
        quantidade: parseInt(quantidade),
        subtotal: parseFloat(produtoTemp.precoDeVenda * quantidade)
    };

    __totalVenda__ += venda.subtotal;   
    compras.push(venda);

    $("#tbProdutos").append(`<tr>
    <td>${produtoTabela.id}</td>
    <td>${produtoTabela.nome}</td>
    <td>${quantidade}</td>
    <td>R$ ${produtoTabela.precoDeVenda}</td>
    <td>${produtoTabela.medicao}</td>
    <td>R$ ${(produtoTabela.precoDeVenda * quantidade).toFixed(2)}</td>
    <td><button class="btn btn-danger">Remover</button></td>
    </tr>`)

}

$("#formProduto").on("submit", function(event){
    event.preventDefault();
    //var produtoParaTabela = produto;
    var qtdProduto = $("#qtdProduto").val();

    preencherTabela(produto, qtdProduto);
    atualizarTotal();
    //var produto = undefined;
    limparFormulario();
})

/* Ajax */

$("#pesquisar").click(function(){
    var codProduto = $("#codProduto").val();

    $.post(enderecoProduto+codProduto, function(dados, status){
        produto = dados;

        var med = "";
        switch(produto.medicao){
            case 0:
                med = "L";
                break;
            case 1:
                med = "Kg";
                break;
            case 2:
                med = "Un";
                break;
            default:
                med = "Un";
                break;
        }
        produto.medicao = med;

        preencherFormulario(produto);   
    }).fail(function(){
        alert("Produto não encontrado.")
    })
})

$("#btnFinalizarVenda").click(function(){
    if(__totalVenda__ <= 0 ){
        alert("Nenhum produto selecionado!")
        return;
    }

    var valorPago = parseFloat($("#campoValorPago").val());
    
    if(valorPago != undefined){
        if(valorPago >= __totalVenda__){

            $("#posvenda").show();
            $("#prevenda").hide();
            $("#campoValorPago").prop("disabled", true)


            var _troco = (valorPago - __totalVenda__)
            $("#campoTroco").val(_troco)


            //Processar e enviar dados da venda para o back-end
            compras.forEach(elemento => {
                elemento.produto = parseInt(elemento.produto.id);
            })

            var _venda = {
                total: parseFloat(__totalVenda__),
                troco: parseFloat(_troco),
                produtos: compras
            };

            $.ajax({
                type: "POST",
                url: enderecoGerarVenda,
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(_venda),
                success: function (data){
                    //console.log("dados enviados com sucesso");
                }
            });

        } else {
            alert("Valor pago muito baixo!")
        }
    } else {
        alert("Valor pago inválido!")
        return;
    }
})

function restaurarModal(){
    $("#posvenda").hide();
    $("#prevenda").show();
    $("#campoValorPago").prop("disabled", false)

    $("#campoValorPago").val("");
    $("#campoTroco").val("")
}

$("#btnFecharModal").click(function(){
    restaurarModal();
})
