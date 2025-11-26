using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;


namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var clients = await _context.Clients.Select(client => new
                {
                    Id = client.Id,
                    Name = client.Name,
                    Email = client.Email,
                    Birthdate = client.Birthdate,
                    Gender = client.Gender,
                    Number_of_orders = client.Orders.Count(),
                    Average_order_amount = client.Orders.Count() == 0 ? 0 : client.Orders.Average(order => order.Quantity)
                }
                    
                ).ToListAsync();
            
            ViewBag.ClientsData = clients; 
            
            return View();
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Email, Birthdate, Gender")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
            return View(client);
            
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            client.Birthdate = client.Birthdate.Date;
            return View(client);
        }
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(int id, [Bind("Name, Email, Birthdate, Gender")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Clients.Update(client);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(client);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);
            return View(client);
        }
        
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            // var orders = await _context.Orders.FindAsync(id);
            var orders = _context.Orders.Where(o => o.ClientId  == id);

            if (client != null)
            {
                // client.Orders.Clear();
                foreach (var order in orders)
                {
                    client.Orders.Remove(order);
                }
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            } 
            return RedirectToAction("Index");
        }
    }
}
