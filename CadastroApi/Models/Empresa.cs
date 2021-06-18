using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroApi.Models
{
    // Classe convertida para JSON para criar o usuário
    public class Empresa
    {
        public long Id { get; set; }
        public string NomeDaEmpresa { get; set; }
        public string EnderecoDaEmpresa { get; set; }
        public int Telefone { get; set; }
        public long CNPJ {get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        

    }
}
