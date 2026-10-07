using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MIRANDA_Midterm_Store.Data;
using MIRANDA_Midterm_Store.Models;

namespace MIRANDA_Midterm_Store.Controllers;

public class CartController : Controller
{
    private readonly ApplicationDbContext _db;

    public CartController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var cart = _db.CartItems
            .Include(c => c.Product)
            .ToList();

        return View(cart);
    }

    public IActionResult Add(int id)
    {
        var product = _db.Products.Find(id);

        if (product == null)
        {
            return NotFound();
        }

        var item = _db.CartItems
            .FirstOrDefault(c => c.ProductId == id);

        if (item == null)
        {
            item = new CartItem
            {
                ProductId = id,
                Quantity = 1
            };

            _db.CartItems.Add(item);
        }
        else
        {
            item.Quantity++;
        }

        _db.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult UpdateQuantity(int id, int quantity)
    {
        var item = _db.CartItems.Find(id);

        if (item != null)
        {
            if (quantity <= 0)
            {
                _db.CartItems.Remove(item);
            }
            else
            {
                item.Quantity = quantity;
            }

            _db.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    public IActionResult Remove(int id)
    {
        var item = _db.CartItems.Find(id);

        if (item != null)
        {
            _db.CartItems.Remove(item);
            _db.SaveChanges();
        }

        return RedirectToAction("Index");
    }
}