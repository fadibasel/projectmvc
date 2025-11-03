using KAShope.Data;
using KAShope.Models;
using Microsoft.AspNetCore.Mvc;

namespace KAShope.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        
        public IActionResult Index()
        {
            var cats = context.Categories.ToList();
            return View(cats);
        }
        public IActionResult Create()
        {

            return View(new Category());
        }

        public IActionResult Store(Category request)
        {
            if (!ModelState.IsValid)
            {

                return View("Create", request);
            }
            context.Categories.Add(request);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int id)
        {
            var cat = context.Categories.Find(id);
            return View(cat);
        }
        public IActionResult Update(Category request)
        {
            if (!ModelState.IsValid)
            {
                return View("edit", request);
            }
            var cat = context.Categories.Find(request.Id);
            cat.Name = request.Name;
            //context.Categories.Update(request);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int id)
        {
            var cat = context.Categories.Find(id);
            context.Categories.Remove(cat);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

       

    }
}
    