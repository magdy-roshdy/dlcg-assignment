using AutoMapper;
using GameCatalogue.Application.CQRS.Queries;
using GameCatalogue.Application.CQRS.QueryHandlers;
using GameCatalogue.Application.Mapping;
using GameCatalogue.Domain.Interfaces;
using GameCatalogue.Domain.Seeding;
using Moq;

namespace GameCatalogue.Application.Tests;

public class GetGamesHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsPagedResult_WithExpectedSampleGames()
    {
        // Arrange
        var sampleGames = GameSeedData.InitialGames;

        var mockRepo = new Mock<IGameRepository>();
        mockRepo.Setup(r => r.GetFilteredPagedAsync(
                It.Is<IEnumerable<string>>(p => p.SequenceEqual(new[] { "all" })),
                It.Is<IEnumerable<string>>(pr => !pr.Any()),
                0,
                10)).ReturnsAsync((sampleGames, sampleGames.Count));

        var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new DomainToDtoProfile()); });
        IMapper mapper = mapperConfig.CreateMapper();

        var handler = new GetGamesHandler(mockRepo.Object, mapper);

        var query = new GetGamesQuery(
            Page: 1,
            PageSize: 10,
            Platforms: ["all"],
            Prices: []
        );

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(10, result.TotalCount);
        Assert.Equal(10, result.Items.Count);

        var names = result.Items.Select(d => d.Name).ToList();
        Assert.Contains("Mortal Kombat", names);
        Assert.Contains("EA SPORTS FC", names);
        Assert.Contains("Batman: Return to Arkham", names);

        mockRepo.Verify(r => r.GetFilteredPagedAsync(
                It.Is<IEnumerable<string>>(p => p.SequenceEqual(new[] { "all" })),
                It.Is<IEnumerable<string>>(pr => !pr.Any()),
                0,
                10
            ),
            Times.Once);
    }
}
