using System.Diagnostics;
using AccountingApp.Interfaces;
using AccountingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountingApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserExpenseRepository _userExpenseRepository;

    public HomeController(
        ILogger<HomeController> logger,
        IUserExpenseRepository userExpenseRepository
    )
    {
        _logger = logger;
        _userExpenseRepository = userExpenseRepository;
    }

    public async Task<IActionResult> Index()
    {
        if (HttpContext.Session.GetString("UserId") == null)
        {
            return RedirectToAction("Index", "Authentication");
        }

        var userId = HttpContext.Session.GetString("UserId")!;
        var today = DateTime.Today;
        var startOfMonth = new DateTime(today.Year, today.Month, 1);
        var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

        // Get user's expenses
        var userExpenses = await _userExpenseRepository.GetByUserIdAsync(userId);
        var userExpenseList = userExpenses.ToList();
        var expenses = userExpenseList.Select(ue => ue.Expense).OfType<Expense>().ToList();

        var todayExpenses = expenses.Where(e => e.ExpenseDate.Date == today.Date);
        var monthExpenses = expenses.Where(e =>
            e.ExpenseDate.Date >= startOfMonth && e.ExpenseDate.Date <= endOfMonth
        );

        var totalExpenses = expenses.Sum(e => e.Amount);
        var thisMonthExpenses = monthExpenses.Sum(e => e.Amount);
        var daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
        var avgPerDay = daysInMonth > 0 ? thisMonthExpenses / daysInMonth : 0;

        // Calculate percentage for each payment method
        var expensesByPaymentMethod = expenses
            .GroupBy(e => e.PaymentMethod)
            .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));
        var expensesByPaymentMethodPercent = expensesByPaymentMethod.ToDictionary(
            kvp => kvp.Key,
            kvp => totalExpenses > 0 ? (double)(kvp.Value / totalExpenses * 100) : 0
        );

        var dashboardViewModel = new DashboardViewModel
        {
            TotalExpenses = totalExpenses,
            TodayExpenses = todayExpenses.Sum(e => e.Amount),
            ThisMonthExpenses = thisMonthExpenses,
            AvgPerDay = avgPerDay,
            TotalExpenseCount = expenses.Count(),
            RecentExpenses = expenses.OrderByDescending(e => e.ExpenseDate).Take(10),
            ExpensesByPaymentMethod = expensesByPaymentMethod,
            ExpensesByPaymentMethodPercent = expensesByPaymentMethodPercent,
        };

        return View(dashboardViewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
