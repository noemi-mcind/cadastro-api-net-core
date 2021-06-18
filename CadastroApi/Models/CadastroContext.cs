using CadastroApi.Controllers.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroApi.Models
{
    public class CadastroContext : DbContext
    {
        public CadastroContext(DbContextOptions<CadastroContext> options)
            : base(options)
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
    }
}