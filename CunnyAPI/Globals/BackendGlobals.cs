namespace CunnyApi.v1.Globals;

public static class BackendGlobals {
    static BackendGlobals() {
        HttpClient = new(new SocketsHttpHandler {
            PooledConnectionLifetime = TimeSpan.FromMinutes(1)
        });

        HttpClient.DefaultRequestHeaders.Add("User-Agent", "CunnyApi");
        HttpClient.DefaultRequestHeaders.Add("X-CunnyApi-Ver", "v1");
        HttpClient.DefaultRequestHeaders.Add("X-CunnyApi-URL", "https://github.com/ProjectCuteAndFunny/CunnyApi");

        ApiVersion = new(1, 1);
    }
    
    public static HttpClient HttpClient { get; }
    public static Version ApiVersion { get; }
}