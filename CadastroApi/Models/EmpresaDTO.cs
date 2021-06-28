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
    }
}
