using System.ComponentModel.DataAnnotations;

namespace to_do_list.src.Requests
{
    public class CreateUserDTO : Request
    {
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [Display(Order = 1)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O E-mail é obrigatório.")]
        [Display(Order = 2)]
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O Telefone é obrigatório.")]
        [Display(Order = 3)]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Senha é obrigatória.")]
        [Display(Order = 4)]    
        public string Password { get; set; } = string.Empty;
        public bool Admin { get; set; } = false;
    }
}