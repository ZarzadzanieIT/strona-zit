using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZIT.Infrastructure.Authorization;
using ZIT.Web.Infrastructure;

namespace ZIT.Web.Controllers;

[Authorize]
[RequireEntitlement(Auth.Claim.Type, Auth.Entitlements.Panel)]
public class PanelController : Controller
{
    private readonly ILogger<PanelController> _logger;

    public PanelController(ILogger<PanelController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}