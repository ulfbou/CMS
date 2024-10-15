using Microsoft.AspNetCore.Components;

public class ImageModel : ContentModel
{
    public string? ImageUrl { get; set; }
    public int Height { get; set; } = 100; // Default height
    public int Width { get; set; } = 100; // Default width
    public int BorderRadius { get; set; } = 0; // Default border radius

    public string BackgroundColor { get; set; } = "#FFFFFF";
    public string BackgroundBorder { get; set; } = "#FFFFFF";
    public int BorderPix { get; set; } = 0;
    public int? Padding { get; set; } 

    public override MarkupString GetContent()
    {
        // Return as MarkupString
        return (MarkupString)$"<img src='{ImageUrl}' height='{Height}px' width='{Width}px' style='border-radius: {BorderRadius}px;' />";
    }
}
