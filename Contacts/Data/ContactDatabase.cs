using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Contacts.Models;
using System.IO;

namespace Contacts.Data
{
    public class ContactDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public ContactDatabase(string dbPath)
        {
            if (!File.Exists(dbPath))
            {
                _database = new SQLiteAsyncConnection(
                dbPath.Substring(dbPath.LastIndexOf('\\') + 1),
                SQLiteOpenFlags.Create |
                SQLiteOpenFlags.FullMutex |
                SQLiteOpenFlags.ReadWrite);
            }
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Contact>().Wait();
        }

        public Task<List<Contact>> GetContactsAsync()
        {
            return _database.Table<Contact>().ToListAsync();
        }

        public Task<Contact> GetContactAsync(int id)
        {
            return _database.Table<Contact>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveContactAsync(Contact Contact)
        {
            if (Contact.ID != 0)
            {
                return _database.UpdateAsync(Contact);
            }
            else
            {
                return _database.InsertAsync(Contact);
            }
        }

        public Task<int> DeleteContactAsync(Contact Contact)
        {
            return _database.DeleteAsync(Contact);
        }
    }
}