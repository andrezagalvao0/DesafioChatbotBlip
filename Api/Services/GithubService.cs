using Api.Models;
using Octokit;

namespace Api.Services
{
    public class GithubService : IGithubService
    {
        private readonly GitHubClient _githubClient;

        public GithubService()
        {
            _githubClient = new GitHubClient(new ProductHeaderValue("APIGithubBlip"));
        }

        public async Task<List<GithubRepository>> ObterRepositoriosCSharpMaisAntigos(string organization, int count)
        {
            var repositories = await _githubClient.Repository.GetAllForOrg(organization);
            var csharpRepositories = repositories
                .Where(repo => repo.Language == "C#")
                .OrderBy(repo => repo.CreatedAt)
                .Take(count)
                .Select(repo => new GithubRepository
                {
                    FullName = repo.FullName,
                    Description = repo.Description,
                    AvatarUrl = repo.Owner.AvatarUrl,
                    CreatedAt = repo.CreatedAt.DateTime
                })
                .ToList();

            return csharpRepositories;
        }
    }
}
