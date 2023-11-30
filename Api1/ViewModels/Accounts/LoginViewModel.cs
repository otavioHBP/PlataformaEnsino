using System.ComponentModel.DataAnnotations;

namespace Api1.ViewModels.Accounts
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "informe o E-mail.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Informe a senha.")]
        public string Password { get; set; }


    }
}
