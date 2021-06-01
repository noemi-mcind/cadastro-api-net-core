using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroApi.Models
{
    // Classe que será convertida para JSON para ler um usuário já criado
    public class UsuarioDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Email { get; set; }
     
     }
}
