using CursoRestWebApi.Business.Interfaces;
using CursoRestWebApi.Business.Interfaces.Services;
using CursoRestWebApi.Business.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

namespace CursoRestWebApi.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificador _notificador;
        public readonly IUser UsuarioLogado;

        protected bool IsAuthenticated { get; set; }
        protected Guid UserId { get; set; }

        protected MainController(INotificador notificador, IUser user)
        {
            _notificador = notificador;
            UsuarioLogado = user;

            if (UsuarioLogado.IsAuthenticated())
            {
                IsAuthenticated = true;
                UserId = UsuarioLogado.GetUserId();
            }
        }

        protected bool OperacaoValida()
        {
            return !_notificador.ExisteNotificacao();
        }

        protected ActionResult CustomResponse(string typeAction = null, object result = null)
        {
            if (OperacaoValida())
            {
                if (typeAction == "Adicionar")
                    return CreatedAtAction(typeAction, new { success = true, data = result });
                return Ok(new { success = true, data = result });
            }

            return BadRequest(new { success = false, errors = _notificador.ObterNotificacoes().Select(e => e.Message) });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!ModelState.IsValid) NotificarErroModalInvalida(modelState);

            return CustomResponse();
        }

        protected void NotificarErroModalInvalida(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var error in errors)
            {
                string errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotificarErro(errorMsg);
            }
        }

        protected void NotificarErro(string msgErro)
        {
            _notificador.Handle(new Notificacao(msgErro));
        }
    }
}
