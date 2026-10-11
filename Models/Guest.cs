namespace wt_lab4_4mvc_pravotorova.Models;

public class Guest
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
}