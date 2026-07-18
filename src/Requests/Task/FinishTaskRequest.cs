using System.ComponentModel.DataAnnotations;

namespace to_do_list.src.Requests
{
    public class FinishTaskRequest : Request
    {
        [Required(ErrorMessage = "O Id é obrigatório.")]
        [Display(Order = 1)]
        public string Id { get; set; } = string.Empty;
    }
}