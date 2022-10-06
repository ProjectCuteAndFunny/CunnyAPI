namespace CunnyApi.v1.Definitions;

public class CunnyApiData {
    public IEnumerable<string> tags { get; init; }
    public string owner_name { get; init; }
    public string image_url { get; init; }
    public string post_url { get; set; }
    public string hash { get; init; }
    public int height { get; init; }
    public int width { get; init; }
    public int id { get; init; }
}
