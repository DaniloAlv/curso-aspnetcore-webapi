using CursoRestWebApi.Business.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoRestWebApi.Business.Interfaces.Services
{
    public interface INotificador
    {
        bool ExisteNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificao);
    }
}
