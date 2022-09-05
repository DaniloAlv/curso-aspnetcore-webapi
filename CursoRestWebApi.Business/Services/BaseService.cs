using CursoRestWebApi.Business.Interfaces.Services;
using CursoRestWebApi.Business.Models;
using CursoRestWebApi.Business.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace CursoRestWebApi.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected bool ExecutarValidacao<TValidator, TEntity>(TValidator validator, TEntity entity) where TValidator : AbstractValidator<TEntity> where TEntity : Entity
        {
            var validate = validator.Validate(entity);

            if (validate.IsValid) return true;

            Notificar(validate);

            return false;
        }
    }
}
