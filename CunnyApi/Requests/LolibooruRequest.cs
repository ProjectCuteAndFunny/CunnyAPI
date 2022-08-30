using System.Text;

using CunnyApi.v1.External_APIs;

namespace CunnyApi.v1.Requests;

public class LolibooruRequest : BaseBooruRequest {
    public LolibooruRequest(string tags) {
        StringBuilder sb = new();
        sb.Append("https://lolibooru.moe/post/index.json?a");
        sb.Append($"&tags={tags.Replace(' ', '+')}");
        _constructedUrl = sb.ToString();
    }

    public bool TryGetJSON(int page, out IEnumerable<LolibooruApiData>? result) => InternalTryGetJSON($"{_constructedUrl}&page={page}", out result);

    private readonly string _constructedUrl;
}