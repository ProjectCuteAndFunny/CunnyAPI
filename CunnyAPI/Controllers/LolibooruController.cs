﻿using CunnyApi.v1.External_APIs;
using CunnyApi.v1.Definitions;
using CunnyApi.v1.Requests;

using Microsoft.AspNetCore.Mvc;

namespace CunnyApi.v1.Controllers;

[Route("api/v1/lolibooru")]
[ApiController]
public class LolibooruController : ControllerBase {
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

    private async Task<IEnumerable<LolibooruApiData>> GetData(string tags, int size, int skip) {
        var request = new LolibooruRequest(tags);
        List<LolibooruApiData> data = new();

        for (int i = 0; data.Count < size + skip; i++) {
            if (!request.TryGetJSON(i, out var raw)) {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return data;
            }

            data.AddRange(raw!);
        }

        await Task.CompletedTask;

        return data.Skip(skip).Take(size);
    }
}
