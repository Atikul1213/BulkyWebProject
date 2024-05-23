using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        #region Ctor

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion


        #region Index Page
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.Getall().ToList();

            return View(objProductList);
        }


        #endregion


        #region Create View
        /// Create option
        public IActionResult Create()
        {
            return View();
        }

        #endregion


        #region Create Post

        [HttpPost]
        public IActionResult Create(Product obj)
        {


            
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();

                TempData["success"] = "Product created Successfully.";
                return RedirectToAction("Index");
            }
            return View();

        }


        #endregion



        #region Edit View

        /// Edit option
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? ProductFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            //  Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id==id);
            //  Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();


            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);
        }

        #endregion





        #region Edit Post


        [HttpPost]
        public IActionResult Edit(Product obj)
        {


           
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product update Successfully";
                return RedirectToAction("Index");
            }
            return View();

        }


        #endregion


        #region Delete View

        /// Delete option
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            //  Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id==id);
            //  Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();


            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        #endregion


        #region Delete Post

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {


            

            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();

            TempData["success"] = "Product Delete Successfully";
            return RedirectToAction("Index");

        }

        #endregion

    }
}
