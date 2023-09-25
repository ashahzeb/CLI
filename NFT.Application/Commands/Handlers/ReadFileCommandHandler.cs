using System.Text.Json;
using System.Text.Json.Nodes;
using MediatR;
using NFT.Application.Domain;
using NFT.Application.Helpers;
using NFT.Application.Processors;
using NFT.Application.Responses;

namespace NFT.Application.Commands.Handlers;

public class ReadFileCommandHandler : IRequestHandler<ReadFileCommand, ReadFileResponse>
{
    private readonly IJsonHelper<Transaction> _jsonHelper;
    
    private readonly ITransactionProcessor _transactionProcessor;
    
    public ReadFileCommandHandler(IJsonHelper<Transaction> jsonHelper, ITransactionProcessor transactionTransactionProcessor)
    {
        _jsonHelper = jsonHelper;
        _transactionProcessor = transactionTransactionProcessor;
    }
    
    public async Task<ReadFileResponse> Handle(ReadFileCommand request, CancellationToken cancellationToken)
    {
        var response = new ReadFileResponse();
        try
        {
            if (!File.Exists(request.FileName))
            {
                response.IsSuccess = false;
                response.Message = "File not found";
            }
            else
            {
                var text = File.ReadAllText(request.FileName);
                JsonNode.Parse(text);
                var transactions = new List<Transaction>();
                if (_jsonHelper.IsJsonArray(text))
                {
                    transactions = _jsonHelper.ProcessArray(text);
                }
                else
                {
                    transactions.Add(_jsonHelper.ProcessSingleObject(text));
                }

                await _transactionProcessor.Process(transactions);
                
                response.IsSuccess = true;
                response.Message = $"Read {transactions.Count} transaction(s)";
            }
        }
        catch (JsonException j)
        {
            response.IsSuccess = false;
            response.Message = "Invalid JSON format";
        }
        catch (Exception e)
        {
            response.IsSuccess = false;
            response.Message = e.Message;
        }

        return response;
    }
}