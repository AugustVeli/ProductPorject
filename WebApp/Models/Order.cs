using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace  WebApp.Models;

public enum Status
{
    Created, 
    Paid, 
    Delivered
}

public class Order
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Choose your Client")]
    public int ClientId { get; set; }
    
    [ForeignKey("ClientId")]
    public Client? Client { get; set; }
    [Required(ErrorMessage = "Choose your Product")]
    public int ProductId { get; set; }
    
    [ForeignKey("ProductId")]
    public Product? Product { get; set; } 
    
    [Required(ErrorMessage = "Choose Quantity")]
    [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public int Quantity { get; set; }
    
    [Required(ErrorMessage = "Choose Status")]
    public Status Status {get; set; }
}