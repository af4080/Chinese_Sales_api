
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

        private static ReadGiftDto MapToReadDto(Gift g)
        {
            return new ReadGiftDto
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                Price = g.Price,
                ImagePath = g.ImagePath,
                CategoryId = g.CategoryId,
                CategoryName = g.Category.Name,
                DonerId = g.DonerId,
                DonerName = g.Doner.Name
            };
        }


        //get
        public async Task<IEnumerable<ReadGiftDto>> GetAllGifts()
        {
            var gifts = await _repository.GetAllGifts();
            var dtos = gifts.Select(d => MapToReadDto(d));

            return dtos;

        }

        //get by name
        public async Task<ReadGiftDto?> GetGiftByName(string name)
        {

            var g = await _repository.GetGiftByName(name);
            if (g == null) return null;
            return MapToReadDto(g);
        }
        //get by doner
        public async Task<IEnumerable<ReadGiftDto?>> GetGiftByDonnerName(string name)
        {
            var g = await _repository.GetGiftByDonnerName(name);
            return g.Select(d => MapToReadDto(d));
        }
        //get by num customer
        public async Task<IEnumerable<ReadGiftDto?>> GetbyNumCustomer(int count)
        {
            var g = await _repository.GetbyNumCustomer(count);
            return g.Select(d => MapToReadDto(d));
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

            return MapToReadDto(createdGift);


        }

        //update
        public async Task<ReadGiftDto?> UpdateGift(string name, UpdateGiftDto dto)
        {
            var existingGift = await _repository.GetGiftByName(name);
            if (existingGift == null)
                return null;

            if (dto.Name != null)
                existingGift.Name = dto.Name;

            if (dto.Price.HasValue)
                existingGift.Price = dto.Price.Value;

            if (dto.CategoryId.HasValue)
                existingGift.CategoryId = dto.CategoryId.Value;

            if (dto.ImagePath != null)
                existingGift.ImagePath = dto.ImagePath;

            if (dto.Description != null)
                existingGift.Description = dto.Description;

            if (existingGift.WinnerId != null)
                throw new InvalidOperationException("Cannot update gift after lottery");


            // DonerId לא משתנה

            var updated = await _repository.UpdateGift(existingGift);
            if (updated == null)
                return null;

            return MapToReadDto(updated);

        }


        //delete
        public async Task<ReadGiftDto?> DeleteGift(int id)
        {
            var del = await _repository.DeleteGift(id);
            if (del == null) return null;
            return MapToReadDto(del);


        }
    }
}
