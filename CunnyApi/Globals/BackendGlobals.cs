namespace CunnyApi.v1.Globals;

public static class BackendGlobals {
    public static readonly HttpClient HttpClient = new(new SocketsHttpHandler {
        PooledConnectionLifetime = TimeSpan.FromMinutes(1)
    });
}