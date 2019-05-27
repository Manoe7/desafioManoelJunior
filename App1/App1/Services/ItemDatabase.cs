using App1.Models;
using App1.Services;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App1.Services
{
    public class ItemDatabase : IDataStore<Item>
    {
        SQLiteAsyncConnection database;

        public ItemDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Item>().Wait();
        }

        public Task<List<Item>> GetItemsAsync()
        {
            return database.Table<Item>().ToListAsync();
        }

        public Task<List<Item>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Item>("SELECT * FROM [Item] WHERE [Done] = 0");
        }

        public Task<Item> GetItemAsync(string id)
        {
            return database.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> UpdateItemAsync(Item item)
        {
            return database.UpdateAsync(item);
        }

        public Task<int> AddItemAsync(Item item)
        {
            if (!string.IsNullOrEmpty(item.Id))
            {
                return database.UpdateAsync(item);
            }
            else
            {
                item.Id = Guid.NewGuid().ToString();
                item.StartDate = DateTime.Now;
                var statusHistoryList = new List<History>() { new History { Status = item.Status, Date = item.StartDate.ToString() } };
                item.StatusHistory = JsonConvert.SerializeObject(statusHistoryList);
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteAllAsync()
        {
            return database.DeleteAllAsync<Item>();
        }
    }
}
