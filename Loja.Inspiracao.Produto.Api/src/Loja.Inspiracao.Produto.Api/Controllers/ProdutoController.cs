using AutoMapper;
using Loja.Inspiracao.Produto.Application.Commands;
using Loja.Inspiracao.Produto.Application.Queries.Produto;
using Loja.Inspiracao.Produto.Application.ViewModels;
using Loja.Inspiracao.Resources.Communication.Mediator;
using Loja.Inspiracao.Resources.Messagens.CommonMessage.Notifications;
using Loja.Inspiracao.Resources.Util;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Loja.Inspiracao.Produto.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerConfiguration
    {
        private readonly ILogger<ProdutoController> _logger;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IProdutoQueries _produtoQueries;
        private readonly IMapper _mapper;

        public ProdutoController(
            ILogger<ProdutoController> logger,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            IProdutoQueries produtoQueries,
            IMapper mapper) : base(notifications, mediatorHandler)
        {
            _logger = logger;
            _mediatorHandler = mediatorHandler;
            _produtoQueries = produtoQueries;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("lista-produtos")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProdutoViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Obter todos os produtos");
            return Ok(await _produtoQueries.ObterTodosProdutos());
        }

        [HttpGet]
        [Route("obter-por-id")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CategoriaViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterCategoriaPorId(Guid id)
        {
            _logger.LogInformation("Obter produto por id");
            return Ok(await _produtoQueries.ObterProdutoPorId(id));
        }

        [HttpPost]
        [Route("adicionar-produto")]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResult>> AdicionarProduto(ProdutoViewModel produtoViewModel)
        {
            var cmd = _mapper.Map<AdicionarProdutoCommand>(produtoViewModel);
            var result = await _mediatorHandler.SendCommand(cmd);

            if (ValidOperation())
                return Ok(result);
            else
                return BadRequest(GetMessageError());
        }
    }
}
