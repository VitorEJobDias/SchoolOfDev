using SchoolOfDevs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using SchoolOfDevs.Entities;
using SchoolOfDevs.Helpers;

namespace SchoolOfDevs.Services
{
    public class NoteService : INoteService
    {
        private readonly DataContext _dataContext;

        public NoteService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Note> Create(Note note)
        {
            _dataContext.Notes.Add(note);
            await _dataContext.SaveChangesAsync();

            return note;
        }

        public async Task Delete(int id)
        {
            Note note = await _dataContext.Notes.SingleOrDefaultAsync(x => x.Id == id);

            if (note is null)
                throw new Exception($"User {id} not found");

            _dataContext.Notes.Remove(note);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<Note>> GetAll() => await _dataContext.Notes.ToListAsync();
        

        public async Task<Note> GetById(int id)
        {
            Note note = await _dataContext.Notes.SingleOrDefaultAsync(x => x.Id == id);

            if (note is null)
                throw new Exception($"Note {id} not found");

            return note;
        }

        public async Task Update(Note user, int id)
        {
            if (user.Id != id)
                throw new Exception("Route id differs Note id");

            Note userDb = await _dataContext.Notes
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

            if (userDb is null)
            {
                throw new Exception($"Note {id} not found");
            }

            _dataContext.Entry(user).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
        }
    }
}
