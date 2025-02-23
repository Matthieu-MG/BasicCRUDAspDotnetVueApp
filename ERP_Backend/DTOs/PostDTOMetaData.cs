namespace Enterprise.API.Responses;

public class PostDTOMetaData
{
    public string Name { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string Label { get; set; } = default!;
    public string? Route { get; set; }
}