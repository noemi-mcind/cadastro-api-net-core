using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroApi.Controllers.Models
{
    // Classe convertida para JSON para criar o usuário
    public class Usuario
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

    }
}
