using Article.Core.Entities;
using Article.Dto.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.BusinessLayer.Interfaces
{
    public interface IUser
    {
        UserDto GetByUserNameAndPw(string userName, string password);
        void Update(UserDto user);
        byte[] GetUserImage(int id);
        UserDto Find(int id);
        UserDto getUserName(string userName);
    }
}
