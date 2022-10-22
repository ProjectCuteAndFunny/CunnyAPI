namespace CunnyApi.v1.Globals;

public static class BackendGlobals {
    static BackendGlobals() {
        HttpClient = new(new SocketsHttpHandler {
            PooledConnectionLifetime = TimeSpan.FromMinutes(1)
        });

        ApiVersion = new(1, 1, 1);
        LatestVersionRoute = "v1";

        HttpClient.DefaultRequestHeaders.Add("User-Agent", "CunnyApi");
        HttpClient.DefaultRequestHeaders.Add("X-CunnyApi-Ver", ApiVersion.ToString());
        HttpClient.DefaultRequestHeaders.Add("X-CunnyApi-URL", "https://github.com/ProjectCuteAndFunny/CunnyApi");
    }

    public static string LatestVersionRoute { get; }
    public static HttpClient HttpClient { get; }
    public static Version ApiVersion { get; }
}