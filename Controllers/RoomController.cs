using Microsoft.AspNetCore.Mvc;
using wt_lab4_4mvc_pravotorova.Models;

namespace wt_lab4_4mvc_pravotorova.Controllers;

public class RoomController : Controller
{
    // "база данных" в памяти
    public static readonly List<Room> Rooms = new()
    {
        new Room {
            Id = 1, Number = "101", Category = "Стандарт", Capacity = 2,
            PricePerNight = 3500, IsAvailable = true,
            ImageUrl = "https://images.unsplash.com/photo-1611892440504-42a792e24d32?w=800",
            Description = "Уютный номер с видом на сад, идеален для короткого отдыха.",
            AmenityIds = new List<int> { 1, 2 }
        },
        new Room {
            Id = 2, Number = "102", Category = "Стандарт", Capacity = 2,
            PricePerNight = 3700, IsAvailable = true,
            ImageUrl = "https://images.unsplash.com/photo-1631049307264-da0ec9d70304?w=800",
            Description = "Светлый номер с панорамным окном и мягкой кроватью.",
            AmenityIds = new List<int> { 1, 2 }
        },
        new Room {
            Id = 3, Number = "201", Category = "Комфорт", Capacity = 2,
            PricePerNight = 5200, IsAvailable = true,
            ImageUrl = "https://images.unsplash.com/photo-1590490360182-c33d57733427?w=800",
            Description = "Просторный номер с рабочей зоной и мини-баром.",
            AmenityIds = new List<int> { 1, 2, 3 }
        },
        new Room {
            Id = 4, Number = "202", Category = "Комфорт", Capacity = 3,
            PricePerNight = 6100, IsAvailable = false,
            ImageUrl = "https://images.unsplash.com/photo-1566665797739-1674de7a421a?w=800",
            Description = "Семейный номер с дополнительным местом для ребёнка.",
            AmenityIds = new List<int> { 1, 2, 3 }
        },
        new Room {
            Id = 5, Number = "301", Category = "Люкс", Capacity = 3,
            PricePerNight = 8900, IsAvailable = true,
            ImageUrl = "https://images.unsplash.com/photo-1582719508461-905c673771fd?w=800",
            Description = "Люкс с гостиной зоной и ванной с джакузи.",
            AmenityIds = new List<int> { 1, 2, 3, 4 }
        },
        new Room {
            Id = 6, Number = "401", Category = "Апартаменты", Capacity = 4,
            PricePerNight = 15000, IsAvailable = false,
            ImageUrl = "https://images.unsplash.com/photo-1591088398332-8a7791972843?w=800",
            Description = "Двухуровневые апартаменты с террасой и кухней.",
            AmenityIds = new List<int> { 1, 2, 3, 4, 5 }
        }
    };

    public static readonly List<Amenity> Amenities = new()
    {
        new Amenity { Id = 1, Name = "Wi-Fi", Icon = "wifi" },
        new Amenity { Id = 2, Name = "Кондиционер", Icon = "snow" },
        new Amenity { Id = 3, Name = "Мини-бар", Icon = "cup" },
        new Amenity { Id = 4, Name = "Джакузи", Icon = "droplet" },
        new Amenity { Id = 5, Name = "Кухня", Icon = "home" }
    };

    public IActionResult Index(string? search)
    {
        var rooms = Rooms.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            rooms = rooms.Where(r =>
                r.Number.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                r.Category.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        ViewBag.Search = search;
        ViewBag.Amenities = Amenities;
        return View(rooms.ToList());
    }

    public IActionResult Details(int id)
    {
        var room = Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null) return NotFound();

        ViewBag.Amenities = Amenities.Where(a => room.AmenityIds.Contains(a.Id)).ToList();
        return View(room);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Room room)
    {
        if (!ModelState.IsValid) return View(room);

        room.Id = Rooms.Any() ? Rooms.Max(r => r.Id) + 1 : 1;
        if (string.IsNullOrWhiteSpace(room.ImageUrl))
            room.ImageUrl = "https://images.unsplash.com/photo-1566073771259-6a8506099945?w=800";

        Rooms.Add(room);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var room = Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null) return NotFound();
        return View(room);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Room room)
    {
        if (!ModelState.IsValid) return View(room);

        var existing = Rooms.FirstOrDefault(r => r.Id == id);
        if (existing == null) return NotFound();

        existing.Number = room.Number;
        existing.Category = room.Category;
        existing.Capacity = room.Capacity;
        existing.PricePerNight = room.PricePerNight;
        existing.IsAvailable = room.IsAvailable;
        existing.Description = room.Description;
        if (!string.IsNullOrWhiteSpace(room.ImageUrl))
            existing.ImageUrl = room.ImageUrl;

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var room = Rooms.FirstOrDefault(r => r.Id == id);
        if (room != null) Rooms.Remove(room);
        return RedirectToAction(nameof(Index));
    }
}