using Article.BusinessLayer.Interfaces;
using Article.Core;
using Article.Core.Entities;
using Article.DataAccess.Repository;
using Article.DataAccess.UnitOfWork;
using Article.Dto.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.BusinessLayer.Services
{
    public class PostService : IPost
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Comment> _commRepository;
        private readonly IUnitOfWork _uow;


        public PostService(IUnitOfWork uow)
        {
            _uow = uow;
            _postRepository = _uow.GetRepository<Post>();
            _userRepository = _uow.GetRepository<User>();
            _categoryRepository = _uow.GetRepository<Category>();
            _commRepository = _uow.GetRepository<Comment>();
        }

        public PostDto GetPostDetailsByPostId(int postId)
        {


            var post = (from p in _postRepository.GetAll()
                        where p.Id == postId
                        select new PostDto
                        {
                            Id = p.Id,
                            CategoryId = p.CategoryId,
                            Title = p.Title,
                            ModifiedOn = p.ModifiedOn,
                            PostContent = p.PostContent,
                            ShortDescription = p.ShortDescription,
                            Slug = p.Slug,
                            IsActive = p.IsActive,
                            ImageUrl = "/PostImageView/" + p.Id + "/100/100"

                        }).SingleOrDefault();
            post.ModifiedOnString = post.ModifiedOn.ToString("yyyy-MM-ddThh:mm"); //javascript tarafı tarih alma

            return post;
        }
        public List<PostUserDto> GetPostAll(int? categoryId)
        {

            var post = (from p in _postRepository.GetAll()
                        join u in _userRepository.GetAll() on p.UserId equals u.Id
                        join c in _categoryRepository.GetAll() on p.CategoryId equals c.Id
                        join y in _commRepository.GetAll() on p.Id equals y.PostId
                        into commentCount
                        where p.IsActive == true
                        && (categoryId != null ? p.CategoryId == categoryId : 1 == 1)
                        orderby p.CreatedOn descending
                        select new PostUserDto
                        {
                            Id = p.Id,
                            CategoryId = p.CategoryId,
                            CategoryName = c.Name,
                            Title = p.Title,
                            Slug = p.Title,
                            ShortDescription = p.ShortDescription,
                            PostImageUrl = "/PostImageView/" + p.Id,
                            UserImageUrl =u.Image !=null ? "/ProfileImageView/" + u.Id : ConstantTypes.profileImage,
                            FullName = u.FullName,
                            Job = u.Job,
                            Color = c.Color,
                            Count = commentCount.Count()

                        }).Take(ConstantTypes.postCount).ToList();
            return post;
        }
        public PostDetailDto GetPostDetail(int id)
        {
            var post = (from p in _postRepository.GetAll()
                        join u in _userRepository.GetAll() on p.UserId equals u.Id
                        join c in _categoryRepository.GetAll() on p.CategoryId equals c.Id
                        where p.IsActive == true
                        && p.Id == id
                        select new PostDetailDto
                        {
                            Id = p.Id,
                            Title = p.Title,
                            PostImageUrl = "/PostImageView/" + p.Id,
                            UserImageUrl = "/ProfileImageView/" + u.Id,
                            FullName = u.FullName,
                            Job = u.Job,
                            PostContent = p.PostContent,
                            CreatedOn = p.CreatedOn,
                        }).SingleOrDefault();
            post.CreatedOnString = post.CreatedOn.ToString("yyyy-MM-ddThh:mm");
            return post;
        }

        public bool AnyPostByCategoryId(int categoryId)
        {
            return _postRepository.GetAll().Any(x => x.CategoryId == categoryId);
        }

        public string GetSlugAnyPost(string slug)
        {

            int count = 0;
            string orgSlug = slug;
            while (_postRepository.GetAll().Where(x => x.Slug == slug).SingleOrDefault() != null)
            {
                count++;
                var result = _postRepository.GetAll().Where(x => x.Slug == slug).SingleOrDefault();
                slug = orgSlug + "-" + count;
            }
            return slug;
        }

        public List<GetPostbyUserIdDto> GetPostsByFilterParams(int userId, int pageNumber, string title, int? categoryId)
        {
            var list = (from d in _postRepository.GetAll()
                        where d.UserId == userId
                        && (categoryId != null ? (d.CategoryId == categoryId.Value) : true)
                        && (title != null ? (d.Title.Contains(title)) : true)
                        orderby d.CreatedOn descending
                        select new GetPostbyUserIdDto
                        {
                            PostId = d.Id,
                            Title = d.Title,
                            CreatedOn = d.CreatedOn,
                            IsActive = d.IsActive

                        }).Skip(pageNumber * ConstantTypes.listCount).Take(ConstantTypes.listCount).ToList();

            foreach (var item in list)
            {
                item.CreatedOnString = item.CreatedOn.ToString("dd.MM.yyyy hh:mm");
            }
            return list;
        }
        public int GetPostCount()
        {
            var count = _postRepository.GetAll().Count();
            var result = count / ConstantTypes.listCount;
            if (count % ConstantTypes.listCount == 0)
                return Convert.ToInt32(result);
            else
                return Convert.ToInt32(result + 1);
        }


        public void Insert(PostDto post)
        {
            var postEntity = Mapper.DynamicMap<Post>(post);
            postEntity.CreatedOn = DateTime.Now;
            _postRepository.Insert(postEntity);

        }

        public void Update(PostDto post)
        {
            var postEntity = _postRepository.Find(post.Id);
            Mapper.DynamicMap(post, postEntity);
            _postRepository.Update(postEntity);

            //postEntity.CategoryId = post.CategoryId;
            //postEntity.ModifiedOn = post.ModifiedOn;
            //postEntity.IsActive = post.IsActive;
            //postEntity.PostContent = post.PostContent;
            //postEntity.ShortDescription = post.ShortDescription;
            //postEntity.Slug = post.Slug;

        }
        public void Delete(int id)
        {
            var postEntity = _postRepository.Find(id);
            _postRepository.Delete(postEntity);

        }
        public byte[] GetPostImageById(int Id)
        {
            var result = _postRepository.GetAll().Where(p => p.Id == Id).SingleOrDefault();
            return result == null ? null : result.Image;
        }
    }
}
