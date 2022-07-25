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
        return data.Select((elm) => elm.post.Select(post => new CunnyApiData {
            post_url = $"https://gelbooru.com/index.php?page=post&s=view&id={post.id}",
            image_url = $"https://gelbooru.com/images/{post.directory}/{post.image}",
            tags = post.tags.Split(' ', StringSplitOptions.RemoveEmptyEntries),
            owner_name = post.owner,
            height = post.height,
            width = post.width,
            hash = post.md5,
            id = post.id
        })).SelectMany(x => x);
    }
    [HttpGet]
    [Route("{tags}/{size};{skip}")]
    public async Task<IEnumerable<CunnyApiData>> Get(string tags, int size, int skip) {
        var data = await GetData(tags, size, skip);
        return data.Select((elm) => elm.post.Select(post => new CunnyApiData {
            post_url = $"https://gelbooru.com/index.php?page=post&s=view&id={post.id}",
            image_url = $"https://gelbooru.com/images/{post.directory}/{post.image}",
            tags = post.tags.Split(' ', StringSplitOptions.RemoveEmptyEntries),
            owner_name = post.owner,
            height = post.height,
            width = post.width,
            hash = post.md5,
            id = post.id
        })).SelectMany(x => x);
    }

    private static async Task<IEnumerable<GelbooruApiData>> GetData(string tags, int size, int skip) {
        if (DateTime.UtcNow.Subtract(Cache.Item2) < TimeSpan.FromHours(1) && Cache.Item1.Count() >= size + skip) return Cache.Item1.Skip(skip).Take(size);
        
        string[] splitTags = tags.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        StringBuilder sb = new();
        if (tags.Length >= 1) {
            sb.Append("https://gelbooru.com/index.php?page=dapi&s=post&q=index&json=1");
            // Gelbooru needs an API key
            sb.Append("&api_key=anonymous&user_id=9455");
            sb.Append($"&tags={splitTags[0]}");
            Array.ForEach(splitTags[1..], (elm) => {
                sb.Append($"+{elm}");
            });
        }
        string baseQuery = sb.ToString();

        List<GelbooruApiData> data = new();

        for (int i = 0; data.Count * 100 < size + skip; i++)
        {
            string result = await _httpClient.GetStringAsync($"{baseQuery}&pid={i}");

            var raw = JsonSerializer.Deserialize<GelbooruApiData>(result);

            data.Add(raw!);
        }

        Cache.Item1 = data;
        Cache.Item2 = DateTime.UtcNow;

        return Cache.Item1.Skip(skip).Take(size);
    }

    private static (IEnumerable<GelbooruApiData>, DateTime) Cache = new();
    private static HttpClient _httpClient = new();
    private ILogger<GelbooruController> _logger;
}
