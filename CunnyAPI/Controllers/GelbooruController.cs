﻿using CunnyApi.v1.External_APIs;
using CunnyApi.v1.Definitions;
using CunnyApi.v1.Requests;

using Microsoft.AspNetCore.Mvc;

namespace CunnyApi.v1.Controllers;

[Route("api/v1/gelbooru")]
[ApiController]
public class GelbooruController : ControllerBase {
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
        var request = new GelbooruRequest(tags);
        List<GelbooruPostApiData> data = new();

        for (int i = 0; data.Count < size + skip; i++) {
            if (!request.TryGetJSON(i, out var raw)) {
                return data;
            }

            // Gelbooru always responds, but sometimes with an empty collection.
            if (raw?.post is null) {
                return data;
            }

            data.AddRange(raw?.post!);
        }

        await Task.CompletedTask;

        return data.Skip(skip).Take(size);
    }
}
