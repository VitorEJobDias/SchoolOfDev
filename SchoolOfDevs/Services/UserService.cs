using SchoolOfDevs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using SchoolOfDevs.Entities;
using SchoolOfDevs.Helpers;

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
            User userDb = await _dataContext.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.UserName == user.UserName);
            if (userDb is not null)
            {
                throw new Exception($"UserName { user.UserName } already exist.");
            }

            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();

            return user;
        }

        public async Task Delete(int id)
        {
            User user = await _dataContext.Users.SingleOrDefaultAsync(x => x.Id == id);

            if (user is null)
                throw new Exception($"User {id} not found");

            _dataContext.Users.Remove(user);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetAll() => await _dataContext.Users.ToListAsync();
        

        public async Task<User> GetById(int id)
        {
            User user = await _dataContext.Users.SingleOrDefaultAsync(x => x.Id == id);

            if (user is null)
                throw new Exception($"User {id} not found");

            return user;
        }

        public async Task Update(User user, int id)
        {
            if (user.Id != id)
                throw new Exception("Route id differs User id");

            User userDb = await _dataContext.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

            if (userDb is null)
            {
                throw new Exception($"User {id} not found");
            }

            _dataContext.Entry(user).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
        }
    }
}
