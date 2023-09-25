using MediatR;
using NFT.Application.Domain;

namespace NFT.Application.Events;

public record BurnEvent(Transaction Transaction) : INotification;