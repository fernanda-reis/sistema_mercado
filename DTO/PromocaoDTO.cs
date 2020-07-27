using System.ComponentModel.DataAnnotations;

namespace aspnetcore_supermercado.DTO
{
    public class PromocaoDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage="Campo obrigatório!")]
        [StringLength(100,ErrorMessage="Nome deve ter menos de 100 caracteres!")]
        [MinLength(3, ErrorMessage="Nome muito curto!")]
        public string Nome { get; set; }   

        [Required(ErrorMessage="Campo obrigatório!")]
        public int ProdutoId { get; set; }
        
        [Required(ErrorMessage="Campo obrigatório!")]
        [Range(0,100, ErrorMessage="Defina um valor entre 0 e 100!")]
        public float Desconto { get; set; }
    }
}