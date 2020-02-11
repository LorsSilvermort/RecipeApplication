using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WindowsTestApp.Models;

namespace WindowsTestApp.Data
{
    public class NoteDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public NoteDatabase(string dbPath) {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Note>().Wait();
        }

        public Task<List<Note>> GetNotesAsync() {
            return _database.Table<Note>().ToListAsync();
        }
        public Task<Note> GetNoteAsync(int id) => _database.Table<Note>().Where(i => i.ID == id).FirstOrDefaultAsync();

        public Task<int> SaveNoteAsync(Note n) {
            if (n.ID != 0) return _database.UpdateAsync(n);
            else return _database.InsertAsync(n);
        }
        public Task<int> DeleteNoteAsync(Note n) => _database.DeleteAsync(n);

    }
}
