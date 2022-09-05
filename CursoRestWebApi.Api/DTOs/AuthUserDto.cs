using System.ComponentModel.DataAnnotations;

namespace CursoRestWebApi.Api.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(15, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem entre si.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(15, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
