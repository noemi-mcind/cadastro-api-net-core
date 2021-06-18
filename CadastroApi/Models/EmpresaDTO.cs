using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroApi.Models
{
    // Classe que será convertida para JSON para ler um usuário já criado
    public class EmpresaDTO
    {
        public long Id { get; set; }
        public string NomeDaEmpresa { get; set; }
        public string EnderecoDaEmpresa { get; set; }
        public int Telefone { get; set; }
        public long CNPJ { get; set; }
        public string Email { get; set; }
    }
}
