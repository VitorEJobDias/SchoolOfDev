using SchoolOfDevs.Entities;

namespace SchoolOfDevs.Services.Interfaces
{
    public interface ICourseService
    {
        public Task<List<Course>> GetAll();
        public Task<Course> GetById(int id);
        public Task Delete(int id);
        public Task<Course> Create(Course course);
        public Task Update(Course course, int id);
    }
}
