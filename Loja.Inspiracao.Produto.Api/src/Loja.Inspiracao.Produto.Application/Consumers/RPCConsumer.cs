using AutoMapper;
using Loja.Inspiracao.MQ.Models;
using Loja.Inspiracao.MQ.Models.Categoria;
using Loja.Inspiracao.MQ.Operators;
using Loja.Inspiracao.Produto.Application.Commands;
using Loja.Inspiracao.Produto.Application.Queries.Categoria;
using Loja.Inspiracao.Resources.Communication.Mediator;
using Loja.Inspiracao.Resources.Messagens.CommonMessage.Notifications;
using Loja.Inspiracao.Util.Extensions;
using MediatR;

namespace Loja.Inspiracao.Produto.Application.Consumers
{
    public class RPCConsumer : Consumer<RequestIn>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ICategoriaQueries _categoryQueries;
        private readonly IMapper _mapper;
        private readonly DomainNotificationHandler _notifications;

        public RPCConsumer(
            IMapper mapper,
            IMediatorHandler mediatorHandler,
            ICategoriaQueries categoryQueries,
            INotificationHandler<DomainNotification> notifications)
        {
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
            _categoryQueries = categoryQueries;
            _notifications = (DomainNotificationHandler)notifications;
        }

        public override async Task ConsumeContex(ConsumerContext<RequestIn> context)
        {
            switch (context.Message.Queue)
            {
                case "ObterCategoriaPorId":
                    await ObterCategoriaPorId(context);
                    break;

                case "ListaCategoria":
                    await ListaTodasCategorias(context);
                    break;

                case "AdicionarCategoria":
                    await AdicionarCategoria(context);
                    break;

                case "AlterarCategoria":
                    await AlterarCategoria(context);
                    break;

                default:
                    await ListaTodasCategorias(context);
                    break;
            }
        }

        private async Task ObterCategoriaPorId(ConsumerContext<RequestIn> context)
        {
            var id = Guid.Parse(context.Message.Result);
            var result = _mapper.Map<ResponseCategoriaOut>(await _categoryQueries.ObterCategoriaPorId(id));
            await context.RespondAsync(result);
        }
        private async Task ListaTodasCategorias(ConsumerContext<RequestIn> context)
        {
            var result = _mapper.Map<IEnumerable<ResponseCategoriaOut>>(await _categoryQueries.ObterTodasCategorias());
            await context.RespondAsync(result.ToArray());
        }

        private async Task AdicionarCategoria(ConsumerContext<RequestIn> context)
        {
            var categoriaOut = context.Message.Result.DeserializeObject<ResponseCategoriaOut>();

            var command = _mapper.Map<AdicionarCategoriaCommand>(categoriaOut);
            var result = await _mediatorHandler.SendCommand(command);

            if (result.Success)
            {
                await context.RespondAsync(new ResponseOut { Success = result.Success });
            }
            else if (!_notifications.ExistNotification())
            {
                await context.RespondAsync(new ResponseOut { Success = result.Success });
            }
        }

        private async Task AlterarCategoria(ConsumerContext<RequestIn> context)
        {
            var categoriaOut = context.Message.Result.DeserializeObject<ResponseCategoriaOut>();

            var command = _mapper.Map<AlterarCategoriaCommand>(categoriaOut);
            var result = await _mediatorHandler.SendCommand(command);

            if (result.Success)
            {
                await context.RespondAsync(new ResponseOut { Success = result.Success });
            }
            else if (!_notifications.ExistNotification())
            {
                await context.RespondAsync(new ResponseOut { Success = result.Success });
            }
        }
    }
}