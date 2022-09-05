using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoRestWebApi.Business.Models.Validations
{
    public class FornecedorValidation : AbstractValidator<Fornecedor>
    {
        public FornecedorValidation()
        {
			When(f => f.TipoFornecedor == (int)TipoFornecedor.PessoaFisica, () =>
			{
				RuleFor(f => f.Documento)
					.MaximumLength(11)
						.WithMessage("O campo {PropertyName} precisa ter {MaxLength} caracteres");
			});

			When(f => f.TipoFornecedor == (int)TipoFornecedor.PessoaJuridica, () =>
			{
				RuleFor(f => f.Documento)
					.MaximumLength(14)
						.WithMessage("O campo {PropertyName} precisa ter {MaxLength} caracteres");
			});

			RuleFor(f => f.Nome)
				.NotEmpty()
					.WithMessage("O campo {PropertyName} é obrigatório}")
				.Length(3, 50)
					.WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
		}
    }
}
