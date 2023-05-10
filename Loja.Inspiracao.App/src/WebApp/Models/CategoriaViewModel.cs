using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class CategoriaViewModel
    {
        public Guid? Id { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Descrição da Categoria obrigatória.")]
        public string? Descricao { get; set; }
    }
}
