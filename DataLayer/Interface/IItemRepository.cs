using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interface
{
    public interface IItemRepository
    {
        Task<Item> AddItem(ItemDTO itemDto);
        Task<List<Item>> GetItems();
        Task<bool> UpdateItem(Item item);
    }
}
