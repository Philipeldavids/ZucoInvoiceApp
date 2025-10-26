using DataLayer.Repository;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }

        public async Task<Item> AddItem(ItemDTO itemDto)
        {
            var result = await _unitOfWork.ItemRepository.AddItem(itemDto);
            return result ;
        }

        public async Task<List<Item>> GetItemsAsync()
        {
            var items = await _unitOfWork.ItemRepository.GetItems();
            return items;
        }
    }
}
