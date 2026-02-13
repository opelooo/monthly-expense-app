using Microsoft.AspNetCore.Mvc;

namespace OpenExpenseApp.Views.Shared.Components;

/// <summary>
/// Reusable quick actions card component
/// Usage: await Component.InvokeAsync("QuickActions", new { actions })
/// </summary>
public class QuickActionsViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(
        IEnumerable<QuickActionItem> actions,
        bool isLoading = false
    )
    {
        if (isLoading)
        {
            return View("_Skeleton");
        }

        var model = new QuickActionsCardViewModel { Actions = actions };
        return await Task.FromResult(View(model));
    }
}

public class QuickActionsCardViewModel
{
    public IEnumerable<QuickActionItem> Actions { get; set; } = Enumerable.Empty<QuickActionItem>();
}

public class QuickActionItem
{
    public string Text { get; set; } = string.Empty;
    public string IconClass { get; set; } = string.Empty;
    public string Controller { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string? ReturnUrl { get; set; }
    public string CssClass { get; set; } = "btn-outline-secondary";
}
