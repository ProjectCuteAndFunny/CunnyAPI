# Rules

- Restrict API calls to one per minute (max)
- Do not query massive amounts of data at once

# Usage

`<deployment>/api/<version>/<booru>/<tags>/<count>`

| variable   | explination                                                                                                                                             | example                                                                       |
|------------|---------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------|
| deployment | must be a valid URL or IP address                                                                                                                       | `https://cunnyapi.breadwas.uber.space`                                        |
| version    | depends on the deployed version. version may be deprecated if security of the API is affected                                                           | `v1`                                                                          |
| booru      | one of: safebooru, gelbooru, lolibooru, yandere, konachan, danbooru                                                                                     | `safebooru`                                                                   |
| tags       | accepts a space-separated list of tags, or just one tag                                                                                                 | `blue_archive hibiki_(cheerleader)_(blue_archive)`                            |
| count      | an integer which specifies how many images you want (if there are not enough images, the api will return less results than requested)                   | `5`                                                                           |
| skip       | an integer which tells the API how many results to skip before the first element of the returned list. to use skip, append `;<your integer>` to the URL | `;3`                                                                          |

The example URL would look like this: `https://cunnyapi.breadwas.uber.space/api/v1/safebooru/blue_archive hibiki_(cheerleader)_(blue_archive)/5;3`

# JSON

The API returns the following JSON:

```
[
    {
        "tags": array of strings,
        "owner_name": string,
        "image_url": string,
        "post_url": string,
        "hash": string,
        "height": integer,
        "width": integer,
        "id": integer
    },
    { ... },
    { ... },
    { ... },
    ...
]
```