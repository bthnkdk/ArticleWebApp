using Article.BusinessLayer.Interfaces;
using Article.DataAccess.UnitOfWork;
using Article.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Article.WebApp.Controllers
{
    public class AdminController : BaseController
    {
        public AdminController(IUnitOfWork uow):base(uow)
        {

        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (CurrentSession.User==null) 
            {
                Response.Redirect("/Login");
            }
            base.OnActionExecuting(filterContext);
        }

    }
}