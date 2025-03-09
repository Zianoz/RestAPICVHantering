using System.Text.Json;

namespace RestAPICVHantering.Endpoints
{
    public class GithubAPIEndpoint
    {
        public static void GithubAPI(WebApplication app)
        {
            // Define a GET endpoint to fetch GitHub repositories for a given username
            app.MapGet("/api/github/{username}/repos", async (string username) =>
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
                var response = await client.GetAsync($"https://api.github.com/users/{username}/repos");
                if (!response.IsSuccessStatusCode)
                {
                    return Results.StatusCode((int)response.StatusCode);
                }
                var content = await response.Content.ReadAsStringAsync();
                var repos = JsonSerializer.Deserialize<List<Repository>>(content) ?? new List<Repository>();
                var result = repos.Select(repo => new { repo.name, language = repo.language ?? "unknown", description = repo.description ?? "missing", repo.url }).ToList();
                return Results.Ok(result);
            });
        }
        private class Repository
        {
            public string name { get; set; } = string.Empty;
            public string language { get; set; } = string.Empty;
            public string description { get; set; } = string.Empty;
            public string url { get; set; } = string.Empty;
        }
    }
}
