using Article.BusinessLayer.Interfaces;
using Article.DataAccess.UnitOfWork;
using Article.Dto.Entity;
using Article.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Article.WebApp.Controllers
{
    public class SiteController : PublicController
    {
        private readonly IUnitOfWork _uow;
        private readonly IPost _postService;
        private readonly ICategory _catService;
        private readonly IUser _userService;
        private readonly IComment _commentService;

        public SiteController(UnitOfWork uow, IPost postService, ICategory catService, IUser userService, IComment commService) : base(uow)
        {
            _uow = uow;
            _postService = postService;
            _catService = catService;
            _userService = userService;
            _commentService = commService;
        }


        public static string html;



        public ActionResult Index()
        {

            html = null;
            var result = _catService.GetCategories();

            foreach (var item in result)
            {
                html += "<li><a href='/" + StringManager.ToSlug(item.Name) + "-" + item.Id + "' style='color:" + item.Color + "!important;'><i class='" + item.Icon + "'></i>" + item.Name + "</a></li>";
            }
            return View(_postService.GetPostAll(null));
        }

        public ActionResult PostList(int categoryId, string categoryName)
        {
            return View(_postService.GetPostAll(categoryId));
        }

        public ActionResult PostDetail(int id, int categoryId)
        {

            PostDetailPageDto pdp = new PostDetailPageDto();
            pdp.PostDetail = _postService.GetPostDetail(id);
            pdp.Comments = _commentService.GetCommentAll(id).ToList();
            pdp.Category = _catService.GetCategoryDetailByCategoryId(categoryId);
            pdp.PostList = _postService.GetPostAll(null).Take(5).ToList();

            if (Request.Cookies["test1"] != null)
            {
                if (Request.Cookies["test1"][string.Format("pId_{0}", id)] == null)
                {
                    HttpCookie cookie = Request.Cookies["test1"];
                    cookie[string.Format("pId_{0}", id)] = "1";
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookie);

                    _postService.UpdatePageCount(id);
                    _uow.SaveChanges();
                }

            }
            else
            {
                HttpCookie cookie = new HttpCookie("test1");
                cookie[string.Format("pId_{0}", id)] = "1";
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(cookie);

                _postService.UpdatePageCount(id);
                _uow.SaveChanges();

            }
            return View(pdp);
        }

        public FileContentResult ProfileImageView(int id, int? w, int? h)
        {
            return new FileContentResult(ImageManager.ConvertToSize(_userService.GetUserImage(id), w, h), "image/png");
        }

        public ActionResult PostImageView(int id, int? w, int? h)
        {
            return new FileContentResult(ImageManager.ConvertToSize(_postService.GetPostImageById(id), w, h), "image/png");
        }

        [HttpPost]
        public ActionResult Insert(CommentDto comment)
        {
            comment.UserId = ((SessionManager)Session["SessionContext"]).Id;
            comment.AddedDate = DateTime.Now;
            _commentService.Insert(comment);
            _uow.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult GetMenu()
        //{
        //    var result = _catService.GetCategories();
        //    var html = "";
        //    foreach (var item in result)
        //    {
        //        html += "<li><a href='/" + StringManager.ToSlug(item.Name) + "-" + item.Id + "' style='color:" + item.Color + "!important;'><i class='" + item.Icon + "'></i>" + item.Name + "</a></li>";
        //    }
        //    return Json(html, JsonRequestBehavior.AllowGet);

        //}

    }
}