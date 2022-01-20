using SchoolOfDevs.Entities;

namespace SchoolOfDevs.Services.Interfaces
{
    public interface INoteService
    {
        public Task<List<Note>> GetAll();
        public Task<Note> GetById(int id);
        public Task Delete(int id);
        public Task<Note> Create(Note note);
        public Task Update(Note note, int id);
    }
}
