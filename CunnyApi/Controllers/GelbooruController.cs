using CunnyApi.v1.External_APIs;
using CunnyApi.v1.Definitions;

using Microsoft.AspNetCore.Mvc;

using System.Text.Json;
using System.Text;

namespace CunnyApi.v1.Controllers;

[Route("api/v1/gelbooru")]
[ApiController]
public class GelbooruController : ControllerBase {
    public GelbooruController(ILogger<GelbooruController> logger) {
        _logger = logger;
    }

    [HttpGet]
    [Route("{tags}/{size}")]
    public async Task<IEnumerable<CunnyApiData>> Get(string tags, int size) {
        var data = await GetData(tags, size, 0);
        return data.Select((elm) => new CunnyApiData {
            post_url = $"https://gelbooru.com/index.php?page=post&s=view&id={elm.id}",
            image_url = $"https://gelbooru.com/images/{elm.directory}/{elm.image}",
            tags = elm.tags.Split(' ', StringSplitOptions.RemoveEmptyEntries),
            owner_name = elm.owner,
            height = elm.height,
            width = elm.width,
            hash = elm.md5,
            id = elm.id
        });
    }
    [HttpGet]
    [Route("{tags}/{size};{skip}")]
    public async Task<IEnumerable<CunnyApiData>> Get(string tags, int size, int skip) {
        var data = await GetData(tags, size, skip);
        return data.Select((elm) => new CunnyApiData {
            post_url = $"https://gelbooru.com/index.php?page=post&s=view&id={elm.id}",
            image_url = $"https://gelbooru.com/images/{elm.directory}/{elm.image}",
            tags = elm.tags.Split(' ', StringSplitOptions.RemoveEmptyEntries),
            owner_name = elm.owner,
            height = elm.height,
            width = elm.width,
            hash = elm.md5,
            id = elm.id
        });
    }

    private static async Task<IEnumerable<GelbooruPostApiData>> GetData(string tags, int size, int skip) {
        string[] splitTags = tags.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        StringBuilder sb = new();
        if (tags.Length >= 1) {
            sb.Append("https://gelbooru.com/index.php?page=dapi&s=post&q=index&json=1");
            // Gelbooru needs an API key
            //sb.Append("&api_key=anonymous&user_id=9455");
            sb.Append($"&tags={splitTags[0]}");
            Array.ForEach(splitTags[1..], (elm) => {
                sb.Append($"+{elm}");
            });
        }
        string baseQuery = sb.ToString();

        List<GelbooruPostApiData> data = new();

        for (int i = 0; data.Count < size + skip; i++)
        {
            string result = await _httpClient.GetStringAsync($"{baseQuery}&pid={i}");

            var raw = JsonSerializer.Deserialize<GelbooruApiData>(result);

            data.AddRange(raw!.post);
        }

        return data.Skip(skip).Take(size);
    }

    private static HttpClient _httpClient = new();
    private ILogger<GelbooruController> _logger;
}
