using System.Text.Json;

using CunnyApi.v1.Globals;
using CunnyApi.v1.External_APIs;

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
        if (!CheckJSON(json)) {
            result = default;
            return false;
        }

        result = json;
        return true;
    }

    private bool CheckJSON<T>(in T json) {
        return json switch {
            GelbooruApiData data => data.post.Count() > 0,
            IEnumerable<YandereApiData> data => data.Count() > 0,
            IEnumerable<KonachanApiData> data => data.Count() > 0,
            IEnumerable<DanbooruApiData> data => data.Count() > 0,
            IEnumerable<LolibooruApiData> data => data.Count() > 0,
            IEnumerable<SafebooruApiData> data => data.Count() > 0,
            _ => false
        };
    }
}