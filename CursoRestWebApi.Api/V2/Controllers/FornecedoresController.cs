using AutoMapper;
using CursoRestWebApi.Api.Controllers;
using CursoRestWebApi.Api.DTOs;
using CursoRestWebApi.Api.Extensions;
using CursoRestWebApi.Api.V1.Controllers;
using CursoRestWebApi.Business.Interfaces;
using CursoRestWebApi.Business.Interfaces.Repositorys;
using CursoRestWebApi.Business.Interfaces.Services;
using CursoRestWebApi.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursoRestWebApi.Api.V2.Controllers
{
    [Authorize]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FornecedoresController : MainController
    {
        private readonly IFornecedorRespository _fornecedorRespository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorRespository fornecedorRespository,
                                      IMapper mapper,
                                      INotificador notificador,
                                      IFornecedorService fornecedorService,
                                      IUser user) : base(notificador, user)
        {
            _fornecedorRespository = fornecedorRespository;
            _mapper = mapper;
            _fornecedorService = fornecedorService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<FornecedorDTO>>> ObterTodos()
        {
            var fornecedores = await _fornecedorRespository.GetAll();
            return Ok(_mapper.Map<IEnumerable<FornecedorDTO>>(fornecedores));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FornecedorDTO>> Get(Guid id)
        {
            var fornecedor = await _fornecedorRespository.GetEnderecoFornecedor(id);

            if (fornecedor == null) return NotFound(new { success = false, error = "Fornecedor não encontrado." });

            return Ok(new { success = true, data = _mapper.Map<FornecedorDTO>(fornecedor) });
        }

        [HttpPost]
        [ClaimsAuthorize("Fornecedor", "Adicionar")]
        public async Task<ActionResult<FornecedorDTO>> Adicionar([FromForm] FornecedorDTO fornecedorDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _fornecedorService.Adicionar(_mapper.Map<Fornecedor>(fornecedorDto));
            return CreatedAtAction(nameof(Adicionar), new { success = true, data = fornecedorDto });
        }

        [HttpPut("{id:guid}")]
        [ClaimsAuthorize("Fornecedor", "Editar")]
        public async Task<ActionResult<FornecedorDTO>> Editar(Guid id, [FromForm] FornecedorDTO fornecedorDto)
        {
            if (!ModelState.IsValid) return BadRequest(new { success = false, error = "Houve erro(s) na validação do formulário. Tente novamente." });

            if (id != fornecedorDto.Id) return BadRequest(new { success = false, error = "Parâmetro não confere com o fornecedor." });

            try
            {
                await _fornecedorService.Atualizar(_mapper.Map<Fornecedor>(fornecedorDto));
                return Ok(new { success = true, data = fornecedorDto });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new { success = false, error = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        [ClaimsAuthorize("Fornecedor", "Excluir")]
        public async Task<ActionResult> Excluir(Guid id)
        {
            var fornecedor = await _fornecedorRespository.GetById(id);

            if (fornecedor == null) return NotFound(new { success = false, error = "Fornecedor não encontrado." });

            await _fornecedorService.Remover(id);
            return CustomResponse();
        }

        [HttpPut("atualizar-endereco/{id:guid}")]
        [ClaimsAuthorize("Fornecedor", "Editar")]
        public async Task<ActionResult<EnderecoDTO>> AtualizarEndereco(Guid id, FornecedorDTO fornecedor)
        {
            if (id != fornecedor.Id)
            {
                NotificarErro("O parâmetro Id é diferente ao do fornecedor requisitado.");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _fornecedorService.AtualizarEndereco(_mapper.Map<Endereco>(fornecedor.Endereco));

            return CustomResponse(nameof(AtualizarEndereco), fornecedor.Endereco);
        }
    }
}
