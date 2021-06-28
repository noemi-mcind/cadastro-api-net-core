using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CadastroApi.Models;

namespace CadastroApi.Controllers
{
    // [controller] ele converte depois para UsuariosController e remove o Controller
    // ficando no fim: api/v1/empresas
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly CadastroContext _context;

        public EmpresasController(CadastroContext context) => _context = context;

        // Método: GET, Rota: api/v1/empresas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaDTO>>> GetEmpresa()
        {
            return await _context.Empresas
                .Select(x => ConvertClassToDTO(x))
                .ToListAsync();
        }

        // Método: GET, rota: api/v1/empresas/{id}, ex: api/v1/empresas/1
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpresaDTO>> GetEmpresa(long id)
        {
            var cadastroEmpresa = await _context.Empresas.FindAsync(id);

            if (cadastroEmpresa == null)
            {
                return NotFound();
            }

            return ConvertClassToDTO(cadastroEmpresa);
        }

        /* 
         * Método: PUT, rota: api/v1/empresas/{id}, ex: api/v1/empresas/1
         * No Request Body enviar um objeto em JSON (application/json)
              {
                   "id": 2,
                   "nome": "Novo Nome",
                   "idade": 24,
                   "email": novoemail@gmail.com
              }

         */
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmpresa(long id, EmpresaDTO empresaDTO)
        {
            // aqui é comparado se o ID que está sendo recebido como parâmetro pela ROTA
            // é DIFERENTE do ID que está sendo recebido no objeto JSON que será convertido para
            // a classe EmpresaDTO
            if (id != empresaDTO.Id)
            {
                return BadRequest();
            }

            // indo até a tabela Empresas e encontrando o primeiro registro da tabela cujo ID
            // seja igual ao do parâmetro da ROTA.
            // SELECT TOP 1 * FROM Empresa WHERE ID = 1
            var cadastroEmpresa = await _context.Empresas.FindAsync(id);

            if (cadastroEmpresa == null)
            {
                return NotFound();
            }

            cadastroEmpresa.Id = empresaDTO.Id;
            cadastroEmpresa.NomeEmpresa = empresaDTO.NomeEmpresa;
            cadastroEmpresa.CNPJ = empresaDTO.CNPJ;
            cadastroEmpresa.Telefone = empresaDTO.Telefone;
            cadastroEmpresa.Responsavel = empresaDTO.Responsavel;
            cadastroEmpresa.CPF = empresaDTO.CPF;
            cadastroEmpresa.Endereco = empresaDTO.Endereco;
            cadastroEmpresa.Cidade = empresaDTO.Cidade;
            cadastroEmpresa.CEP = empresaDTO.CEP;
            cadastroEmpresa.Estado = empresaDTO.Estado; 
            cadastroEmpresa.Email = empresaDTO.Email;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CadastroEmpresaExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // Método POST, Rota: api/v1/empresas
        // No Request Body enviar um objeto em JSON(application/json)
        /*
         *  {
         *     "Nome da Empresa": "Nome da empresa",
         *     "CNPJ": CNPJ da empresa,
         *     "Telefone": telefone da empresa,
         *     "Endereço": "endereço da empresa",
         *     "Email": "emailempresa@.gmail.com",
         *     "Senha": "snduoo"
         * 
         *  }
         */
        [HttpPost]
        public async Task<ActionResult<EmpresaDTO>> CreateEmpresa(Empresa Empresa)
        {
            var cadastroEmpresa = new Empresa
            {
                NomeEmpresa = Empresa.NomeEmpresa,
                CNPJ = Empresa.CNPJ,
                Telefone = Empresa.Telefone,
                Responsavel = Empresa.Responsavel,
                CPF = Empresa.CPF,
                Endereco = Empresa.Endereco,
                Cidade = Empresa.Cidade,
                CEP = Empresa.CEP,
                Estado = Empresa.Estado,
                Email = Empresa.Email
            };

            _context.Empresas.Add(cadastroEmpresa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEmpresa),
                new { id = cadastroEmpresa.Id },
                ConvertClassToDTO(cadastroEmpresa));
        }

        // Método: DELETE, Rota: api/v1/empresas/{id}, ex: api/v1/empresas/1
        // é só colocar o ID da rota e enviar que o objeto vai ser deletado.

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpresa(long id)
        {
            var cadastroEmpresa = await _context.Empresas.FindAsync(id);

            if (cadastroEmpresa == null)
            {
                return NotFound();
            }

            _context.Empresas.Remove(cadastroEmpresa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CadastroEmpresaExists(long id) =>
             _context.Empresas.Any(e => e.Id == id);

        // aqui esta convertendo a classe Empresa para EmpresaDTO
        private static EmpresaDTO ConvertClassToDTO(Empresa cadastroEmpresa) =>
            new EmpresaDTO
            {
                Id = cadastroEmpresa.Id,
                NomeEmpresa = cadastroEmpresa.NomeEmpresa,
                CNPJ = cadastroEmpresa.CNPJ,
                Telefone = cadastroEmpresa.Telefone,
                Responsavel = cadastroEmpresa.Responsavel,
                CPF = cadastroEmpresa.CPF,
                Endereco = cadastroEmpresa.Endereco,
                Cidade = cadastroEmpresa.Cidade,
                CEP = cadastroEmpresa.CEP,
                Estado = cadastroEmpresa.Estado,
                Email = cadastroEmpresa.Email
            };
    }
}
