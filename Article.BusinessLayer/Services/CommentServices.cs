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
    public class CommentServices : IComment
    {
        private readonly IRepository<Comment> _commRepository;
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _uow;

        public CommentServices(IUnitOfWork uow)
        {
            _uow = uow;
            _commRepository = uow.GetRepository<Comment>();
            _postRepository = uow.GetRepository<Post>();
            _userRepository = uow.GetRepository<User>();
        }

        public List<CommentUserDto> GetCommentAll(int postId)
        {
            var post = (from c in _commRepository.GetAll()
                        join u in _userRepository.GetAll() on c.UserId equals u.Id
                        join p in _postRepository.GetAll() on c.PostId equals p.Id
                        where p.Id == postId
                        orderby c.AddedDate descending
                        select new CommentUserDto
                        {
                            Id = c.Id,
                            UserId = c.UserId,
                            PostId = c.PostId,
                            Text = c.Text,
                            AddedDate = c.AddedDate,
                            Fullname = u.UserName,
                            ProfileImageUrl = u.Image != null ? "/ProfileImageView/" + u.Id : ConstantTypes.profileImage,
                        }).ToList();
            return post;
        }

       
        public void Insert(CommentDto comment)
        {
            var commentEntity = Mapper.DynamicMap<Comment>(comment);
            _commRepository.Insert(commentEntity);
            _uow.SaveChanges(); // kontrol et, save changes olayının business değil controllerda çalışması gerekli

        }


    }
}
