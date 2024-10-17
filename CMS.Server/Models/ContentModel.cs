using Microsoft.AspNetCore.Components;

namespace CMS.Server.Models;
public abstract class ContentModel
{
    public int Id { get; set; }
    public int? SelectedContainerId { get; set; }
    public bool IsEditing { get; set; } = false;
    public bool ShowEditButton { get; set; }
    public abstract MarkupString GetContent();

    public virtual ContentModel Clone(int col, int row)
    {
        var clone = (ContentModel)MemberwiseClone();
        clone.Column = col;
        clone.Row = row;
        return clone;
    }

    public string TextAlign { get; set; } = "left"; // Default alignment


    public int Row { get; set; } = 1; // Default row
    public int Column { get; set; } = 1; // Default column
}
