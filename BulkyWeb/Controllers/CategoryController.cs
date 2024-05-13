using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using BulkyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        #region Ctor

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion


        #region Index Page
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.catrepo.Getall().ToList();
               
            return View(objCategoryList);
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
        public IActionResult Create(Category obj)
        {
          
             
            if(obj.Name == obj.DisplayOrder.ToString()) 
            {
                ModelState.AddModelError("name","The DisplayOrder cannot exactly match the Name.");    
            }
            /**
            if(obj.Name.ToLower()=="test")
            {
                ModelState.AddModelError("", "Test is ans Invalid value");
            }
            */
            if (ModelState.IsValid)
            {
                _unitOfWork.catrepo.Add(obj);
                _unitOfWork.Save();
               
                TempData["success"] = "Category created Successfully.";
                 return RedirectToAction("Index");
            }
            return View();

        }


        #endregion



        #region Edit View

        /// Edit option
        public IActionResult Edit(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _unitOfWork.catrepo.Get(u => u.Id == id);
        
           //  Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id==id);
           //  Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
           

            if(categoryFromDb==null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        #endregion





        #region Edit Post


        [HttpPost]
        public IActionResult Edit(Category obj)
        {


            /**
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            
            if(obj.Name.ToLower()=="test")
            {
                ModelState.AddModelError("", "Test is ans Invalid value");
            }
            */
            if (ModelState.IsValid)
            {
                _unitOfWork.catrepo.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category update Successfully";
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

            Category? categoryFromDb = _unitOfWork.catrepo.Get(u => u.Id == id);
                
            //  Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id==id);
            //  Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();


            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        #endregion


        #region Delete Post

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {


            /**
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            
            if(obj.Name.ToLower()=="test")
            {
                ModelState.AddModelError("", "Test is ans Invalid value");
            }
            */

            Category? obj = _unitOfWork.catrepo.Get(u => u.Id == id);
                 
            if (obj==null)
            {
                return NotFound();
            }

            _unitOfWork.catrepo.Remove(obj);
            _unitOfWork.Save();
 
            TempData["success"] = "Category Delete Successfully";
            return RedirectToAction("Index");

        }

        #endregion



    }
}
