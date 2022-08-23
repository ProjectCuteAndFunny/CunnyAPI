using CunnyApi.v1.External_APIs;
using CunnyApi.v1.Definitions;

using Microsoft.AspNetCore.Mvc;

using System.Text.Json;
using System.Text;

namespace CunnyApi.v1.Controllers;

[Route("api/v1/safebooru")]
[ApiController]
public class SafebooruController : ControllerBase {
    public SafebooruController(ILogger<SafebooruController> logger) {
        _logger = logger;
    }

    [HttpGet]
    [Route("{tags}/{size}")]
    public async Task<IEnumerable<CunnyApiData>> Get(string tags, int size) {
        var data = await GetData(tags, size, 0);
        return data.Select((elm) => new CunnyApiData {
            post_url = $"https://safebooru.org/index.php?page=post&s=view&id={elm.id}",
            image_url = $"https://safebooru.org/images/{elm.directory}/{elm.image}",
            tags = elm.tags.Split(' ', StringSplitOptions.RemoveEmptyEntries),
            owner_name = elm.owner,
            height = elm.height,
            width = elm.width,
            hash = elm.hash,
            id = elm.id
        });
    }
    [HttpGet]
    [Route("{tags}/{size};{skip}")]
    public async Task<IEnumerable<CunnyApiData>> Get(string tags, int size, int skip) {
        var data = await GetData(tags, size, skip);
        return data.Select((elm) => new CunnyApiData {
            post_url = $"https://safebooru.org/index.php?page=post&s=view&id={elm.id}",
            image_url = $"https://safebooru.org/images/{elm.directory}/{elm.image}",
            tags = elm.tags.Split(' ', StringSplitOptions.RemoveEmptyEntries),
            owner_name = elm.owner,
            height = elm.height,
            width = elm.width,
            hash = elm.hash,
            id = elm.id
        });
    }

    private static async Task<IEnumerable<SafebooruApiData>> GetData(string tags, int size, int skip) {
        string[] splitTags = tags.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        StringBuilder sb = new();
        if (tags.Length >= 1) {
            sb.Append("https://safebooru.org/index.php?page=dapi&s=post&q=index&json=1");
            sb.Append($"&tags={splitTags[0]}");
            Array.ForEach(splitTags[1..], (elm) => {
                sb.Append($"+{elm}");
            });
        }
        string baseQuery = sb.ToString();

        List<SafebooruApiData> data = new();

        for (int i = 0; data.Count < size + skip; i++)
        {
            string result = await _httpClient.GetStringAsync($"{baseQuery}&pid={i}");

            var raw = JsonSerializer.Deserialize<IEnumerable<SafebooruApiData>>(result);

            data.AddRange(raw!);
        }

        return data.Skip(skip).Take(size);
    }

    private static HttpClient _httpClient = new();
    private ILogger<SafebooruController> _logger;
}
