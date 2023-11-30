using System.ComponentModel.DataAnnotations;

namespace Api1.ViewModels.Accounts
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O nome é um campo obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O E-mail é um campo obrigatório.")]
        [EmailAddress(ErrorMessage = "O E-mail é inválido.")]
        public string Email { get; set; }


    }
}
