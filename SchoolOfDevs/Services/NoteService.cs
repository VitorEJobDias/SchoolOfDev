using SchoolOfDevs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using SchoolOfDevs.Entities;
using SchoolOfDevs.Helpers;
using SchoolOfDevs.Exceptions;

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
            note.CreatedAt = DateTime.Now;
            _dataContext.Notes.Add(note);
            await _dataContext.SaveChangesAsync();

            return note;
        }

        public async Task Delete(int id)
        {
            Note note = await _dataContext.Notes.SingleOrDefaultAsync(x => x.Id == id);

            if (note is null)
                throw new KeyNotFoundException($"Note {id} not found");

            _dataContext.Notes.Remove(note);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<Note>> GetAll() => await _dataContext.Notes.ToListAsync();
        

        public async Task<Note> GetById(int id)
        {
            Note note = await _dataContext.Notes.SingleOrDefaultAsync(x => x.Id == id);

            if (note is null)
                throw new KeyNotFoundException($"Note {id} not found");

            return note;
        }

        public async Task Update(Note note, int id)
        {
            if (note.Id != id)
                throw new BadRequestException("Route id differs Note id");

            note.UpdatedAt = DateTime.Now;

            Note noteDb = await _dataContext.Notes
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

            if (noteDb is null)
            {
                throw new KeyNotFoundException($"Note {id} not found");
            }

            note.CreatedAt = noteDb.CreatedAt;

            _dataContext.Entry(note).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
        }
    }
}
