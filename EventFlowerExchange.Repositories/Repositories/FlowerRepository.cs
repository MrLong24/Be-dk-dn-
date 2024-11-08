using EventFlowerExchange.Repositories.Entities;
using EventFlowerExchange.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace EventFlowerExchange.Repositories.Repositories
{
    public class FlowerRepository : IFlowerRepository
    {
        private readonly EventFlowerExchangeContext _context;

        public FlowerRepository(EventFlowerExchangeContext context)
        {
            _context = context;
        }

        public async Task<Flower> GetFlowerByIdAsync(int id)
        {
            try
            {
                return await _context.Flowers.FirstOrDefaultAsync(f => f.Id == id)
                       ?? throw new KeyNotFoundException("Flower not found");
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving flower", ex);
            }
        }

        public async Task<IEnumerable<Flower>> GetAllFlowersAsync()
        {
            try
            {
                return await _context.Flowers.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving flowers list", ex);
            }
        }

        public async Task AddFlowerAsync(Flower flower)
        {
            if (flower == null)
                throw new ArgumentNullException(nameof(flower));

            // Kiểm tra tính hợp lệ của SellerId
            var seller = await _context.Users.FindAsync(flower.SellerId);
            if (seller == null)
            {
                throw new ArgumentException("Invalid SellerId", nameof(flower.SellerId));
            }

            try
            {
                await _context.Flowers.AddAsync(flower);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Bắt các lỗi khác
                throw new Exception("An error occurred while adding flower", ex);
            }
        }


        public async Task UpdateFlowerAsync(Flower flower)
        {
            if (flower == null)
                throw new ArgumentNullException(nameof(flower));

            try
            {
                _context.Flowers.Update(flower);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating flower in repository", ex);
            }
        }


        public async Task DeleteFlowerAsync(Flower flower)
        {
            if (flower == null)
                throw new ArgumentNullException(nameof(flower));

            try
            {
                _context.Flowers.Remove(flower);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting flower in repository", ex);
            }
        }

    }
}
