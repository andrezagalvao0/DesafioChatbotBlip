using Api.Controllers;
using Api.DTOs;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;

namespace ApiTest.ControllersTest
{
    [TestFixture]
    public class RepositoriesControllerTests
    {
        private Mock<IGithubService> _mockGithubService;
        private RepositoriesController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockGithubService = new Mock<IGithubService>();
            _controller = new RepositoriesController(_mockGithubService.Object);
        }

        [Test]
        public async Task QuandoTentarObterRepositóriosCSharpMaisAntigos_RetornarOkComListaDeRepositórios()
        {
            // Arrange
            var repositories = new List<GithubRepository>
            {
                new GithubRepository
                {
                    FullName = "takenet/repo1",
                    Description = "Description 1",
                    AvatarUrl = "http://example.com/avatar1.png",
                    CreatedAt = System.DateTime.Now
                },
                new GithubRepository
                {
                    FullName = "takenet/repo2",
                    Description = "Description 2",
                    AvatarUrl = "http://example.com/avatar2.png",
                    CreatedAt = System.DateTime.Now
                }
            };

            _mockGithubService.Setup(service => service.ObterRepositoriosCSharpMaisAntigos("takenet", 5))
                              .ReturnsAsync(repositories);

            // Act
            var result = await _controller.ObterRepositoriosCSharpMaisAntigos();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var returnedRepositories = okResult.Value as List<RepositoryDTO>;
            Assert.AreEqual(2, returnedRepositories.Count);
        }

        [Test]
        public async Task QuandoTentarObterRepositóriosCSharpMaisAntigos_RetornarNaoEncontrado()
        {
            // Arrange
            _mockGithubService.Setup(service => service.ObterRepositoriosCSharpMaisAntigos("takenet", 5))
                              .ReturnsAsync(new List<GithubRepository>());

            // Act
            var result = await _controller.ObterRepositoriosCSharpMaisAntigos();

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
