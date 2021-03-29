using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScanDemo.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(long id);
        Task<T> GetItemByCodeAsync(string code);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<List<T>> GetAllItems();
        Task<bool> AddItem(T item);
    }
}
