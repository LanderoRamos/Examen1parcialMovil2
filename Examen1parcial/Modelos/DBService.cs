using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen1parcial.Modelos
{
    public class DBService
    {

        readonly SQLiteAsyncConnection _connection;

        public DBService()
        {
            SQLite.SQLiteOpenFlags extensiones = SQLiteOpenFlags.ReadWrite |
                SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;

            _connection = new SQLiteAsyncConnection(
                Path.Combine(
                    FileSystem.AppDataDirectory,
                    "DBPerson.db3"), extensiones);

            _connection.CreateTableAsync<Lugares>();
        }

        public async Task<int> StoreLugares(Lugares lugares)
        {
            if (lugares.Id == 0)
            {
                return await _connection.InsertAsync(lugares);
            }
            else
            {
                return await _connection.UpdateAsync(lugares);
            }
        }
        //read
        public async Task<List<Modelos.Lugares>> GetLugares()
        {
            return await _connection.Table<Modelos.Lugares>().ToListAsync();
        }
        //read ID
        public async Task<Modelos.Lugares> GetLugares(int pid)
        {
            return await _connection.Table<Modelos.Lugares>().
               Where(i => i.Id == pid).FirstOrDefaultAsync();
        }
        //delete
        public async Task<int> DeleteLugares(Modelos.Lugares lugares)
        {
            return await _connection.DeleteAsync(lugares);
        }

    }
}
