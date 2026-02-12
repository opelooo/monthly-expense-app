using Microsoft.AspNetCore.Mvc;

namespace OpenExpenseApp.Views.Shared.Components;

/// <summary>
/// Reusable progress card component with progress bars
/// Usage: await Component.InvokeAsync("ProgressCard", new { title, iconClass, items, emptyMessage })
/// </summary>
public class ProgressCardViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(
        string title,
        string iconClass,
        Dictionary<string, decimal> items,
        string emptyMessage = "No data available"
    )
    {
        var total = items.Values.Sum();
        var percentItems = items.ToDictionary(
            kvp => kvp.Key,
            kvp => total > 0 ? (decimal)(kvp.Value / total * 100) : 0
        );

        var model = new ProgressCardViewModel
        {
            Title = title,
            IconClass = iconClass,
            Items = items,
            PercentItems = percentItems,
            EmptyMessage = emptyMessage,
        };

        return await Task.FromResult(View(model));
    }
}

public class ProgressCardViewModel
{
    public string Title { get; set; } = string.Empty;
    public string IconClass { get; set; } = string.Empty;
    public Dictionary<string, decimal> Items { get; set; } = [];
    public Dictionary<string, decimal> PercentItems { get; set; } = [];
    public string EmptyMessage { get; set; } = "No data available";
}
