using EventFlowerExchange.Repositories.Entities;
using EventFlowerExchange.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlowerExchange.services.Services
{
    public class FlowerService : IFlowerService
    {
        private readonly IFlowerRepository _flowerRepository;

        public FlowerService(IFlowerRepository flowerRepository)
        {
            _flowerRepository = flowerRepository ?? throw new ArgumentNullException(nameof(flowerRepository));
        }

        public async Task<Flower> GetFlowerByIdAsync(int id)
        {
            try
            {
                return await _flowerRepository.GetFlowerByIdAsync(id);
            }
            catch (KeyNotFoundException knfEx)
            {
                throw new KeyNotFoundException(knfEx.Message, knfEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in FlowerService while retrieving flower by ID", ex);
            }
        }

        public async Task<IEnumerable<Flower>> GetAllFlowersAsync()
        {
            try
            {
                return await _flowerRepository.GetAllFlowersAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in FlowerService while retrieving all flowers", ex);
            }
        }

        public async Task AddFlowerAsync(Flower flower)
        {
            if (flower == null) throw new ArgumentNullException(nameof(flower));

            try
            {
                await _flowerRepository.AddFlowerAsync(flower);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in FlowerService while adding flower", ex);
            }
        }

        public async Task UpdateFlowerAsync(Flower flower)
        {
            if (flower == null) throw new ArgumentNullException(nameof(flower));

            try
            {
                await _flowerRepository.UpdateFlowerAsync(flower);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in FlowerService while updating flower", ex);
            }
        }

        public async Task DeleteFlowerAsync(int id)
        {
            try
            {
                await _flowerRepository.DeleteFlowerAsync(id);
            }
            catch (KeyNotFoundException knfEx)
            {
                throw new KeyNotFoundException(knfEx.Message, knfEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in FlowerService while deleting flower", ex);
            }
        }
    }
}
