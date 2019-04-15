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
    public class ManagerController : AdminController
    {
        private readonly IUser _userService;
        private readonly IUnitOfWork _uow;

        public ManagerController(IUnitOfWork uow, IUser userService) : base(uow)
        {
            _userService = userService;
            _uow = uow;
        }

        public ActionResult Index()
        {

            return View();
        }
    }
}