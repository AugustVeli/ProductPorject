using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using SQLitePCL;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers;

public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;
        

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // "Product name", "Quantity", "Order amount" (quantity multiplied by the cost of the product), "Order status".
        var products = await _context.Orders.Select(o=> new
        {
            Id = o.Id,
            Client = o.Client.Name,
            Product_name = o.Product.Title,
            Quantity = o.Quantity,
            Order_amount = o.Quantity * o.Product.Price,
            Status = o.Status,
            
            
        }).ToListAsync();
        
        ViewBag.Products = products;
        
        return View();
    }
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Products = new SelectList(_context.Products, "Id", "Title");
        ViewBag.Clients = new SelectList(_context.Clients, "Id", "Name");
        return View();
    }
        
    [HttpPost]
    public async Task<IActionResult> Create([Bind("Quantity, Status, ProductId, ClientId")] Order order)
    {
        if (ModelState.IsValid)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        ViewBag.Products = new SelectList(_context.Products, "Id", "Title");
        ViewBag.Clients = new SelectList(_context.Clients, "Id", "Name");
        // ViewBag.Products = new SelectList(_context.Products.Select(p=>p.Id == order.ProductId), "Id", "Title");
        // ViewBag.Clients = new SelectList(_context.Clients.Select(p=>p.Id == order.ClientId), "Id", "Name");
        
        return View(order);
            
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        ViewBag.Products = new SelectList(_context.Products, "Id", "Title");
        ViewBag.Clients = new SelectList(_context.Clients, "Id", "Name");
        return View(order);
    }
    [HttpPost, ActionName("Edit")]
    public async Task<IActionResult> Edit(int id, [Bind("Id, Quantity, Status, ProductId, ClientId")] Order order)
    {
        if (ModelState.IsValid)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        ViewBag.Products = new SelectList(_context.Products, "Id", "Title");
        ViewBag.Clients = new SelectList(_context.Clients, "Id", "Name");
        
        return View(order);
    }
    
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        return View(order);
    }
    
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        } 
        return RedirectToAction("Index");
    }
}