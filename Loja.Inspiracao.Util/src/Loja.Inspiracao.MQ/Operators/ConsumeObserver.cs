using Loja.Inspiracao.Util.Extensions;
using Loja.Inspiracao.Util.Helpers.Logs;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Loja.Inspiracao.MQ.Operators
{
    public class ConsumeObserver : IConsumeObserver
    {
        public async Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception) where T : class
        {
            LogHelper.AddLog(LogLevel.Error, ConstantsLogs.CONSUME_EVENT, $"Erro " + context.SerializeObject() + "Error: " + exception.Message);
            await Task.CompletedTask;
        }

        public async Task PostConsume<T>(ConsumeContext<T> context) where T : class
        {
            LogHelper.AddLog(LogLevel.Information, ConstantsLogs.CONSUME_EVENT, $"Erro " + context.SerializeObject());

            await Task.CompletedTask;
        }

        public async Task PreConsume<T>(ConsumeContext<T> context) where T : class
        {
            LogHelper.AddLog(LogLevel.Information, ConstantsLogs.CONSUME_EVENT, $"Erro " + context.SerializeObject());

            await Task.CompletedTask;
        }
    }
}
