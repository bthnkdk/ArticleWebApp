using Article.BusinessLayer.Interfaces;
using Article.DataAccess.UnitOfWork;
using Article.Dto.Entity;
using Article.Utilities;
using Article.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Article.WebApp.Controllers
{
    public class CommentController : AdminController
    {
        private readonly IComment _commentService;
        private readonly IUnitOfWork _uow;

        public CommentController(IUnitOfWork uow, IComment commentService) : base(uow)
        {
            _commentService = commentService;
            _uow = uow;
        }


        [HttpPost]
        public ActionResult Insert(CommentDto comment)
        {
            comment.UserId = CurrentSession.User.Id;
            comment.AddedDate = DateTime.Now;
            _commentService.Insert(comment);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}