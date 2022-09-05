using CursoRestWebApi.Business.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CursoRestWebApi.Business.Notifications
{
    public class Notificador : INotificador
    {
        private List<Notificacao> _notificacoes;

        public Notificador()
        {
            _notificacoes = new List<Notificacao>();
        }

        public bool ExisteNotificacao()
        {
            return _notificacoes.Any();
        }

        public void Handle(Notificacao notificao)
        {
            _notificacoes.Add(notificao);
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return _notificacoes;
        }
    }
}
