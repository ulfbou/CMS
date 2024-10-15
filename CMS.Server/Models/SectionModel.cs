using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace DragAndDropProject.Models
{
    public class SectionModel : ContentModel
    {
        public string? Title { get; set; }
        public List<ContentModel> InnerBoxes { get; set; } = new List<ContentModel>(); // Lista över boxar i containern
        private int? selectedInnerBoxId;
        public string BackgroundColor { get; set; } = "#FFFFFF";
        public string BackgroundBorder { get; set; } = "#FFFFFF";
        public int BorderPix { get; set; } = 0;
        public int? Height { get; set; } 
        

       
        
        

        
        public int? SelectedInnerBoxId
        {
            get => selectedInnerBoxId;
            set
            {
                selectedInnerBoxId = value;
                // Invoke a method or event to handle the box movement when selection changes
                if (selectedInnerBoxId.HasValue)
                {
                    // This is where you might call a method to move the box
                    // MoveBox(selectedInnerBoxId.Value); // Uncomment this if you implement a MoveBox method
                }
            }
        }

        public override MarkupString GetContent()
        {
            return (MarkupString)Title; // Returnera titeln för containern som MarkupString
        }
        
       
    }
}
