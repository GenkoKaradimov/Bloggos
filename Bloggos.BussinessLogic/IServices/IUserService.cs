using Bloggos.BussinessLogic.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggos.BussinessLogic.IServices
{
    public interface IUserService
    {
        Task<UserModel> GetUser(string username);

        Task<UserModel> LoginUser(UserLoginModel model);

        Task<UserModel> RegisterUser(UserRegistrationModel model);
    }
}
