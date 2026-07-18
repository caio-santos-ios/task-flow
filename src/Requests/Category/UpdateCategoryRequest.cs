using System.ComponentModel.DataAnnotations;

namespace to_do_list.src.Requests
{
    public class UpdateCategoryRequest : Request
    {
        [Required(ErrorMessage = "O Id é obrigatório.")]
        [Display(Order = 1)]
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [Display(Order = 2)]
        public string Name { get; set; } = string.Empty;
    }
}