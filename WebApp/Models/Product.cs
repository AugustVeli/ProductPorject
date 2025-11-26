using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace  WebApp.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(10, ErrorMessage = "Product code cannot be longer than 10 characters")]
    public required string Code { get; set; }
    [Required]
    public required string Title { get; set; }
    [Required]
    [DataType(DataType.Currency)]
    // [RegularExpression(@"\.", 
    //     ErrorMessage = "Use only comma")]
    public required decimal Price { get; set; }
    
}