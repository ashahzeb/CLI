using AutoFixture;
using Moq;
using NFT.Application.Domain;
using NFT.Application.Domain.Repositories;
using NFT.Application.Storage;
using Xunit;

namespace NFT.Application.Tests.Domain.Repositories;

public class TransactionRepositoryTests
{
    private readonly Fixture _fixture = new();
    
    [Fact]
    public async Task GetByIdAsync_ExistingTokenId_ReturnsValue()
    {
        // Arrange
        var tokenId = "tokenId";
        var address = "address";
        var ownershipDataMock = new Mock<IOwnershipData>();
        ownershipDataMock.SetupGet(d => d.Data).Returns(new Dictionary<string, string> { { tokenId, address } });
        var repository = new TransactionRepository(ownershipDataMock.Object);

        // Act
        var result = await repository.GetByIdAsync(tokenId);

        // Assert
        Assert.Equal(address, result);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingTokenId_ReturnsEmptyString()
    {
        // Arrange
        var tokenId = "nonExistingTokenId";
        var ownershipDataMock = new Mock<IOwnershipData>();
        ownershipDataMock.SetupGet(d => d.Data).Returns(new Dictionary<string, string>());
        var repository = new TransactionRepository(ownershipDataMock.Object);

        // Act
        var result = await repository.GetByIdAsync(tokenId);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public async Task GetByAddressAsync_ExistingAddress_ReturnsTokens()
    {
        // Arrange
        var address = "address";
        var tokenId1 = "token1";
        var tokenId2 = "token2";
        var ownershipDataMock = new Mock<IOwnershipData>();
        ownershipDataMock.SetupGet(d => d.Data).Returns(new Dictionary<string, string>
        {
            { tokenId1, address },
            { tokenId2, address }
        });
        var repository = new TransactionRepository(ownershipDataMock.Object);

        // Act
        var result = await repository.GetByAddressAsync(address);

        // Assert
        Assert.Equal(new[] { tokenId1, tokenId2 }, result);
    }

    [Fact]
    public async Task GetByAddressAsync_NonExistingAddress_ReturnsEmptyList()
    {
        // Arrange
        var address = "nonExistingAddress";
        var ownershipDataMock = new Mock<IOwnershipData>();
        ownershipDataMock.SetupGet(d => d.Data).Returns(new Dictionary<string, string>());
        var repository = new TransactionRepository(ownershipDataMock.Object);

        // Act
        var result = await repository.GetByAddressAsync(address);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task AddAsync_AddsTransactionToData()
    {
        // Arrange
        var transaction = _fixture.Create<Transaction>();
        var ownershipDataMock = new Mock<IOwnershipData>();
        ownershipDataMock.Setup(_ => _.Data).Returns(new Dictionary<string, string>());
        var repository = new TransactionRepository(ownershipDataMock.Object);

        // Act
        await repository.AddAsync(transaction);

        // Assert
        ownershipDataMock.Verify(data => data.SaveData(), Times.Once);
        Assert.Equal(transaction.Address, ownershipDataMock.Object.Data[transaction.TokenId]);
    }

    [Fact]
    public async Task DeleteAsync_RemovesTransactionFromData()
    {
        // Arrange
        var tokenId = "token1";
        var ownershipDataMock = new Mock<IOwnershipData>();
        ownershipDataMock.Setup(d => d.Data).Returns(new Dictionary<string, string> { { tokenId, "address1" } });
        var repository = new TransactionRepository(ownershipDataMock.Object);

        // Act
        await repository.DeleteAsync(new Transaction { TokenId = tokenId });

        // Assert
        ownershipDataMock.Verify(data => data.SaveData(), Times.Once);
        Assert.False(ownershipDataMock.Object.Data.ContainsKey(tokenId));
    }

    [Fact]
    public async Task UpdateAsync_UpdatesTransactionInData()
    {
        // Arrange
        var tokenId = "token1";
        var originalAddress = "address1";
        var updatedAddress = "address2";
        var ownershipData = new OwnershipData();
        var repository = new TransactionRepository(ownershipData);

        repository.AddAsync(new Transaction() { TokenId = tokenId, Address = originalAddress });
        
        // Act
        await repository.UpdateAsync(new Transaction { TokenId = tokenId, To = updatedAddress });

        // Assert
        Assert.Equal(updatedAddress, ownershipData.Data[tokenId]);
    }
}
