using System.Text;
using System.Text.Json;

using CunnyApi.v1.Globals;

namespace CunnyApi.v1.Requests;

public abstract class BaseBooruRequest {
    protected bool InternalTryGetJSON<T>(string url, out T? result) {
        string response;
        try {
            var task = BackendGlobals.HttpClient.GetStringAsync(url);
            task.Wait();
            response = task.Result;
        } catch (HttpRequestException) {
            result = default;
            return false;
        }
        
        T? json;
        try {
            json = JsonSerializer.Deserialize<T>(response);
        } catch (JsonException) {
            result = default;
            return false;
        }
        if (json is null) {
            result = default;
            return false;
        }

        result = json;
        return true;
    }
}