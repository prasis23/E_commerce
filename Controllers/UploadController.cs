//using EmployeeManagement.Data;
//using Microsoft.AspNetCore.Mvc;
//using System.IO;
//namespace EmployeeManagement.Controllers
//{
//    public class UploadController: Controller
//    {
//        private readonly ApplicationDbContext context;
//        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment environment;

//        public UploadController(ApplicationDbContext Context,IHostingEnvironment environment)
//        {
//            context = Context;
//            this.environment = environment;
//        }
//        [HttpGet]
//        public IActionResult UploadImage()
//        {
//            return View();
//        }
//        [HttpPost]
//        public IActionResult UploadImage(ImageCreateModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var path = Environment.WebRootPath;
//                var filePath = "Content/Image/" + model.ImagePath.FileName;
//                var fullPath = Path.Combine(path, filePath);
//                return View();
//            }
//           else
//            {
//                return View(model);
//            }
//        }


//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}
