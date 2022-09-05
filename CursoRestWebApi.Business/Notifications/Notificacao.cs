namespace CursoRestWebApi.Business.Notifications
{
    public class Notificacao
    {
        public string Message { get; }

        public Notificacao(string message)
        {
            Message = message;
        }
    }
}
