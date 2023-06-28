using Main.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Main.Pages;

public class IndexModel : PageModel
{
    private readonly PostgresContext _db;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger, PostgresContext db)
    {
        _logger = logger;
        _db = db;
    }

    public int CounterCount { get; set; } // Property to hold the count value

    public async Task OnGetAsync()
    {
        await _db.Database.ExecuteSqlRawAsync("CALL public.IncrementCounter({0})", 1);

        var counter = await _db.Counters.FirstOrDefaultAsync();
        if (counter != null) CounterCount = counter.Count;
    }
}
