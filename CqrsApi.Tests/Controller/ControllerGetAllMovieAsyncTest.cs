using System.Threading;
using System.Threading.Tasks;
using CqrsApi.Core.Controllers;
using CqrsApi.Requests.Queries;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace CqrsApi.Tests.Controller
{
    [TestFixture]
    public class ControllerGetAllMovieAsyncTest
    {
        [Test]
        public async Task Controller_GetAllMoviesAsync_Test()
        {
            // Arrange
            var mediator = new Mock<IMediator>();

            mediator.Setup(m => m.Send(It.IsAny<GetAllMoviesQuery>(),
                    It.IsAny<CancellationToken>()))
                .Returns(() => Task.FromResult(TestHelper.AllMovies));

            var controller = new MoviesController(mediator.Object, TestHelper.Mapper);
            
            // Act
            var response = await controller.GetAllMoviesAsync();

            // Assert
            response.Should().NotBeNull();
            var objectResult = response as ObjectResult;
            objectResult?.StatusCode.Should().Be(200);
        }
    }
}