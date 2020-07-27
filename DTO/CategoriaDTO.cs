using System.ComponentModel.DataAnnotations;

namespace aspnetcore_supermercado.DTO
{
    public class CategoriaDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage="Campo obrigatório!")]
        [StringLength(100, ErrorMessage="Nome muito longo, tente um nome menor!")]
        [MinLength(2, ErrorMessage="Nome muito pequeno, tente um nome maior!")]
        public string Nome { get; set; }
        
    }
}