using FluentValidation;

namespace CursoRestWebApi.Business.Models.Validations
{
    public class EnderecoValidation : AbstractValidator<Endereco>
    {
        public EnderecoValidation()
        {
			RuleFor(e => e.Logradouro)
				.NotEmpty()
					.WithMessage("O campo {PropertyName} é obrigatório")
				.Length(3, 200)
					.WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

			RuleFor(e => e.Estado)
				.NotEmpty()
					.WithMessage("O campo {PropertyName} é obrigatório")
				.Length(2)
					.WithMessage("O campo {PropertyName} deve ter {MaxLength} caracteres");

			RuleFor(e => e.Bairro)
				.NotEmpty()
					.WithMessage("O campo {PropertyName} é obrigatório")
				.Length(3, 50)
					.WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

			RuleFor(e => e.Cidade)
				.NotEmpty()
					.WithMessage("O campo {PropertyName} é obrigatório")
				.Length(3, 100)
					.WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

			RuleFor(e => e.CEP)
				.NotEmpty()
					.WithMessage("O campo {PropertyName} é obrigatório")
				.Length(11)
					.WithMessage("O campo {PropertyName} deve ter {MaxLength} caracteres");

			RuleFor(e => e.Numero)
				.NotEmpty()
					.WithMessage("O campo {PropertyName} é obrigatório")
				.Length(1, 50)
					.WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");
		}
    }
}
