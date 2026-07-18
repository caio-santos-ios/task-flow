using System.ComponentModel.DataAnnotations;

namespace to_do_list.src.Requests
{
    public class CreateTaskRequest : Request
    {
        [Required(ErrorMessage = "O Titulo é obrigatório.")]
        [Display(Order = 1)]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
    }
}