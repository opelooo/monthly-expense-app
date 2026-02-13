using Microsoft.AspNetCore.Mvc;

namespace OpenExpenseApp.Views.Shared.Components;

/// <summary>
/// Reusable data card component with table
/// Usage: await Component.InvokeAsync("DataCard", new { title, iconClass, items, emptyMessage })
/// </summary>
public class DataCardViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(
        string title,
        string iconClass,
        IEnumerable<dynamic> items,
        string emptyMessage = "No data available",
        bool isLoading = false
    )
    {
        if (isLoading)
        {
            return View("_Skeleton");
        }

        var model = new DataCardViewModel
        {
            Title = title,
            IconClass = iconClass,
            Items = items,
            EmptyMessage = emptyMessage,
        };

        return await Task.FromResult(View(model));
    }
}

public class DataCardViewModel
{
    public string Title { get; set; } = string.Empty;
    public string IconClass { get; set; } = string.Empty;
    public IEnumerable<dynamic> Items { get; set; } = Enumerable.Empty<dynamic>();
    public string EmptyMessage { get; set; } = "No data available";
}
