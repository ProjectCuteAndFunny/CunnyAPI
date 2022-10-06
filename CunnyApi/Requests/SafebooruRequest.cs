using System.Text;

using CunnyApi.v1.External_APIs;

namespace CunnyApi.v1.Requests;

public class SafebooruRequest : BaseBooruRequest {
    public SafebooruRequest(string tags) {
        StringBuilder sb = new();
        sb.Append("https://safebooru.org/index.php?page=dapi&s=post&q=index&json=1");
        sb.Append($"&tags={tags.Replace(' ', '+')}");
        _constructedUrl = sb.ToString();
    }

    public bool TryGetJSON(int page, out IEnumerable<SafebooruApiData>? result) => InternalTryGetJSON($"{_constructedUrl}&pid={page}", out result);

    private readonly string _constructedUrl;
}