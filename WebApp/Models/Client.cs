using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace  WebApp.Models;

public enum Gender
{
    Male,
    Female
}

public class Client
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public required string Name { get; set; }
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public required string Email { get; set; }
    [Required(ErrorMessage = "Birthdate is required")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]  
    public required DateTime Birthdate { get; set; }
    [Required(ErrorMessage = "Choose your gender")]
    public Gender? Gender { get; set; }

    public ICollection<Order> Orders { get; } = new List<Order>();

}