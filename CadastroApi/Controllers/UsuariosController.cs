using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CadastroApi.Controllers.Models;
using CadastroApi.Models;

namespace CadastroApi.Controllers
{
    // [controller] ele converte depois para UsuariosController e remove o Controller
    // ficando no fim: api/v1/usuarios
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly CadastroContext _context;

        public UsuariosController(CadastroContext context) => _context = context;

        // Método: GET, Rota: api/v1/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            return await _context.Usuarios
                .Select(x => ConvertClassToDTO(x))
                .ToListAsync();
        }

        // Método: GET, rota: api/v1/usuarios/{id}, ex: api/v1/usuarios/1
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(long id)
        {
            var cadastroUsuario = await _context.Usuarios.FindAsync(id);

            if (cadastroUsuario == null)
            {
                return NotFound();
            }

            return ConvertClassToDTO(cadastroUsuario);
        }

        /* 
         * Método: PUT, rota: api/v1/usuarios/{id}, ex: api/v1/usuarios/1
         * No Request Body enviar um objeto em JSON (application/json)
              {
                   "id": 2,
                   "nome": "Novo Nome",
                   "idade": 24,
                   "email": novoemail@gmail.com
              }

         */
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(long id, UsuarioDTO usuarioDTO)
        {
            // aqui é comparado se o ID que está sendo recebido como parâmetro pela ROTA
            // é DIFERENTE do ID que está sendo recebido no objeto JSON que será convertido para
            // a classe UsuarioDTO
            if (id != usuarioDTO.Id)
            {
                return BadRequest();
            }

            // indo até a tabela Usuarios e encontrando o primeiro registro da tabela cujo ID
            // seja igual ao do parâmetro da ROTA.
            // SELECT TOP 1 * FROM Usuarios WHERE ID = 1
            var cadastroUsuario = await _context.Usuarios.FindAsync(id);
            
            if (cadastroUsuario == null)
            {
                return NotFound();
            }

            cadastroUsuario.Id = usuarioDTO.Id;
            cadastroUsuario.Nome = usuarioDTO.Nome;
            cadastroUsuario.Idade = usuarioDTO.Idade;
            cadastroUsuario.Email = usuarioDTO.Email;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CadastroUsuarioExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // Método POST, Rota: api/v1/usuarios
        // No Request Body enviar um objeto em JSON(application/json)
        /*
         *  {
         *     "Nome": "Nome do usuario",
         *     "Idade": idade do usuario,
         *     "Email": "emailusuario@.gmail.com",
         *     "Senha": "snduoo"
         * 
         *  }
         */
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> CreateUsuario(Usuario Usuario)
        {
            var cadastroUsuario = new Usuario
            {
                Nome =  Usuario.Nome,
                Idade = Usuario.Idade,
                Email = Usuario.Email,
                Senha = Usuario.Senha
            };

            _context.Usuarios.Add(cadastroUsuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetUsuario),
                new { id = cadastroUsuario.Id },
                ConvertClassToDTO(cadastroUsuario));
        }

        // Método: DELETE, Rota: api/v1/usuarios/{id}, ex: api/v1/usuarios/1
        // é só colocar o ID da rota e enviar que o objeto vai ser deletado.

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(long id)
        {
            var cadastroUsuario = await _context.Usuarios.FindAsync(id);

            if (cadastroUsuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(cadastroUsuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CadastroUsuarioExists(long id) =>
             _context.Usuarios.Any(e => e.Id == id);

        // aqui esta convertendo a classe Usuario para UsuarioDTO
        private static UsuarioDTO ConvertClassToDTO(Usuario cadastroUsuario) =>
            new UsuarioDTO
            {
                Id = cadastroUsuario.Id,
                Nome = cadastroUsuario.Nome,
                Idade = cadastroUsuario.Idade,
                Email = cadastroUsuario.Email,
                
            };
    }
}
