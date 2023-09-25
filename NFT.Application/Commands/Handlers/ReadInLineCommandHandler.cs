using System.Text.Json;
using MediatR;
using NFT.Application.Domain;
using NFT.Application.Helpers;
using NFT.Application.Processors;
using NFT.Application.Responses;

namespace NFT.Application.Commands.Handlers;

public class ReadInLineCommandHandler : IRequestHandler<ReadInLineCommand, ReadInLineResponse>
{
    private readonly IJsonHelper<Transaction> _jsonHelper;
    
    private readonly ITransactionProcessor _transactionProcessor;
    
    public ReadInLineCommandHandler(IJsonHelper<Transaction> jsonHelper, ITransactionProcessor transactionTransactionProcessor)
    {
        _jsonHelper = jsonHelper;
        _transactionProcessor = transactionTransactionProcessor;
    }

    public async Task<ReadInLineResponse> Handle(ReadInLineCommand request, CancellationToken cancellationToken)
    {
        var response = new ReadInLineResponse();
        try
        {
            var transactions = new List<Transaction>();
            if (_jsonHelper.IsJsonArray(request.Json))
            {
                transactions = _jsonHelper.ProcessArray(request.Json);
            }
            else
            {
                var transaction = _jsonHelper.ProcessSingleObject(request.Json);
                if (transaction is not null)
                {
                    transactions.Add(transaction);
                }
            }

            await _transactionProcessor.Process(transactions);

            response.IsSuccess = true;
            response.Message = $"Read {transactions.Count} transaction(s)";
        }
        catch (JsonException j)
        {
            response.IsSuccess = false;
            response.Message = j.Message;
        }
        catch (Exception e)
        {
            response.IsSuccess = false;
            response.Message = e.Message;
        }

        return response;
    }
}