using AutoMapper;
using Loja.Inspiracao.Produto.Application.Commands;
using Loja.Inspiracao.Produto.Application.Queries.Categoria;
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
    public class CategoriaController : ControllerConfiguration
    {
        private readonly ILogger<ProdutoController> _logger;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ICategoriaQueries _categoryQueries;
        private readonly IMapper _mapper;

        public CategoriaController(
            ILogger<ProdutoController> logger,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            ICategoriaQueries categoryQueries,
            IMapper mapper) : base(notifications, mediatorHandler)
        {
            _logger = logger;
            _mediatorHandler = mediatorHandler;
            _categoryQueries = categoryQueries;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("lista-categorias")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CategoriaViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Obter todas as categorias");
            return Ok(await _categoryQueries.ObterTodasCategorias());
        }

        [HttpGet]
        [Route("obter-por-id")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CategoriaViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterCategoriaPorId(Guid id)
        {
            _logger.LogInformation("Obter categoria por id");
            return Ok(await _categoryQueries.ObterCategoriaPorId(id));
        }

        [HttpPost]
        [Route("adicionar-categoria")]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DefaultResult), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DefaultResult>> AdicionarCategory(CategoriaViewModel categoriaViewModel)
        {
            var cmd = _mapper.Map<AdicionarCategoriaCommand>(categoriaViewModel);
            var result = await _mediatorHandler.SendCommand(cmd);

            if (ValidOperation())
                return Ok(result);
            else
                return BadRequest(GetMessageError());
        }
    }
}
