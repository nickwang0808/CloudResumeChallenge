using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.Cosmos;

namespace Main.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly CosmosClient _dbClient;

    public IndexModel(ILogger<IndexModel> logger, CosmosClient dbClient)
    {
        _logger = logger;
        _dbClient = dbClient;
    }

    public void OnGetAsync()
    {
        var dbId = _dbClient.GetDatabase("nick");
        Console.WriteLine("dbId: " + dbId.Id);
    }

}
