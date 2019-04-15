using Article.BusinessLayer.Interfaces;
using Article.DataAccess.UnitOfWork;
using Article.Dto.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Article.WebApp.Controllers
{
    public class CategoryController : AdminController
    {
        private readonly ICategory _categoryService;
        private readonly IPost _postService;
        private readonly IUnitOfWork _uow;

        public CategoryController(IUnitOfWork uow, ICategory categoryService, IPost postService) : base(uow)
        {
            _uow = uow;
            _categoryService = categoryService;
            _postService = postService;
        }

        public ActionResult Index()
        {
            return View(_categoryService.GetCategories());
        }

        [HttpGet]
        public ActionResult GetCategories()
        {
            var list = _categoryService.GetCategories();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCategoryDetailByCategoryId(int categoryId)
        {
            var result = _categoryService.GetCategoryDetailByCategoryId(categoryId);
          
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Delete(IdDto model)
        {
            if (_postService.AnyPostByCategoryId(model.Id) == false)
            {
                _categoryService.Delete(model.Id);
                _uow.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult Update(CategoryDto category)
        {
            _categoryService.Update(category);
            _uow.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Insert(CategoryDto category)
        {
            _categoryService.Insert(category);
            _uow.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}