using System.Text.Json.Serialization;

namespace CunnyApi.v1.External_APIs;

public class SafebooruApiData {
    /// <summary>
    /// Example value: 1202
    /// </summary>
    public int sample_height { get; init; }
    /// <summary>
    /// Example value: 850
    /// </summary>
    public int sample_width { get; init; }
    /// <summary>
    /// Example value: "3908"
    /// </summary>
    public string directory { get; init; }
    /// <summary>
    /// Example value: 0
    /// </summary>
    public int parent_id { get; init; }
    /// <summary>
    /// Example value: "general"
    /// </summary>
    public string rating { get; init; }
    /// <summary>
    /// Example value: "danbooru"
    /// </summary>
    public string owner { get; init; }
    /// <summary>
    /// Example value: "02338c8b2339020a4d46ce4916766e1e0feb741f.jpg"
    /// </summary>
    public string image { get; init; }
    /// <summary>
    /// Example value: "e07e63aca458e2daa5c6f2e06a3eb65b"
    /// </summary>
    public string hash { get; init; }
    /// <summary>
    /// Example value: 4093
    /// </summary>
    public int height { get; init; }
    /// <summary>
    /// Example value: 1658323834
    /// </summary>
    public int change { get; init; }
    /// <summary>
    /// Example value: true
    /// </summary>
    public bool sample { get; init; }
    /// <summary>
    /// Example value: "1girl absurdres ahoge ailu_elf bangs bed bed_sheet black_skirt blue_archive blush commentary_request demon_girl demon_horns demon_wings forehead hair_ornament hairclip halo highres hina_(blue_archive) horns long_hair looking_at_viewer parted_bangs parted_lips pencil_skirt ponytail sidelocks simple_background sitting skirt sleeveless solo violet_eyes wariza white_hair wings zettai_ryouiki"
    /// </summary>
    public string tags { get; init; }
    /// <summary>
    /// Example value: 0
    /// </summary>
    public int? score { get; init; }
    /// <summary>
    /// Example value: 2894
    /// </summary>
    public int width { get; init; }
    /// <summary>
    /// Example value: 4084251
    /// </summary>
    public int id { get; init; }
}