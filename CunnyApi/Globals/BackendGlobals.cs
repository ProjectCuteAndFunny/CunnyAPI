namespace CunnyApi.v1.Globals;

public static class BackendGlobals {
    static BackendGlobals() {
        HttpClient = new(new SocketsHttpHandler {
            PooledConnectionLifetime = TimeSpan.FromMinutes(1)
        });

        HttpClient.DefaultRequestHeaders.Add("User-Agent", "CunnyApi");
        HttpClient.DefaultRequestHeaders.Add("X-CunnyApi-Ver", "v1");
        HttpClient.DefaultRequestHeaders.Add("X-CunnyApi-URL", "https://github.com/ProjectCuteAndFunny/CunnyApi");
    }
    
    public static HttpClient HttpClient { get; }
}