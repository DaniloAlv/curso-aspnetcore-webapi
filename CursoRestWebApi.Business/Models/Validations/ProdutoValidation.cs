using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoRestWebApi.Business.Models.Validations
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
			RuleFor(p => p.Nome)
				.NotEmpty()
					.WithMessage("O campo {PropertyName} é obrigatório")
				.Length(2, 100)
					.WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

			RuleFor(p => p.Valor)
				.GreaterThan(0).WithMessage("O campo {PropertyName} deve ser maior que {ComparisonValue}");

			RuleFor(p => p.Descricao)
				.NotEmpty()
					.WithMessage("O campo {PropertyName} é obrigatório")
				.Length(3, 500)
					.WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");
		}
    }
}
