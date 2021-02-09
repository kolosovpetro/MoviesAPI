using System.Threading;
using System.Threading.Tasks;
using CqrsApi.Core.Controllers;
using CqrsApi.Models.Models;
using CqrsApi.Requests.Queries;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace CqrsApi.Tests.Controller
{
    [TestFixture]
    public class ControllerGetMovieByIdAsyncTest
    {
        [Test]
        public async Task GetMovieByIdAsync_Success_Test()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var movie = new Movie(5, "Platoon", 1986, 18, 5f);

            mediator.Setup(m => m.Send(It.IsAny<GetMovieByIdQuery>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(movie));

            var controller = new MoviesController(mediator.Object, TestHelper.Mapper);

            // Act
            var response = await controller.GetMovieByIdAsync(1);

            // Assert
            response.Should().NotBeNull();
            var objectResult = response as ObjectResult;
            objectResult?.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task GetMovieByIdASync_NotFound_Test()
        {
            // Arrange
            var mediator = new Mock<IMediator>();

            mediator.Setup(m => m.Send(It.IsAny<GetMovieByIdQuery>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Movie>(null));

            var controller = new MoviesController(mediator.Object, TestHelper.Mapper);

            // Act
            var response = await controller.GetMovieByIdAsync(1);

            // Assert
            response.Should().NotBeNull();
            var objectResult = response as ObjectResult;
            objectResult?.StatusCode.Should().Be(404);
        }

        [Test]
        public async Task GetMovieByIdAsync_BadRequest_Test()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            
            var controller = new MoviesController(mediator.Object, TestHelper.Mapper);

            // Act
            var response = await controller.GetMovieByIdAsync(-1);

            // Assert
            response.Should().NotBeNull();
            var objectResult = response as ObjectResult;
            objectResult?.StatusCode.Should().Be(400);
        }
    }
}