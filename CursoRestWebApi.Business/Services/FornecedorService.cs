using CursoRestWebApi.Business.Interfaces.Repositorys;
using CursoRestWebApi.Business.Interfaces.Services;
using CursoRestWebApi.Business.Models;
using CursoRestWebApi.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoRestWebApi.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
		private readonly IFornecedorRespository _fornecedorRepository;
		private readonly IEnderecoRepository _enderecoRepository;

		public FornecedorService(IFornecedorRespository fornecedorRepository,
								 IEnderecoRepository enderecoRepository,
								 INotificador notificador) : base(notificador)
		{
			_fornecedorRepository = fornecedorRepository;
			_enderecoRepository = enderecoRepository;
		}

		public async Task Adicionar(Fornecedor fornecedor)
		{
			if (!ExecutarValidacao(new FornecedorValidation(), fornecedor) ||
				!ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)) return;

			if ((await _fornecedorRepository.GetAllFilter(f => f.Documento == fornecedor.Documento)).Any())
			{
				Notificar("Já existe um fornecedor com este documento.");
				return;
			}

			await _fornecedorRepository.Add(fornecedor);
		}

		public async Task Atualizar(Fornecedor fornecedor)
		{
			if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return;

			if ((await _fornecedorRepository.GetAllFilter(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id)).Any())
			{
				Notificar("Já existe um fornecedor com este documento.");
				return;
			}

			await _fornecedorRepository.Update(fornecedor);
		}

		public async Task AtualizarEndereco(Endereco endereco)
		{
			if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

			await _enderecoRepository.Update(endereco);
		}

		public async Task Remover(Guid id)
		{
			if ((await _fornecedorRepository.GetProdutosFornecedor(id)).Produtos.Any())
			{
				Notificar("O fornecedor ainda possui produtos vinculados");
				return;
			}

			await _fornecedorRepository.Remove(id);
		}
	}
}
