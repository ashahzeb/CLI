using MediatR;
using NFT.Application.Domain;

namespace NFT.Application.Events;

public record TransferEvent(Transaction Transaction) : INotification;