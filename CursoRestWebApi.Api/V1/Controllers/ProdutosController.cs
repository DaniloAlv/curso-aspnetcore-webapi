using AutoMapper;
using CursoRestWebApi.Api.Controllers;
using CursoRestWebApi.Api.DTOs;
using CursoRestWebApi.Api.Extensions;
using CursoRestWebApi.Business.Interfaces;
using CursoRestWebApi.Business.Interfaces.Repositorys;
using CursoRestWebApi.Business.Interfaces.Services;
using CursoRestWebApi.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CursoRestWebApi.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IFornecedorRespository _fornecedorRespository;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoRepository produtoRepository,
                                  IMapper mapper,
                                  INotificador notificador,
                                  IProdutoService produtoService,
                                  IFornecedorRespository fornecedorRespository,
                                  IUser user) : base(notificador, user)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _produtoService = produtoService;
            _fornecedorRespository = fornecedorRespository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> ObterTodos()
        {
            var produtos = await _produtoRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<ProdutoDTO>>(produtos));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProdutoDTO>> ObterPorId(Guid id)
        {
            var produto = await _produtoRepository.GetProdutoFornecedor(id);

            if (produto == null) return NotFound(new { success = false, error = "Produto não encontrado!" });

            return Ok(new { success = true, data = _mapper.Map<ProdutoDTO>(produto) });
        }

        [AllowAnonymous]
        [HttpGet("fornecedores")]
        public async Task<IActionResult> ObterFornecedoresProdutoCombo()
        {
            return Ok(_mapper.Map<IEnumerable<FornecedorProdutoCombo>>(await _fornecedorRespository.GetAll()));
        }

        [HttpPost]
        [ClaimsAuthorize("Produto", "Adicionar")]
        public async Task<ActionResult<ProdutoDTO>> Adicionar([FromForm] ProdutoDTO produtoDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            string fileName = string.Concat(Guid.NewGuid(), $"_{produtoDto.Imagem}");
            if (!UploadArquivo(produtoDto.ImagemUpload, fileName)) return CustomResponse();

            produtoDto.Imagem = fileName;
            await _produtoService.Adicionar(_mapper.Map<Produto>(produtoDto));

            return CustomResponse(nameof(Adicionar), produtoDto);
        }

        [HttpPut("{id:guid}")]
        [ClaimsAuthorize("Produto", "Editar")]
        public async Task<ActionResult<ProdutoDTO>> Atualizar(Guid id, [FromBody] ProdutoDTO produtoDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (id != produtoDto.Id)
            {
                NotificarErro("Id do produto não é o mesmo passado como paramêtro!");
                return CustomResponse();
            }

            await _produtoService.Atualizar(_mapper.Map<Produto>(produtoDto));

            return CustomResponse(nameof(Atualizar), produtoDto);
        }

        [HttpDelete("{id:guid}")]
        [ClaimsAuthorize("Produto", "Excluir")]
        public async Task<ActionResult<ProdutoDTO>> Remover(Guid id)
        {
            var produto = await _produtoRepository.GetById(id);

            if (produto == null) return NotFound(new { success = false, error = "Não foi encontrado nenhum produto." });

            await _produtoService.Remover(id);

            return CustomResponse();
        }

        private bool UploadArquivo(string arquivo, string nomeArquivo)
        {
            if (string.IsNullOrEmpty(arquivo))
            {
                NotificarErro("Nenhuma imagem foi fornecida para o produto.");
                return false;
            }

            byte[] arrayFile = Convert.FromBase64String(arquivo);

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", nomeArquivo);

            if (System.IO.File.Exists(filePath))
            {
                NotificarErro("Já existe um arquivo com o mesmo nome.");
                return false;
            }

            System.IO.File.WriteAllBytes(filePath, arrayFile);
            return true;
        }

        private bool UploadArquivoAlternativo(IFormFile arquivo, string nomeArquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                NotificarErro("Não foi submetida nenhuma imagem para o produto.");
                return false;
            }

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", nomeArquivo);

            if (System.IO.File.Exists(filePath))
            {
                NotificarErro("Já existe um arquivo com o mesmo nome.");
                return false;
            }

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                arquivo.CopyToAsync(fs);
                return true;
            }
        }
    }
}
