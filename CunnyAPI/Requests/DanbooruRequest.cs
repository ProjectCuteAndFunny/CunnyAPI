using System.Text;

using CunnyApi.v1.External_APIs;

namespace CunnyApi.v1.Requests;

public class DanbooruRequest : BaseBooruRequest {
    public DanbooruRequest(string tags) {
        StringBuilder sb = new();
        sb.Append("https://danbooru.donmai.us/post/index.json?a");
        sb.Append("&tags=");
        sb.Append(tags.Replace(' ', '+'));
        _constructedUrl = sb.ToString();
    }

    public bool TryGetJSON(int page, out IEnumerable<DanbooruApiData>? result) => InternalTryGetJSON($"{_constructedUrl}&pid={page}", out result);

    private readonly string _constructedUrl;
}