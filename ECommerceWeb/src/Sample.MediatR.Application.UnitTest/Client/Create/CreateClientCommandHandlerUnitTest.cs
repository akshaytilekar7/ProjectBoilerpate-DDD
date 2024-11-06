using AutoMapper;
using Moq;
using Sample.MediatR.Application.Client.Create;
using Sample.MediatR.Domain.Contracts;
using System;
using Xunit;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Sample.MediatR.Persistence.Notification;

namespace Sample.MediatR.Application.UnitTest.Client.Create
{
    public class CreateClientCommandHandlerUnitTest
    {
        private CreateClientCommand _request;
        private Mock<IRepository<Domain.Client>> _repositoryMock;
        private Mock<IMediator> _meditRMock;
        private IMapper _mapper;

        public CreateClientCommandHandlerUnitTest()
        {
            _repositoryMock = new Mock<IRepository<Domain.Client>>();
            _mapper = MapperInstace.SetMapper(_mapper);
            _meditRMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task Handle_Should_Create_Client_And_Publish_Event()
        {
            // Arrange
            _request = new CreateClientCommand()
            {
                CreateClient = new Dto.CreateClientRequestDto()
                {
                    Name = "Akshay",
                    Email = "akshay@gmail.com",
                    Date = new DateTime(2022, 1, 15)
                }
            };

            var client = _mapper.Map<Domain.Client>(_request);
            client.Id = 10;

            var handler = new CreateClientCommandHandler(_repositoryMock.Object, _mapper, _meditRMock.Object);

            _repositoryMock.Setup(x => x.AddAsync(It.Is<Domain.Client>(c => c.Name == "Akshay"))).ReturnsAsync(client);

            // Act
            var result = await handler.Handle(_request, CancellationToken.None);

            // Assert
            Assert.Equal(client.Id, result.Id);
            Assert.Equal(client.Name, result.Name);
            _repositoryMock.Verify(x => x.AddAsync(It.Is<Domain.Client>(c => c.Name == "Akshay")), Times.Once);
            _meditRMock.Verify(x => x.Publish(It.Is<ClientCreatedDoaminEvent>(f => f.ClientId == client.Id), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Not_Create_Client_And_Not_Publish_Event()
        {
            // Arrange
            _request = new CreateClientCommand()
            {
                CreateClient = new Dto.CreateClientRequestDto()
                {
                    Name = "Akshay",
                    Email = "akshay@gmail.com",
                    Date = new DateTime(2022, 1, 15)
                }
            };

            var client = _mapper.Map<Domain.Client>(_request);
            client.Id = 10;

            var handler = new CreateClientCommandHandler(_repositoryMock.Object, _mapper, _meditRMock.Object);

            _repositoryMock.Setup(x => x.AddAsync(It.Is<Domain.Client>(c => c.Name == "Akshay"))).ReturnsAsync((Domain.Client)null);

            // Act
            var result = await handler.Handle(_request, CancellationToken.None);

            // Assert
            Assert.Null(result);
            _repositoryMock.Verify(x => x.AddAsync(It.Is<Domain.Client>(c => c.Name == "Akshay")), Times.Once);
            _meditRMock.Verify(x => x.Publish(It.Is<ClientCreatedDoaminEvent>(f => f.ClientId == client.Id), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
