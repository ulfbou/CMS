using CMS.Server.Components.Shared;
using CMS.Server.Models;

using Microsoft.AspNetCore.Components;

namespace CMS.Server.Components
{
    public partial class ConfigureTextDialog : ComponentBase
    {
        [Parameter] public TextModel Content { get; set; }
        [Parameter] public EventCallback OnClose { get; set; }
        private RichTextEditor Editor { get; set; }

        private EventCallback HandleValidSubmit { get; set; }
    }
}
