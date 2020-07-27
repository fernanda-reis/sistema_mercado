using System.ComponentModel.DataAnnotations;

namespace aspnetcore_supermercado.DTO
{
    public class ProdutoDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage="Campo obrigatório!")]
        [StringLength(100, ErrorMessage="Nome deve ter menos de 100 caracteres.")]
        [MinLength(2, ErrorMessage="Nome do produto muito curto.")]
        public string Nome { get; set; }

        [Required(ErrorMessage="Campo obrigatório!")]
        public int CategoriaId { get; set; }
        
        [Required(ErrorMessage="Campo obrigatório!")]
        public int FornecedorId { get; set; }

        [Required(ErrorMessage="Campo obrigatório!")]
        public float PrecoDeCusto { get; set; }

        [Required(ErrorMessage="Campo obrigatório!")]
        public float PrecoDeVenda { get; set; }

        [Required(ErrorMessage="Campo obrigatório!")]
        [Range(0, 2, ErrorMessage="Medição inválida")]
        public int Medicao { get; set; } 
    }
}