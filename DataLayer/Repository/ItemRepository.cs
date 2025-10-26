using DataLayer.Database;
using DataLayer.Interface;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ItemRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Item> AddItem(ItemDTO itemDto)
        {
            if (itemDto == null)
            {
                return null;
            }
            Item item = new Item()
            {
                
                Description = itemDto.Description,
                Quantity = itemDto.Quantity,
                Discount = itemDto.Discount,
                UnitPrice = itemDto.UnitPrice,
                Amount = itemDto.Amount
            };
            _applicationDbContext.Items.Add(item);
            await _applicationDbContext.SaveChangesAsync();

            return item;
        }
        public async Task<List<Item>> GetItems()
        {
            List<Item> items = new List<Item>();    

            var data = from item in _applicationDbContext.Items
                       select new Item()
                       {   Id = item.Id,                     
                           Description = item.Description,
                           Quantity = item.Quantity,
                           Discount = item.Discount,
                           UnitPrice = item.UnitPrice,
                           Amount = item.Amount,
                       };
            return data.ToList();
        }

        public async Task<bool> UpdateItem(Item item)
        {
            var itm = await _applicationDbContext.Items.FirstOrDefaultAsync(itm => itm.Id == item.Id);
            if (itm == null)
            {
                return false;
            }
            _applicationDbContext.Items.Update(itm);
            var result = await _applicationDbContext.SaveChangesAsync();
            return result > 0;
        }

    }
}
