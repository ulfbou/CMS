using CMS.Server.Components.Shared.UI;

using Microsoft.AspNetCore.Components;

namespace CMS.Server.Components;

public partial class BoxList : ComponentBase
{
    private ConfigurationDialog Configuration { get; set; }

    private RenderFragment ConfigurationInput { get; set; }
    private EventCallback OnConfigurationConfirm { get; set; }
    private EventCallback OnConfigurationCancel { get; set; }
}
