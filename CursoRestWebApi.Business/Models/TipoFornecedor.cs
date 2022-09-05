using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CursoRestWebApi.Business.Models
{
    public enum TipoFornecedor
    {
        [Description("Pessoa Física")]
        PessoaFisica = 1,

        [Description("Pessoa Jurídica")]
        PessoaJuridica = 2
    }
}
