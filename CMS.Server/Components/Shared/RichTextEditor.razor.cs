using CMS.Server.Components.Shared.UI;

using Markdig;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using PSC.Blazor.Components.MarkdownEditor.EventsArgs;

namespace CMS.Server.Components.Shared;

public partial class RichTextEditor : ComponentBase
{
    [Parameter] public string Content { get; set; } = string.Empty;
    [Parameter] public EventCallback<string> ContentChanged { get; set; }
    [Parameter] public string Placeholder { get; set; }
    [Parameter] public string EditorTheme { get; set; } = "light"; // Options: "light", "dark"
    [Parameter] required public string Markdown { get; set; }
    [Parameter] public EventCallback<string> OnSave { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public bool ShowPreview { get; set; } = true;
    [Parameter] public bool ShowToolbar { get; set; } = true;
    [Parameter] public bool EnableSyntaxHighlighting { get; set; } = true;
    [Parameter] public bool EnableUndoRedo { get; set; } = true;
    [Parameter] public bool EnableImageUpload { get; set; } = true;
    [Parameter] public bool EnableCodeBlockLanguageSelection { get; set; } = true;
    [Parameter] public string Theme { get; set; } = "default";
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private ConfirmationDialog Confirmation = new ConfirmationDialog();

    private LinkInput LinkModel { get; set; }
    private string ImageUrl = string.Empty;
    private string ImageText = string.Empty;

    private ConfirmationDialog? ImageConfirmation { get; set; }
    private ConfirmationDialog? LinkConfirmation { get; set; }

    private EventCallback OnConfirmInsert { get; set; }
    private EventCallback OnCancelInsert { get; set; }

    private List<string> _undoStack = new List<string>();
    private List<string> _redoStack = new List<string>();
    private int _undoIndex = -1;

    private bool _showEditor = true;

    protected override void OnInitialized()
    {
        Markdown = Placeholder ?? "Placeholder text...";
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        if (firstRender)
        {
            Show();
        }
    }

    private Task OnMarkdownValueChanged(string value)
    {
        Content = value;
        return Task.CompletedTask;
    }

    private Task OnCustomButtonClicked(MarkdownButtonEventArgs eventArgs)
    {
        Console.WriteLine("OnCustomButtonClicked -> " + eventArgs.Value);
        Content += "OnCustomButtonClicked -> " + eventArgs.Value + "<br />";

        return Task.CompletedTask;
    }

    private async Task SaveAsync()
    {
        await OnSave.InvokeAsync(Content);
        Confirmation.Hide();
    }

    private void Cancel()
    {
        Confirmation.Hide();
    }

    // Insert elements with ConfirmationDialog. Track which confirmation dialog is open, so that it is closed accordingly. 
    private void InsertLink()
    {
        OnConfirmInsert = EventCallback.Factory.Create(this, InsertLinkMarkdown);
        OnCancelInsert = EventCallback.Factory.Create(this, () => Cancel(LinkConfirmation));
        LinkConfirmation?.Show();
    }

    private void InsertImage()
    {
        OnConfirmInsert = EventCallback.Factory.Create(this, InsertImageMarkdown);
        OnCancelInsert = EventCallback.Factory.Create(this, () => Cancel(ImageConfirmation));
        ImageConfirmation?.Show();
    }

    private Task InsertImageMarkdown(object input)
    {
        if (input is not ImageInput imageInput)
        {
            throw new InvalidOperationException($"Expected input to be of type ImageInput, but found {input.GetType().Name}.");
        }

        Content += $"\n![{imageInput.AltText}]({imageInput.Url})\n";
        ImageConfirmation?.Hide();
        return Task.CompletedTask;
    }

    private Task InsertLinkMarkdown(object input)
    {
        if (input is not LinkInput linkInput)
        {
            throw new InvalidOperationException($"Expected input to be of type LinkInput, but found {input.GetType().Name}.");
        }

        Content += $"\n[{linkInput.Text}]({linkInput.Url})\n";
        ImageConfirmation?.Hide();
        return Task.CompletedTask;
    }

    private Task Save()
    {
        // Save the content to the parent component
        ContentChanged.InvokeAsync(Content);
        return Task.CompletedTask;
    }

    private void Cancel(ConfirmationDialog? dialog)
    {
        dialog?.Hide();
    }

    private string RenderMarkdown(string markdown)
    {
        // Implement your Markdown rendering logic here, using a Markdown parser library like Markdig or CommonMark.
        // Consider using a debouncing or throttling technique to improve performance for large documents.
        var pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .UseBootstrap()
            .UseAutoIdentifiers()
            .UseAutoLinks()
            .UseAbbreviations()
            .UseCitations()
            .UseCustomContainers()
            .UseDefinitionLists()
            .UseEmphasisExtras()
            .UseFigures()
            .UseFooters()
            .UseFootnotes()
            .UseMediaLinks()
            .UsePipeTables()
            .UseListExtras()
            .UseTaskLists()
            .UseDiagrams()
            .UseGenericAttributes()
            .UseGridTables()
            .UseMathematics()
            .UseSoftlineBreakAsHardlineBreak()
            .UseSmartyPants()
            .Build();

        var html = Markdig.Markdown.ToHtml(markdown, pipeline);
        return html;
    }

    private void SaveContent()
    {
        OnSave.InvokeAsync(Content);
        Hide();
    }

    private void CancelEditing()
    {
        OnCancel.InvokeAsync(null);
        Hide();
    }

    public void Show()
    {
        _showEditor = true;
        Confirmation.Show();
    }

    public void Hide()
    {
        _showEditor = false;
        Confirmation.Hide();
    }

    public void ToggleVisibility()
    {
        _showEditor = !_showEditor;
        if (_showEditor)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private Task HandleLinkClick()
    {
        return Task.CompletedTask;
    }

    private Task HandleImageClick()
    {
        return Task.CompletedTask;
    }

    private Task HandleVideoClick()
    {
        return Task.CompletedTask;
    }


}

public sealed class ImageInput
{
    public string Url { get; set; } = string.Empty;
    public string AltText { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}
public sealed class LinkInput
{
    public string Url { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}