using Api.Models;

namespace Api.Services
{
    public interface IGithubService
    {
        Task<List<GithubRepository>> ObterRepositoriosCSharpMaisAntigos(string organization, int count);

    }
}
