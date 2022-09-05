using CursoRestWebApi.Business.Interfaces.Repositorys;
using CursoRestWebApi.Business.Interfaces.Services;
using CursoRestWebApi.Business.Models;
using CursoRestWebApi.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CursoRestWebApi.Business.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
		private readonly IProdutoRepository _produtoRepository;

		public ProdutoService(IProdutoRepository produtoRepository,
							  INotificador notificador) : base(notificador)
		{
			_produtoRepository = produtoRepository;
		}

		public async Task Adicionar(Produto produto)
		{
			if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

			await _produtoRepository.Add(produto);
		}

		public async Task Atualizar(Produto produto)
		{
			if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

			await _produtoRepository.Update(produto);
		}

		public async Task Remover(Guid id)
		{
			await _produtoRepository.Remove(id);
		}
	}
}
