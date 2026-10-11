using Microsoft.AspNetCore.Mvc;
using wt_lab4_4mvc_pravotorova.Models;

namespace wt_lab4_4mvc_pravotorova.Controllers;

public class GuestController : Controller
{
    public static readonly List<Guest> Guests = new()
    {
        new Guest { Id = 1, FullName = "Анна Смирнова", Email = "anna@mail.com", Phone = "+375 29 111-22-33", Country = "Беларусь", AvatarUrl = "https://i.pravatar.cc/150?img=47" },
        new Guest { Id = 2, FullName = "Иван Петров", Email = "ivan@mail.com", Phone = "+375 29 444-55-66", Country = "Россия", AvatarUrl = "https://i.pravatar.cc/150?img=12" },
        new Guest { Id = 3, FullName = "Мария Ковалёва", Email = "maria@mail.com", Phone = "+375 29 777-88-99", Country = "Польша", AvatarUrl = "https://i.pravatar.cc/150?img=32" }
    };

    public IActionResult Index() => View(Guests);
}