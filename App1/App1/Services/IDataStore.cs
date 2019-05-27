using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App1.Services
{
    public interface IDataStore<T>
    {
        Task<int> AddItemAsync(T item);
        Task<int> UpdateItemAsync(T item);
        Task<int> DeleteAllAsync();
        Task<T> GetItemAsync(string id);
        Task<List<T>> GetItemsAsync();
    }
}
