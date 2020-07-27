$( document ).ready(function() {
  var enderecoRelatorioDeVendas = "https://localhost:5001/Gestao/RelatorioDeVendas"

  var labels = [] // data/mes das vendas
  var day;
  var month;
  var year;
  
  var data = [] // dados em si (vendas)

  $.post(enderecoRelatorioDeVendas, function(dados, status){

  labels = dados.map(function(item){
    dataOriginal = item.data.slice(0, 10)

    day = dataOriginal.slice(8,10)
    month = dataOriginal.slice(5,7)
    year = dataOriginal.slice(0,4)

    dataFinal = day + "/" + month + "/" + year;

    return dataFinal
  })


    for(var i=0; i < dados.length; i++) {
      data.push(dados[i].total)
    }
   
   
    var ctx = document.getElementById('grafico').getContext('2d');
    var chart = new Chart(ctx, {
      type:"line",
      data:{
        labels:labels,
        datasets: [{
          label: "Total de vendas em R$",
          data: data,
          borderWidth: 12,
          //borderColor: blue,
          //backgroundColor: '#ffce56'
        }]
      }
    });


  });
});