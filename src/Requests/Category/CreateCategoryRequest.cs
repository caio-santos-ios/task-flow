using System.ComponentModel.DataAnnotations;

namespace to_do_list.src.Requests
{
    public class CreateCategoryRequest : Request
    {
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [Display(Order = 1)]
        public string Name { get; set; } = string.Empty;
    }
}