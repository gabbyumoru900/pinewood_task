using Microsoft.AspNetCore.Mvc;
using pinewood_crud_app; // Add your namespace here if it's different
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pinewood_crud_app.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SQLitePCL;

namespace pinewood_crud_app.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.ToListAsync();
            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong {ex.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, $"Something went wrong invalid model");
            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var exist = await _context.Customers.Where(x => x.Id == id).FirstOrDefaultAsync();
            return View(exist);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exist = _context.Customers.Where(x => x.Id == customer.Id).FirstOrDefault();
                    if (exist != null)
                    {
                        exist.Name = customer.Name;
                        exist.Email = customer.Email;
                        exist.Address = customer.Address;
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong {ex.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, $"Something went wrong invalid model");
            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var exist = await _context.Customers.Where(x => x.Id == id).FirstOrDefaultAsync();
            return View(exist);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exist = _context.Customers.Where(x => x.Id == customer.Id).FirstOrDefault();
                    if (exist != null)
                    {
                       _context.Remove(exist);
                       await _context.SaveChangesAsync();
                       return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong {ex.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, $"Something went wrong invalid model");
            return View(customer);
        }
    }
}
