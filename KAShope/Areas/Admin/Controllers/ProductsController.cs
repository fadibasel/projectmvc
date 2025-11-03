using KAShope.Data;
using KAShope.Models;
using KAShope.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KAShope.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
        {
              // var products = context.Products.Join(context.Categories,p=>p.CategoryId,
              //  c=>c.Id,(p,c)=>new
              //   {
              //     p.Name,p.Id,p.Description,p.Price,p.ImageUrl,CategoryName=c.Name
              //  });
              //بدلها 
            var products = context.Products.Include(p => p.Category).ToList();
            var productsVm = new List<ProductsViewModels>();
            foreach(var item in products)
            {
                var vm = new ProductsViewModels
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    ImageUrll = $"{Request.Scheme}://{Request.Host}/images/{item.ImageUrl}",
                    CategoryName = item.Category.Name
                };
                productsVm.Add(vm);
            }

            return View(productsVm);
        }
        public IActionResult Create()
        {
            ViewBag.Categories = context.Categories.ToList();
            return View(new Product());
        }
        public IActionResult Edit(int id)
        {
            var product = context.Products.Find(id);
            ViewBag.Categories = context.Categories.ToList();
            return View(product);
        }
       // [ValidateAntiForgeryToken]
        public IActionResult Store(Product request, IFormFile file)
        {
            ViewBag.Categories = context.Categories.ToList();
            ModelState.Remove("File");


            if (!ModelState.IsValid)
            {

                return View("Create", request);
            }
            if (file == null || file.Length == 0) 
            {
                ModelState.AddModelError("ImageUrl", "please uploud an image");
                return View("Create", request);
            }

            var allowedExtensions = new[] { ".jpg", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLower();//.JPG

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("ImageUrl", "only jpg ,webp files are allowed");
                return View("Create", request);
            }
            if(file.Length>2 * 1024 * 1024)
            {
                ModelState.AddModelError("ImageUrl", "image size must be less than 2MB");
                return View("Create", request);
            }
            
                var fileName = Guid.NewGuid().ToString();//rewrw432wrewrw
                fileName += Path.GetExtension(file.FileName);//rewrw432wrewrw.png
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images" ,fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }       
                request.ImageUrl = fileName;
                request.Rate = 0;
                context.Products.Add(request);
                context.SaveChanges();  
                return RedirectToAction(nameof(Index));
            }

            
            
        
        public IActionResult Update(Product Request,IFormFile? file)
        {
            var product = context.Products.Find(Request.Id);//6
            //var product = context.Products.AsNoTracking().FirstOrDefault(c=> c.Id == Request.Id);
            product.Name=Request.Name;
            product.Description=Request.Description;
            product.Price=Request.Price;
            product.Quantity=Request.Quantity;
            product.CategoryId=Request.CategoryId;
            if(file != null && file.Length > 0)
            {
                var oldfilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", product.ImageUrl);
                System.IO.File.Delete(oldfilePath);

                var fileName = Guid.NewGuid().ToString();//rewrw432wrewrw
                fileName += Path.GetExtension(file.FileName);//rewrw432wrewrw.png
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                product.ImageUrl = fileName;
            }
            context.SaveChanges();


            return RedirectToAction("Index");

        }
        public IActionResult Remove(int id)
        {
            var products = context.Products.Find(id);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", products.ImageUrl);
            System.IO.File.Delete(filePath);
            context.Products.Remove(products);
            context.SaveChanges ();
            return RedirectToAction(nameof(Index));
        }
    }
}
