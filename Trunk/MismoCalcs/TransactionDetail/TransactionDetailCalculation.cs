using CalcEngine.Models.Mismo;
using MismoCalcs.Implementation.TransactionDetail;
using MismoCalcs.Interface.TransactionDetail;

namespace CalcEngine.Calcs.TransactionDetail
{
    public class TransactionDetailCalculation
    {
        private readonly ITransactionDetailCommonCalcs _transactionDetailCommonCalcs;
        private readonly ITransactionDetailRefinanceCalcs _transactionDetailRefinanceCalcs;

        public TransactionDetailCalculation(ITransactionDetailCommonCalcs transactionDetailCommonCalcs,
            ITransactionDetailRefinanceCalcs transactionDetailRefinanceCalcs = null)
        {
            _transactionDetailCommonCalcs = transactionDetailCommonCalcs;
            _transactionDetailRefinanceCalcs = transactionDetailRefinanceCalcs;
        }

        public decimal PurchaseTotalCosts()
        {
            return _transactionDetailCommonCalcs.PurchaseTotalCosts();
        }

        public decimal RefinanceTotalCosts()
        {
            return _transactionDetailRefinanceCalcs.RefinanceTotalCosts();
        }
    }
}
