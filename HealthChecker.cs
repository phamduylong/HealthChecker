namespace HealthChecker;

public class HealthChecker
{
    private static readonly List<string> SitesToBeChecked = [
        "https://longph.dev",
        "https://alias-web-based.vercel.app",
        "https://cli.longph.dev",
        "https://shopping-list-pi-three.vercel.app/",
        "https://v1.longph.dev/",
        "https://random-netflix-quotes.vercel.app/"
    ];
    
    public record CheckResult
    {
        public required string SiteUrl { get; set; }
        public required bool IsOnline { get; set; }
    }

    public static List<CheckResult> Check()
    {
        var results = new List<CheckResult>();
        var httpClient = new HttpClient();
        foreach (string site in SitesToBeChecked)
        {
            results.Add(new CheckResult()
            {
                SiteUrl = site,
                IsOnline = SiteIsOnline(site)
            });
        }
        
        return results;
    }

    public static bool SiteIsOnline(string url)
    {
        var client = new HttpClient();

        var webRequest = new HttpRequestMessage(HttpMethod.Get, url);

        var response = client.Send(webRequest);

        return response.IsSuccessStatusCode;
    }
}