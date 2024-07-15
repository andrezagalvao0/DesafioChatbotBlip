using Api.DTOs;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RepositoriesController : ControllerBase
    {
        private readonly IGithubService _githubService;

        public RepositoriesController(IGithubService githubService)
        {
            _githubService = githubService;
        }

        [HttpGet()]
        public async Task<IActionResult> ObterRepositoriosCSharpMaisAntigos()
        {
            var repositories = await _githubService.ObterRepositoriosCSharpMaisAntigos("takenet", 5);

            if (!repositories.Any())
            {
                return NotFound();
            }

            var repositoriesDTO = repositories.Select(repo => new RepositoryDTO
            {
                FullName = repo.FullName,
                Description = repo.Description,
                AvatarUrl = repo.AvatarUrl
            }).ToList();

            return Ok(repositoriesDTO);
        }
    }
}
