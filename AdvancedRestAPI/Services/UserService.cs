using AdvancedRestAPI.Data;
using AdvancedRestAPI.DTOs;
using AdvancedRestAPI.Interfaces;
using AdvancedRestAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace AdvancedRestAPI.Services
{
    public class UserService : IUser
    {
        private IMongoCollection<User> usersCollection;
        private AppDbContext _context;
        private IMapper _mapper;
        public UserService( AppDbContext context, IMapper mapper, IConfiguration configuration)
        {
                _context = context;
                _mapper= mapper;
            var mongoClient = new MongoClient(configuration.GetConnectionString("UserConnection"));
            var mongoDatabase = mongoClient.GetDatabase("UserDB");
            usersCollection =  mongoDatabase.GetCollection<User>("Users");
        }

        public async Task<(bool isSuccess, string ErrorMessage)> AddUser(UserDTO userdto)
        {
            if(userdto is not null)
            {
                var user =_mapper.Map<User>(userdto);
                //await _context.Users.AddAsync(user);
                //await _context.SaveChangesAsync();
                await usersCollection.InsertOneAsync(user);
                
                return (true, null);
            }
            return (false, "Please provide user data");   
        }

        public async Task<(bool isSuccess, string ErrorMessage)> DeleteUser(Guid id)
        {
            //var user = await _context.Users.FindAsync(id);
            var user = await usersCollection.Find(u=> u.Id == id).FirstOrDefaultAsync();
            if(user is not null)
            {
                //_context.Users.Remove(user);
                //await _context.SaveChangesAsync();
                await usersCollection.DeleteOneAsync(u => u.Id == id);
                return(true, null);
            }
            return (false, "User not found");

        }

        public async Task<(bool isSuccess, List<UserDTO> User, string ErrorMessage)> GetAllUsers()
        {
            //var users = await _context.Users.ToListAsync();
            var users= await usersCollection.Find(u=>true).ToListAsync();
            if(users is not null)
            {
               var result = _mapper.Map<List<UserDTO>>(users);
                
                return (true, result, null);
            }
            return(false, null, "Users not found");
            
        }

        public async Task<(bool isSuccess, UserDTO User, string ErrorMessage)> GetUserById(Guid Id)
        {
            //var user = await _context.Users.FindAsync( Id);
            var user = await usersCollection.Find(u=>u.Id == Id).FirstOrDefaultAsync();
            if (user is not null)
            {
               var result= _mapper.Map<UserDTO>(user);
                return (true, result, null);
            }
            return (false, null, "User not found");
        }

        public async Task<(bool isSuccess, string ErrorMessage)> UpdateUser(Guid id, UserDTO userdto)
        {
            //var userObject = await _context.Users.FindAsync(id);
            var userObject = await usersCollection.Find(u=> u.Id == id).FirstOrDefaultAsync();  
            if(userObject is not null)
            {
               var user = _mapper.Map<User>(userdto);
                userObject.Name = user.Name;
                userObject.Address = user.Address;
                userObject.Phone = user.Phone;
                userObject.BloodGroup = user.BloodGroup;
                //await _context.SaveChangesAsync();
                await usersCollection.ReplaceOneAsync(u => u.Id == id,user);
                return (true, null);
            }
            return (false, "User not found");
            

        }
    }
}
