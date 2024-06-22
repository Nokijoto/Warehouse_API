using SeederLib;
using Warehouse_API.Dto;
using Warehouse_API.Entities;
using Warehouse_API.Extensions.Dtos;

namespace Warehouse_API.FakeDataGenerator
{
    public class Seeder
    {
        private readonly WarehouseDbContext _context;
        private readonly DataGenerator _dataGenerator;
        private readonly ILogger<Seeder> _logger;

        public Seeder(WarehouseDbContext context, DataGenerator dataGenerator, ILogger<Seeder> logger)
        {
            _context = context;
            _dataGenerator = dataGenerator;
            _logger = logger;
        }

        public async Task SeedData()
        {
            try
            {
                _logger.LogInformation("Starting data seeding...");

                if (!_context.Products.Any())
                {
                    _logger.LogInformation("Seeding products...");

                    // Generate fake products
                    List<Product> fakeProducts = _dataGenerator.GenerateFakeProducts(20);

                    // Add generated products to the database context
                    await _context.Products.AddRangeAsync(fakeProducts);
                }
                else
                {
                    _logger.LogInformation("Products already exist in the database.");
                }

                if (!_context.RFIDTags.Any())
                {
                    _logger.LogInformation("Seeding RFID tags...");

                    // Generate fake RFID tags
                    List<RFIDTag> fakeRFIDTags = _dataGenerator.GenerateFakeRFIDs(20);

                    // Add generated RFID tags to the database context
                    await _context.RFIDTags.AddRangeAsync(fakeRFIDTags);
                }
                else
                {
                    _logger.LogInformation("RFID tags already exist in the database.");
                }

                // Save all changes to the database
                await _context.SaveChangesAsync();
                _logger.LogInformation("Data seeding completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred during data seeding: {ex.Message}");
            }
        }
    }
}
