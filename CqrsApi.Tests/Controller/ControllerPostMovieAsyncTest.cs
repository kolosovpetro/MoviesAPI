using System.Threading;
using System.Threading.Tasks;
using CqrsApi.Core.Controllers;
using CqrsApi.Requests.CommandResponses;
using CqrsApi.Requests.Commands;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace CqrsApi.Tests.Controller
{
    [TestFixture]
    public class ControllerPostMovieAsyncTest
    {
        [Test]
        public async Task PostMovieAsync_Success_Test()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            
            var command = new PostMovieCommand
            {
                Title = "Platoon",
                Year = 1986,
                Price = 5f,
                AgeRestriction = 18
            };

            mediator.Setup(m => m.Send(It.IsAny<PostMovieCommand>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new PostMovieSuccessResponse(command.Title)));

            var controller = new MoviesController(mediator.Object, TestHelper.Mapper);

            // Act
            var response = await controller.PostMovieAsync(command);

            // Assert
            response.Should().NotBeNull();
            var objectResult = response as ObjectResult;
            objectResult?.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task PostMovieAsync_BadRequest_Test()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            
            var command = new PostMovieCommand
            {
                Title = "Platoon",
                Year = 1986,
                Price = -5f,
                AgeRestriction = 18
            };

            var controller = new MoviesController(mediator.Object, TestHelper.Mapper);

            // Act
            var response = await controller.PostMovieAsync(command);

            // Assert
            response.Should().NotBeNull();
            var objectResult = response as ObjectResult;
            objectResult?.StatusCode.Should().Be(400);
        }
    }
}