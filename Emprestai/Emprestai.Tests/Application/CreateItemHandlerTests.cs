using AutoMapper;
using Emprestai.Application.Items.Create;
using Emprestai.Application.Mappings;
using Emprestai.Application.Repositories;
using Emprestai.Domain.Entities;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace Emprestai.Tests.Application;

public class CreateItemHandlerTests
{
    private readonly Mock<IItemRepository> _itemRepositoryMock;

    public CreateItemHandlerTests()
    {
        _itemRepositoryMock = new Mock<IItemRepository>();
    }

    [Fact]
    public async Task Handle_ShouldCreateTwoItems_WhenNamesAreEqual()
    {
        // Arrange
        var firstId = Guid.NewGuid();
        var secondId = Guid.NewGuid();

        var configuration = new MapperConfiguration(
            cfg => cfg.AddProfile<ItemMappingProfile>(),
            NullLoggerFactory.Instance);

        var mapper = configuration.CreateMapper();
        var handler = new CreateItemHandler(_itemRepositoryMock.Object, mapper);
        var command = new CreateItemCommand
        {
            Name = "Furadeira",
            Description = "Furadeira de bancada"
        };

        var secondCommand = new CreateItemCommand
        {
            Name = "Furadeira",
            Description = "Furadeira de bancada"
        };

        var ids = new Queue<Guid>(new[] { firstId, secondId });

        _itemRepositoryMock
            .Setup(repo => repo.CreateAsync(
                It.IsAny<Item>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Item item, CancellationToken _) =>
            {
                item.Id = ids.Dequeue();
                return item;
            });

        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        var secondResult = await handler.Handle(secondCommand, CancellationToken.None);

        // Assert
        _itemRepositoryMock.Verify(
            repo => repo.CreateAsync(
                It.Is<Item>(item => item.Name == "Furadeira"),
                It.IsAny<CancellationToken>()),
            Times.Exactly(2));

        Assert.Equal(firstId, result.Id);
        Assert.Equal(secondId, secondResult.Id);
    }
}
