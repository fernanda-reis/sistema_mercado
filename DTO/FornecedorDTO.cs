using System.ComponentModel.DataAnnotations;

namespace aspnetcore_supermercado.DTO
{
    public class FornecedorDTO
    {
        [Required]
        public int Id { get; set; }
        
        [Required(ErrorMessage="Campo obrigatório!")]
        [StringLength(100, ErrorMessage="Nome muito longo, tente um nome menor!")]
        [MinLength(2, ErrorMessage="Nome muito pequeno, tente um nome maior!")]
        public string Nome { get; set; }

        [Required(ErrorMessage="Campo obrigatório!")]
        [EmailAddress(ErrorMessage="E-mail inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage="Campo obrigatório!")]
        [Phone(ErrorMessage="Número inválido!")]
        [MinLength(10, ErrorMessage="Número inválido!")]
        public string Telefone { get; set; }
        
    }
}