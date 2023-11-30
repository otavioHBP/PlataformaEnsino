using System.ComponentModel.DataAnnotations;

namespace Api1.ViewModels.Accounts
{
    public class UploadImageViewModel
    {
        [Required(ErrorMessage= "Imagem inválida.")]
        public string Base64Image { get; set; } 
    }
}
