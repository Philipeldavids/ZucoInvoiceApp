using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IItemService
    {
        Task<Item> AddItem(ItemDTO itemDto);
        Task<List<Item>> GetItemsAsync();
    }
}
