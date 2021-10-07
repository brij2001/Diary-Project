using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Diary.Models;

namespace Diary.Data
{
    public class NoteDatabase
    {
        private readonly SQLiteAsyncConnection database;

        public NoteDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Note>().Wait();
        }

        public Task<List<Note>> GetNotesAsync()//get all notes
        {
            return database.Table<Note>().ToListAsync();
        }

        public Task<Note> GetNoteAsync(int id)//get 1  note
        {
            return database.Table<Note>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Note note)
        {
            if (note.ID != 0)//to update note
            {

                return database.UpdateAsync(note);
            }
            else // Save a new note.
            {
                return database.InsertAsync(note);
            }
        }

        public Task<int> DeleteNoteAsync(Note note)// Delete a note.
        {
            return database.DeleteAsync(note);
        }
    }
}