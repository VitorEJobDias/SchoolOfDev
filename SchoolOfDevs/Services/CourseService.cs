using SchoolOfDevs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using SchoolOfDevs.Entities;
using SchoolOfDevs.Helpers;

namespace SchoolOfDevs.Services
{
    public class CourseService : ICourseService
    {
        private readonly DataContext _dataContext;

        public CourseService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Course> Create(Course course)
        {
            Course courseDb = await _dataContext.Courses
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Name == course.Name);
            if (courseDb is not null)
            {
                throw new Exception($"Name { course.Name } already exist.");
            }

            _dataContext.Courses.Add(course);
            await _dataContext.SaveChangesAsync();

            return course;
        }

        public async Task Delete(int id)
        {
            Course course = await _dataContext.Courses.SingleOrDefaultAsync(x => x.Id == id);

            if (course is null)
                throw new Exception($"Course {id} not found");

            _dataContext.Courses.Remove(course);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<Course>> GetAll() => await _dataContext.Courses.ToListAsync();
        

        public async Task<Course> GetById(int id)
        {
            Course course = await _dataContext.Courses.SingleOrDefaultAsync(x => x.Id == id);

            if (course is null)
                throw new Exception($"Course {id} not found");

            return course;
        }

        public async Task Update(Course course, int id)
        {
            if (course.Id != id)
                throw new Exception("Route id differs Course id");

            Course courseDb = await _dataContext.Courses
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

            if (courseDb is null)
            {
                throw new Exception($"Course {id} not found");
            }

            _dataContext.Entry(course).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
        }
    }
}
