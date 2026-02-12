using Microsoft.AspNetCore.Mvc;

namespace OpenExpenseApp.Views.Shared.Components;

/// <summary>
/// Reusable summary card component for dashboard
/// Usage: await Component.InvokeAsync("SummaryCard", new { title, value, iconClass, iconBgClass, subtitle })
/// </summary>
public class SummaryCardViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(
        string title,
        string value,
        string iconClass,
        string iconBgClass = "bg-primary bg-opacity-10",
        string? subtitle = null
    )
    {
        var model = new SummaryCardViewModel
        {
            Title = title,
            Value = value,
            IconClass = iconClass,
            IconBgClass = iconBgClass,
            Subtitle = subtitle,
        };

        return await Task.FromResult(View(model));
    }
}

public class SummaryCardViewModel
{
    public string Title { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string IconClass { get; set; } = string.Empty;
    public string IconBgClass { get; set; } = "bg-primary bg-opacity-10";
    public string? Subtitle { get; set; }
}
