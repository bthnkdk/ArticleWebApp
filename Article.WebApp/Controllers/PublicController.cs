using Article.BusinessLayer.Interfaces;
using Article.BusinessLayer.Services;
using Article.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Article.WebApp.Controllers
{
    public class PublicController : BaseController
    {

        private readonly IUnitOfWork _uow;
        public PublicController(IUnitOfWork uow) : base(uow)
        {
            _uow = uow;
        }


    }
}