using System.Text.Json.Serialization;

namespace CunnyApi.v1.External_APIs;

public struct GelbooruPostApiData {
    public string has_children { get; set; }
    public string has_comments { get; set; }
    public string preview_url { get; set; }
    public int preview_height { get; set; }
    public string created_at { get; set; }
    public int preview_width { get; set; }
    public string sample_url { get; set; }
    public int sample_height { get; set; }
    public int sample_width { get; set; }
    public string has_notes { get; set; }
    public string directory { get; set; }
    public int post_locked { get; set; }
    public string file_url { get; set; }
    public int creator_id { get; set; }
    public string? source { get; set; }
    public string status { get; set; }
    public string rating { get; set; }
    public int parent_id { get; set; }
    public string image { get; set; }
    public string owner { get; set; }
    public string title { get; set; }
    public string tags { get; set; }
    public int height { get; set; }
    public string md5 { get; set; }
    public int change { get; set; }
    public int sample { get; set; }
    public int score { get; set; }
    public int width { get; set; }
    public int id { get; set; }
}

public struct GelbooruAttributesApiData
{
    public int limit { get; set; }
    public int offset { get; set; }
    public int count { get; set; }
}

public struct GelbooruApiData
{
    [JsonPropertyName("@attributes")]
    public GelbooruAttributesApiData Attributes { get; set; }
    public IEnumerable<GelbooruPostApiData> post { get; set; }
}