using SchoolOfDevs.Entities;

namespace SchoolOfDevs.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<User>> GetAll();
        public Task<User> GetById(int id);
        public Task Delete(int id);
        public Task<User> Create(User user);
        public Task Update(User user, int id);
    }
}
