﻿using Article.BusinessLayer.Interfaces;
using Article.DataAccess.UnitOfWork;
using Article.Dto.Entity;
using Article.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Article.WebApp.Controllers
{
    public class ProfileController : AdminController
    {
        private readonly IUser _userService;
        private readonly IUnitOfWork _uow;

        public ProfileController(IUnitOfWork uow, IUser userService) : base(uow)
        {
            _uow = uow;
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View(_userService.Find(((SessionManager)Session["SessionContext"]).Id));
        }

        [HttpPost]
        public ActionResult TempImage(HttpPostedFileBase ImageFormData)
        {
            using (BinaryReader reader = new BinaryReader(ImageFormData.InputStream))
            {
                Session["TempImage"] = reader.ReadBytes((Int32)ImageFormData.ContentLength);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult TempImageShow(HttpPostedFileBase ImageFormData)
        {
            return new FileContentResult((byte[])Session["TempImage"], "image/png");
        }

        [HttpPost]
        public ActionResult ImageUpdate()
        {
            _userService.Update(new UserDto
            {
                WhichUpdate = "image",
                Value = (byte[])Session["TempImage"],
                Id = ((SessionManager)Session["SessionContext"]).Id
            });
            //_uow.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Update(UserDto user)
        {
            var sameUser = _userService.getUserName(user.UserName);
            if (user.WhichUpdate == "nameJob")
            {
                user.Id = ((SessionManager)Session["SessionContext"]).Id;
                _userService.Update(user);
                //_uow.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            if (sameUser != null)
            {
                if (sameUser.UserName == ((SessionManager)Session["SessionContext"]).UserName)
                {
                    user.Id = ((SessionManager)Session["SessionContext"]).Id;
                    user.Password = EncryptManager.Base64Encrypt(user.Password);
                    _userService.Update(user);
                    //_uow.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            else
            {
                user.Id = ((SessionManager)Session["SessionContext"]).Id;
                _userService.Update(user);
                //_uow.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }

        }
    }
}