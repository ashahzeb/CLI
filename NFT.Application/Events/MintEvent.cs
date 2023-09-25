using MediatR;
using NFT.Application.Domain;

namespace NFT.Application.Events;

public record MintEvent(Transaction Transaction) : INotification;