using Article.BusinessLayer.Interfaces;
using Article.BusinessLayer.Services;
using Article.Core.Entities;
using Article.DataAccess.Repository;
using Article.DataAccess.UnitOfWork;
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;
using Unity.Mvc5;

namespace Article.IoC
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            RegisterTypes(container);       
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.BindInRequestScope<ICategory, CategoryService>();
            container.BindInRequestScope<IPost, PostService>();
            container.BindInRequestScope<IUnitOfWork, UnitOfWork>();
            container.BindInRequestScope<IUser, UserServices>();
            container.BindInRequestScope<IComment, CommentServices>();
            container.BindInRequestScope<IRepository<User>,Repository<User>>();
            container.BindInRequestScope<IRepository<Post>, Repository<Post>>();
            container.BindInRequestScope<IRepository<Category>, Repository<Category>>();
            container.BindInRequestScope<IRepository<Comment>, Repository<Comment>>();
        }

        public static void BindInRequestScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new HierarchicalLifetimeManager());
        }
    }
}