namespace CunnyApi.v1.Definitions;

public class CunnyApiData {
    public IEnumerable<string> tags { get; init; }
    public string owner_name { get; init; }
    public string image_url { get; init; }
    public string post_url { get; set; }
    public string hash { get; init; }
    public uint height { get; init; }
    public uint width { get; init; }
    public uint id { get; init; }
}
