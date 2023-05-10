using System.Transactions;

namespace Loja.Inspiracao.Util.Helpers
{
    public class TransactionHelper
    {
        public static TransactionScope CreateTransactionScopeAsync()
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            };

            return new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
