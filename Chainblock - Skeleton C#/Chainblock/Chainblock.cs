using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Chainblock : IChainblock
{
    Dictionary<int, Transaction> transactions;
    //Dictionary<TransactionStatus, List<Transaction>> transactionsByStatus;
    public Chainblock()
    {
        this.transactions = new Dictionary<int, Transaction>();
    //    this.transactionsByStatus = new Dictionary<TransactionStatus, List<Transaction>>();
    }
    public int Count => this.transactions.Count;

    public void Add(Transaction tx)
    {
        transactions[tx.Id] = tx;
        //if (!transactionsByStatus.ContainsKey(tx.Status))
        //{
        //    transactionsByStatus[tx.Status] = new List<Transaction>();
        //}
        //transactionsByStatus[tx.Status].Add(tx);
    }

    public void ChangeTransactionStatus(int id, TransactionStatus newStatus)
    {
        if (!transactions.ContainsKey(id))
        {
            throw new ArgumentException();
        }
        var transaction = transactions[id];
        transaction.Status = newStatus;
        transactions[id] = transaction;
    }

    public bool Contains(Transaction tx)
    {
        return this.transactions.ContainsKey(tx.Id);
    }

    public bool Contains(int id)
    {
        return this.transactions.ContainsKey(id);
    }

    public IEnumerable<Transaction> GetAllInAmountRange(double lo, double hi)
    {
        return this.transactions.Where(x => x.Value.Amount >= lo && x.Value.Amount <= hi).Select(x => x.Value);
    }

    public IEnumerable<Transaction> GetAllOrderedByAmountDescendingThenById()
    {
        return this.transactions.OrderByDescending(x => x.Value.Amount).ThenBy(x => x.Key).Select(x => x.Value);
    }

    public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
    {
        var result = transactions.Where(x => x.Value.Status == status).Select(r => r.Value.To);
        if (result.Count() > 0)
        {
            return result;
        }
        throw new InvalidOperationException();
    }

    public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
    {
        var result = transactions.Where(x => x.Value.Status == status).OrderByDescending(x => x.Value.Amount).Select(r => r.Value.From);
       
        if (result.Count() > 0)
        {
            return result;
        }
        throw new InvalidOperationException();
    }

    public Transaction GetById(int id)
    {
        if (!transactions.ContainsKey(id))
        {
            throw new InvalidOperationException();
        }
        return transactions[id];
    }

    public IEnumerable<Transaction> GetByReceiverAndAmountRange(string receiver, double lo, double hi)
    {
        var result = this.transactions.Where(x => x.Value.To == receiver && x.Value.Amount >= lo && x.Value.Amount < hi)
            .OrderByDescending(x => x.Value.Amount)
            .ThenBy(x => x.Key)
            .Select(r => r.Value);
        if (!result.Any())
        {
            throw new InvalidOperationException();
        }
        return result;
    }

    public IEnumerable<Transaction> GetByReceiverOrderedByAmountThenById(string receiver)
    {
        var result = this.transactions.Where(x => x.Value.To == receiver)
            .OrderByDescending(x => x.Value.Amount)
            .ThenBy(x => x.Key)
            .Select(r => r.Value);
        if (!result.Any())
        {
            throw new InvalidOperationException();
        }
        return result;
    }

    public IEnumerable<Transaction> GetBySenderAndMinimumAmountDescending(string sender, double amount)
    {
        var result = this.transactions.Where(x => x.Value.From == sender && x.Value.Amount > amount)
            .OrderByDescending(x => x.Value.Amount).Select(r => r.Value);
        if (!result.Any())
        {
            throw new InvalidOperationException();
        }
        return result;
    }

    public IEnumerable<Transaction> GetBySenderOrderedByAmountDescending(string sender)
    {
        var result = this.transactions.Where(x => x.Value.From == sender).OrderByDescending(x => x.Value.Amount).Select(r => r.Value);
        if (!result.Any())
        {
            throw new InvalidOperationException();
        }
        return result;
    }

    public IEnumerable<Transaction> GetByTransactionStatus(TransactionStatus status)
    {
        var result = transactions.Where(x => x.Value.Status == status).OrderByDescending(x => x.Value.Amount).Select(r => r.Value);
        if (!result.Any())
        {
            throw new InvalidOperationException(); 
        }
        return result;
    }

    public IEnumerable<Transaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, double amount)
    {
        var result = transactions.Where(x => x.Value.Status == status && x.Value.Amount <= amount)
            .OrderByDescending(x => x.Value.Amount).Select(r => r.Value);
        if (!result.Any())
        {
            return new List<Transaction>();
        }
        return result;
    }

    public IEnumerator<Transaction> GetEnumerator()
    {
        foreach (var trans in transactions.Values)
        {
            yield return trans;
        }
    }

    public void RemoveTransactionById(int id)
    {
        if (!transactions.ContainsKey(id))
        {
            throw new InvalidOperationException();
        }
        transactions.Remove(id);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

