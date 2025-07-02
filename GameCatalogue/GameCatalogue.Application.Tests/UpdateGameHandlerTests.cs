using AutoMapper;
using GameCatalogue.Application.CQRS.CommandHandlers;
using GameCatalogue.Application.CQRS.Commands;
using GameCatalogue.Application.Interfaces;
using GameCatalogue.Application.Mapping;
using GameCatalogue.Domain.Entities;
using GameCatalogue.Domain.Interfaces;
using Moq;

namespace GameCatalogue.Application.Tests
{
    public class UpdateGameHandlerTests
    {
        [Fact]
        public async Task Handle_Updates_Metadata_Only_When_NoImageStream()
        {
            // Arrange
            var game = new Game { Id = 42, Name = "Old", Price = 1m, Platforms = "pc", AddedOn = DateTime.UtcNow };
            var mockRepo = new Mock<IGameRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(42)).ReturnsAsync(game);
            mockRepo.Setup(r => r.UpdateAsync(game)).Returns(Task.CompletedTask);

            var mockFs = new Mock<IFileStorageService>();

            var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new DomainToDtoProfile()); });
            IMapper mapper = mapperConfig.CreateMapper();

            var handler = new UpdateGameHandler(mockRepo.Object, mockFs.Object, mapper);

            var cmd = new UpdateGameCommand(
                Id: 42,
                Name: "New",
                Price: 2.5m,
                LastModified: null,
                Platforms: "pc,ps4",
                ImageStream: null,
                ImageExtension: null
            );

            // Act
            var dto = await handler.Handle(cmd, CancellationToken.None);

            // Assert
            Assert.Equal("New", game.Name);
            Assert.Equal(2.5m, game.Price);
            Assert.Equal("pc,ps4", game.Platforms);
            mockRepo.Verify(r => r.UpdateAsync(game), Times.Once);
            mockFs.VerifyNoOtherCalls();
            Assert.Equal(game.Id, dto.Id);
        }

        [Fact]
        public async Task Handle_Deletes_Old_And_Saves_New_Image_When_StreamProvided()
        {
            // Arrange
            var game = new Game
            {
                Id = 7,
                Name = "G",
                Price = 3.3m,
                Platforms = "pc",
                AddedOn = DateTime.UtcNow,
                ImagePath = "images/old.png"
            };

            var mockRepo = new Mock<IGameRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(7)).ReturnsAsync(game);
            mockRepo.Setup(r => r.UpdateAsync(game)).Returns(Task.CompletedTask);

            var mockFs = new Mock<IFileStorageService>();
            mockFs.Setup(f => f.DeleteFileAsync("images/old.png"))
              .Returns(Task.CompletedTask);

            mockFs.Setup(f => f.SaveImageAsync(7, It.IsAny<Stream>(), ".jpg"))
              .ReturnsAsync("images/new.jpg");

            var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new DomainToDtoProfile()); });
            IMapper mapper = mapperConfig.CreateMapper();

            var handler = new UpdateGameHandler(mockRepo.Object, mockFs.Object, mapper);

            using var ms = new MemoryStream(new byte[] { 1, 2, 3 });

            var cmd = new UpdateGameCommand(Id: 7,
                Name: "G2",
                Price: 4.4m,
                LastModified: null,
                Platforms: "pc,ps4",
                ImageStream: ms,
                ImageExtension: ".jpg");

            // Act
            var dto = await handler.Handle(cmd, CancellationToken.None);

            // Assert metadata update
            Assert.Equal("G2", game.Name);
            Assert.Equal(4.4m, game.Price);

            // Assert file ops
            mockFs.Verify(f => f.DeleteFileAsync("images/old.png"), Times.Once);
            mockFs.Verify(f => f.SaveImageAsync(7, ms, ".jpg"), Times.Once);
            mockRepo.Verify(r => r.UpdateAsync(game), Times.Exactly(2));

            Assert.Equal("images/new.jpg", dto.ImagePath);
        }
    }
}
