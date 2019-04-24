using Article.BusinessLayer.Interfaces;
using Article.DataAccess.UnitOfWork;
using Article.Dto.Entity;
using Article.Utilities;
using Article.WebApp.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Article.WebApp.Controllers
{
    public class LoginController : PublicController
    {

        private readonly IUser _userService;
        private readonly IUnitOfWork _uow;
        private SessionManager _sessionManager;

        public LoginController(IUnitOfWork uow, IUser userService):base(uow)
        {
            _userService = userService;
            _uow = uow;
            _sessionManager = new SessionManager();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginControl(LoginDto login)
        {
            login.Password = EncryptManager.Base64Encrypt(login.Password);
            var result = _userService.GetByUserNameAndPw(login.UserName, login.Password);
            if (result != null)
            {

                Mapper.DynamicMap(result, _sessionManager);
                Session["SessionContext"] = _sessionManager;
                CurrentSession.Set<UserDto>("login", result);// sessionmanageri sil kullanışsız
                return Json("/Site/Index", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("loginFailed", JsonRequestBehavior.AllowGet);


        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            Response.Redirect("/Site/Index");
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }

}
