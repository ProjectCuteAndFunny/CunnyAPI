using CunnyApi.v1.External_APIs;
using CunnyApi.v1.Definitions;

using Microsoft.AspNetCore.Mvc;

using System.Text.Json;
using System.Text;

namespace CunnyApi.v1.Controllers;

[Route("api/v1/lolibooru")]
[ApiController]
public class LolibooruController : ControllerBase {
    public LolibooruController(ILogger<LolibooruController> logger) {
        _logger = logger;
    }

    [HttpGet]
    [Route("{tags}/{size}")]
    public async Task<IEnumerable<CunnyApiData>> Get(string tags, int size) {
        var data = await GetData(tags, size, 0);
        return data.Select((elm) => new CunnyApiData {
            post_url = $"https://lolibooru.moe/post/show/{elm.id}/{elm.tags.Replace(' ', '-')}",
            tags = elm.tags.Split(' '),
            image_url = elm.file_url,
            owner_name = elm.author,
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
            post_url = $"https://lolibooru.moe/post/show/{elm.id}/{elm.tags.Replace(' ', '-')}",
            tags = elm.tags.Split(' '),
            image_url = elm.file_url,
            owner_name = elm.author,
            height = elm.height,
            width = elm.width,
            hash = elm.md5,
            id = elm.id
        });
    }

    private static async Task<IEnumerable<LoliBooruApiData>> GetData(string tags, int size, int skip) {
        if (Cache.Item1?.Count() >= size + skip && DateTime.UtcNow.Subtract(Cache.Item2) < TimeSpan.FromHours(1)) return Cache.Item1.Skip(skip).Take(size);
        
        string[] splitTags = tags.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        StringBuilder sb = new();
        if (tags.Length >= 1) {
            // ?a is needed, else we get a 404
            sb.Append("https://lolibooru.moe/post/index.json?a");
            sb.Append($"&tags={splitTags[0]}");
            Array.ForEach(splitTags[1..], (elm) => {
                sb.Append($"+{elm}");
            });
        }
        string baseQuery = sb.ToString();

        List<LoliBooruApiData> data = new();

        for (int i = 0; data.Count < size + skip; i++)
        {
            var result = await _httpClient.GetStreamAsync($"{baseQuery}&page={i}");

            var raw = JsonSerializer.Deserialize<IEnumerable<LoliBooruApiData>>(result);

            data.AddRange(raw!);
        }

        Cache.Item1 = data;
        Cache.Item2 = DateTime.UtcNow;

        return Cache.Item1.Skip(skip).Take(size);
    }

    private static (IEnumerable<LoliBooruApiData>, DateTime) Cache = new();
    private static HttpClient _httpClient = new();
    private ILogger<LolibooruController> _logger;
}
