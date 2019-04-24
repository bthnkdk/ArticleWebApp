using Article.BusinessLayer.Interfaces;
using Article.Core;
using Article.Core.Entities;
using Article.DataAccess.Repository;
using Article.DataAccess.UnitOfWork;
using Article.Dto.Entity;
using Article.Utilities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.BusinessLayer.Services
{
    public class UserServices : IUser
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _uow;
        private readonly UserDto _userDto;



        public UserServices(UnitOfWork uow)
        {
            _uow = uow;
            _userRepository = _uow.GetRepository<User>();
            _userDto = new UserDto();
        }

        public byte[] GetUserImage(int id)
        {
            var result = _userRepository.GetAll().Where(x => x.Id == id).SingleOrDefault();
            return result == null ? null : result.Image;
        }

        public UserDto GetByUserNameAndPw(string userName, string password)
        {
            var control = (from x in _userRepository.GetAll()
                           where x.UserName == userName && x.Password == password
                           select new UserDto
                           {
                               Id = x.Id,
                               FullName = x.FullName,
                               UserName = x.UserName,
                               Job = x.Job,
                               ImageUrl = x.Image != null ? "/ProfileImageView/" + x.Id : ConstantTypes.profileImage,
                               IsActive = x.IsActive,
                               IsAdmin = x.IsAdmin,
                           }).SingleOrDefault();

            return control;
        }


        public void Update(UserDto user)
        {

            var entity = _userRepository.Find(user.Id);
            if (user.WhichUpdate == "userPass")
            {
                entity.UserName = user.UserName;
                entity.Password = user.Password;
                _uow.SaveChanges();

            }
            if (user.WhichUpdate == "nameJob")
            {
                entity.FullName = user.FullName;
                entity.Job = user.Job;
                _uow.SaveChanges();
            }
            if (user.WhichUpdate == "image")
            {
                entity.Image = user.Value;
                _uow.SaveChanges();
            }
            //Mapper.DynamicMap(user, entity);
            _userRepository.Update(entity);

        }



        public UserDto Find(int id)
        {
            var user = (from x in _userRepository.GetAll()
                        where x.Id == id
                        select new UserDto
                        {
                            Id = x.Id,
                            FullName = x.FullName,
                            ImageUrl = x.Image != null ? "/ProfileImageView/" + x.Id : ConstantTypes.profileImage,
                            Job = x.Job,
                            UserName = x.UserName,
                            Password = x.Password,
                            IsActive = x.IsActive,
                            IsAdmin = x.IsAdmin

                        }).SingleOrDefault();
            user.Password = EncryptManager.Base64Decrypt(user.Password);
            return user;
        }

        public UserDto getUserName(string userName)
        {
            var control = (from x in _userRepository.GetAll()
                           where x.UserName == userName
                           select new UserDto
                           {
                               Id = x.Id,
                               FullName = x.FullName,
                               UserName = x.UserName,
                               Job = x.Job,
                               ImageUrl = x.Image != null ? "/ProfileImageView/" + x.Id : ConstantTypes.profileImage
                           }).SingleOrDefault();
            return control;
        }
    }
}
