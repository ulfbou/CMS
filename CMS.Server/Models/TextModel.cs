using Microsoft.AspNetCore.Components;

public class TextModel : ContentModel
{
    public string? Text { get; set; } 
    public bool IsBold { get; set; }
    public bool IsItalic { get; set; }
   
    public int BorderRadius { get; set; } = 0; // Default border radius
    public string BackgroundColor { get; set; } = "#FFFFFF";
    public string BackgroundBorder { get; set; } = "#FFFFFF";
    public int BorderPix { get; set; } = 0;
    public int? Padding { get; set; } 
    public string Color { get; set; } ="#000000";
    public int? FontSize { get; set; } 

    public override MarkupString GetContent()
    {
        // Return as MarkupString
        var fontWeight = IsBold ? "bold" : "normal";
        var fontStyle = IsItalic ? "italic" : "normal";
        return (MarkupString)$"<span style='font-weight: {fontWeight}; font-style: {fontStyle};'>{Text}</span>";
    }
}
