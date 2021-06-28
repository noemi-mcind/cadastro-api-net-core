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
        public string NomeEmpresa { get; set; }
        public long CNPJ { get; set; }
        public int Telefone { get; set; }
        public string Responsavel { get; set; }
        public long CPF { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public long CEP { get; set; }
        public string Estado { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        

    }
}
