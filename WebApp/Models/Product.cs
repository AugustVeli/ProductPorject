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
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Product code must be alphanumeric")]
    public required string Code { get; set; }
    
    [Required]
    [MaxLength(30, ErrorMessage = "Title cannot be longer than 30 characters")]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Title code must be alphanumeric")]
    public required string Title { get; set; }
    
    [Required]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    [RegularExpression(@"^\d+?\.?\d{1,2}$", ErrorMessage = "Only two numbers after separator and separator is dot")]
    public required decimal Price { get; set; }
    
}