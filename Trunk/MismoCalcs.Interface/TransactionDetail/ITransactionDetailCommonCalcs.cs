using CalcEngine.Models.Mismo;
using System;
using System.Collections.Generic;
using System.Text;

namespace MismoCalcs.Interface.TransactionDetail
{
    /// <summary>
    /// Following interface is to expose the methods to be performed on the <TRANSACTION_DETAIL></TRANSACTION_DETAIL> node in MISMO.
    /// This interface will be injected into the respective DocType class by the DocType factory.
    /// </summary>
    public interface ITransactionDetailCommonCalcs
    {
        decimal PurchaseTotalCosts();

        decimal PurchaseTotalCredits();
    }
}
