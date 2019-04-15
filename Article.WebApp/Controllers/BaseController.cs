using Article.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Article.WebApp.Controllers
{
    public class BaseController : Controller
    {

        public BaseController(IUnitOfWork uow)
        {

        }

    }
}