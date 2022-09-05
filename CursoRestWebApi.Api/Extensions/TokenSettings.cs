namespace CursoRestWebApi.Api.Extensions
{
    public class TokenSettings
    {
        public string Secret { get; set; }
        public int ExpiraHoras { get; set; }
        public string Emissor { get; set; }
        public string ValidoEm { get; set; }
    }
}
