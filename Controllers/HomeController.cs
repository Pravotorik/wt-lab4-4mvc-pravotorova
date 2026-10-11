using Microsoft.AspNetCore.Mvc;

namespace wt_lab4_4mvc_pravotorova.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}