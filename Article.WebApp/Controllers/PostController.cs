using Article.BusinessLayer.Interfaces;
using Article.DataAccess.UnitOfWork;
using Article.Dto.Entity;
using Article.Utilities;
using Article.WebApp.Filters;
using Article.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Article.WebApp.Controllers
{
    [AuthAdmin]
    public class PostController : AdminController
    {
        private readonly IPost _postService;
        private readonly IUnitOfWork _uow;

        public PostController(IUnitOfWork uow, IPost postService) : base(uow)
        {
            _postService = postService;
            _uow = uow;
        }

        public ActionResult Index()
        {

            return View(_postService.GetPostsByFilterParams(((SessionManager)Session["SessionContext"]).Id, 0, null, null));
        }

        public ActionResult GetPostCount()
        {
            return Json(_postService.GetPostCount(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPostsByFilterParams(int pageNumber, string title, int? categoryId)
        {
            title = title == "null" ? title = null : title; // js tarafında null değerini string olarak algılıyor ?
            return Json(_postService.GetPostsByFilterParams(((SessionManager)Session["SessionContext"]).Id, pageNumber, title, categoryId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPostDetailByPostId(int postId)
        {
            var result = _postService.GetPostDetailsByPostId(postId);
            result.PostContent = HttpUtility.HtmlDecode(result.PostContent);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(IdDto post)
        {
            _postService.Delete(post.Id);
            _uow.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(PostDto post)
        {
            post.Slug = _postService.GetSlugAnyPost(StringManager.ToSlug(post.Title));
            post.UserId = CurrentSession.User.Id;
            post.PostContent = HttpUtility.HtmlEncode(post.PostContent);
            if (Session["TempImage"] != null)
                post.Image = (byte[])Session["TempImage"];
            _postService.Update(post);
            _uow.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Insert(PostDto post)
        {
            post.Slug = _postService.GetSlugAnyPost(StringManager.ToSlug(post.Title));
            post.UserId = CurrentSession.User.Id;
            post.PostContent = HttpUtility.HtmlEncode(post.PostContent);
            if (Session["TempImage"] != null)
                post.Image = (byte[])Session["TempImage"];
            _postService.Insert(post);
            _uow.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}