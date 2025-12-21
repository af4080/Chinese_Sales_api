using Microsoft.EntityFrameworkCore;
using projectApiAngular.Models;
using projectApiAngular.Repositories;
using static projectApiAngular.DTO.GiftDto;

namespace projectApiAngular.Services
{
    public class GiftService : IGiftService
    {
        private readonly IGiftRepository _repository;

        public GiftService(IGiftRepository repository)
        {
            _repository = repository;
        }

        //get
        public async Task<IEnumerable<ReadGiftDto>> GetAllGifts()
        {
            var gifts = await _repository.GetAllGifts();
            if (gifts == null) return Enumerable.Empty<ReadGiftDto>();
            var dtos = gifts.Select(d => new ReadGiftDto
            {
                Name = d.Name,
                Description = d.Description,
                Id = d.Id,
                Price = d.Price,
                ImagePath = d.ImagePath,
                CategoryName = d.category.Name,
                DonerName = d.Doner.Name
            });

            return dtos;

        }

        //get by name
        public async Task<ReadGiftDto?> GetGiftByName(string name)
        {

            var g = await _repository.GetGiftByName(name);
            if (g == null) return null;
            return new ReadGiftDto
            {
                Name = g.Name,
                Description = g.Description,
                Id = g.Id,
                Price = g.Price,
                ImagePath = g.ImagePath,
                CategoryName = g.category.Name,
                DonerName = g.Doner.Name
            };
        }
        //get by doner
        public async Task<IEnumerable<ReadGiftDto?>> GetGiftByDonnerName(string name)
        {
            var g = await _repository.GetGiftByDonnerName(name);
            return g.Select(d => new ReadGiftDto
            {
                Name = d.Name,
                Description = d.Description,
                Id = d.Id,
                Price = d.Price,
                ImagePath = d.ImagePath,
                CategoryName = d.category.Name,
                DonerName = d.Doner.Name
            });
        }
        //get by num customer
        public async Task<IEnumerable<ReadGiftDto?>> GetbyNumCastomer(int count)
        {
            var g = await _repository.GetbyNumCastomer(count);
            return g.Select(d => new ReadGiftDto
            {
                Name = d.Name,
                Description = d.Description,
                Id = d.Id,
                Price = d.Price,
                ImagePath = d.ImagePath,
                CategoryName = d.category.Name,
                DonerName = d.Doner.Name
            });
        }
        //post
        public async Task<ReadGiftDto> AddGift(CreateGiftDto gift)
        {
            var entity = new Gift
            {
                Name = gift.Name,
                Price = gift.Price,
                CategoryId = gift.CategoryId,
                ImagePath = gift.ImagePath,
                Description = gift.Description,
                DonerId = gift.DonerId
            };
            var createdGift = await _repository.AddGift(entity);
            return new ReadGiftDto
            {
                Name = createdGift.Name,
                Price = createdGift.Price,
                CategoryName = createdGift.category.Name,
                ImagePath = createdGift.ImagePath,
                Description = createdGift.Description,
                DonerName = createdGift.Doner.Name
            };
        }

        //update
        public async Task<ReadGiftDto?> UpdateGift(string name, UpdateGiftDto gift)
        {
            var existingGift = await _repository.GetGiftByName(name);
            if (existingGift == null) return null;
            var entity = new Gift
            {
                Name = gift.Name ?? existingGift.Name,
                Price = gift.Price ?? existingGift.Price,
                CategoryId = gift.CategoryId ?? existingGift.CategoryId,
                ImagePath = gift.ImagePath ?? existingGift.ImagePath,
                Description = gift.Description ?? existingGift.Description,
                DonerId = gift.DonerId ?? existingGift.DonerId
            };
            var updated = await _repository.UpdateGift(name, entity);
            return new ReadGiftDto
            {
                Name = name,
                Price = updated.Price,
                ImagePath = updated.ImagePath,
                Description = updated.Description,
                CategoryName = updated.category.Name,
                DonerName = updated.Doner.Name,
            };
        }

        //delete
        public async Task<ReadGiftDto?> DeleteGift(int id)
        {
            var del = await _repository.DeleteGift(id);
            if (del == null) return null;
            return new ReadGiftDto { Name = del.Name, Price = del.Price, ImagePath = del.ImagePath, Description = del.Description, CategoryName = del.category.Name, DonerName = del.Doner.Name };
        }

    }
}
