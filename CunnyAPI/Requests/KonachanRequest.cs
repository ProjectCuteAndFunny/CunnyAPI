using System.Text;

using CunnyApi.v1.External_APIs;

namespace CunnyApi.v1.Requests;

public class KonachanRequest : BaseBooruRequest {
    public KonachanRequest(string tags) {
        StringBuilder sb = new();
        sb.Append("https://konachan.net/post.json?a");
        sb.Append("&tags=");
        sb.Append(tags.Replace(' ', '+'));
        _constructedUrl = sb.ToString();
    }

    public bool TryGetJSON(int page, out IEnumerable<KonachanApiData>? result) => InternalTryGetJSON($"{_constructedUrl}&page={page}", out result);

    private readonly string _constructedUrl;
}