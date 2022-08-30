using System.Text;

using CunnyApi.v1.External_APIs;

namespace CunnyApi.v1.Requests;

public class YandereRequest : BaseBooruRequest {
    public YandereRequest(string tags) {
        StringBuilder sb = new();
        sb.Append("https://yande.re/post.json?a");
        sb.Append($"&tags={tags.Replace(' ', '+')}");
        _constructedUrl = sb.ToString();
    }

    public bool TryGetJSON(int page, out IEnumerable<YandereApiData>? result) => InternalTryGetJSON($"{_constructedUrl}&page={page}", out result);

    private readonly string _constructedUrl;
}