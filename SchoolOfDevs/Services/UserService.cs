using SchoolOfDevs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;
using SchoolOfDevs.Entities;
using SchoolOfDevs.Helpers;
using SchoolOfDevs.Exceptions;

namespace SchoolOfDevs.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;

        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User> Create(User user)
        {
            if (!user.Password.Equals(user.ConfirmPassword))
            {
                throw new BadRequestException("Password does not match ConfirmPassword");
            }

            User userDb = await _dataContext.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.UserName == user.UserName);
            if (userDb is not null)
            {
                throw new BadRequestException($"UserName { user.UserName } already exist.");
            }

            user.Password = BC.HashPassword(user.Password);

            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();

            return user;
        }

        public async Task Delete(int id)
        {
            User user = await _dataContext.Users.SingleOrDefaultAsync(x => x.Id == id);

            if (user is null)
                throw new KeyNotFoundException($"User {id} not found");

            _dataContext.Users.Remove(user);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetAll() => await _dataContext.Users.ToListAsync();
        

        public async Task<User> GetById(int id)
        {
            User user = await _dataContext.Users.SingleOrDefaultAsync(x => x.Id == id);

            if (user is null)
                throw new KeyNotFoundException($"User {id} not found");

            return user;
        }

        public async Task Update(User user, int id)
        {
            if (user.Id != id)
                throw new BadRequestException("Route id differs User id");
            else if (!user.Password.Equals(user.ConfirmPassword))
                throw new BadRequestException("Password does not match ConfirmPassword");
            

            User userDb = await _dataContext.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

            if (userDb is null)
                throw new KeyNotFoundException($"User {id} not found");
            else if (!BC.Verify(user.CurrentPassword, userDb.Password))
                throw new BadRequestException("Incorrect Password");

            user.CreatedAt = userDb.CreatedAt;
            user.Password = BC.HashPassword(user.Password);

            _dataContext.Entry(user).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
        }
    }
}
