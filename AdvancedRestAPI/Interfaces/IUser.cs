using AdvancedRestAPI.DTOs;
using AdvancedRestAPI.Models;

namespace AdvancedRestAPI.Interfaces
{
    public interface IUser
    {
        Task<(bool isSuccess,List<UserDTO> User,string ErrorMessage)> GetAllUsers();
        Task<(bool isSuccess, UserDTO User, string ErrorMessage)> GetUserById( Guid Id);
        Task<(bool isSuccess, string ErrorMessage)> AddUser(UserDTO user);
        Task<(bool isSuccess,  string ErrorMessage)> UpdateUser(Guid id, UserDTO user); 
        Task<(bool isSuccess, string ErrorMessage)> DeleteUser(Guid id);
    }
}
