using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index() 
        {
            // var products = await _context.Products.ToListAsync();
            List<Product> products = await _context.Products.Select(p=> new Product
            {
                Id = p.Id,
                Code = p.Code,
                Title = p.Title,
                Price = p.Price,
            }).ToListAsync();
            
            return View(products);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Code, Title, Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(product);
            
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return View(product);
        }
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Code, Title, Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
                var orders = _context.Orders.Where(o => o.ProductId  == id);
                return View(product);
        }
        
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            } 
            return RedirectToAction("Index");
        }
        
    }
}
