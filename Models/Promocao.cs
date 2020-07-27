namespace aspnetcore_supermercado.Models
{
    public class Promocao
    {
        public int Id { get; set; }
        public string Nome { get; set; }    
        public Produto Produto { get; set; }
        public float Desconto { get; set; }
        public bool Status { get; set; }
    }
}