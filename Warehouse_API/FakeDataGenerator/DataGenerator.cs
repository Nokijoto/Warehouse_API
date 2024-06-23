using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Warehouse_API.Dto.CreationsDto;
using Warehouse_API.Entities;

namespace SeederLib
{
    public class DataGenerator
    {
        private readonly Faker<Product> _productFaker;
        private readonly Faker<RFIDTag> _rfidTagFaker;
        public DataGenerator()
        {
            Randomizer.Seed = new Random(1234567890);
       

            _productFaker = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
                .RuleFor(p => p.CreatedAt, f => f.Date.Past(2))
                .RuleFor(p => p.UpdatedAt, f => f.Date.Past(1))
                .RuleFor(p => p.CreatedBy, f => f.Internet.UserName())
                .RuleFor(p => p.UpdatedBy, f => f.Internet.UserName())
                .RuleFor(p => p.Guid, f => f.Random.Guid());

            _rfidTagFaker = new Faker<RFIDTag>()
                .RuleFor(r => r.TagNumber, f => f.Random.AlphaNumeric(10))
                .RuleFor(r => r.CreatedAt, f => f.Date.Past(2))
                .RuleFor(r => r.UpdatedAt, f => f.Date.Past(1))
                .RuleFor(r => r.CreatedBy, f => f.Internet.UserName())
                .RuleFor(r => r.UpdatedBy, f => f.Internet.UserName())
                .RuleFor(r => r.Guid, f => f.Random.Guid());

            // Ustawienie unikalnych RFIDTagId w oparciu o zwiększający się licznik
            int rfidTagIdCounter = 1;
            _productFaker.RuleFor(p => p.RFIDTagId, () => rfidTagIdCounter++);

            // Można również użyć unikalnego indeksu Bogus dla RFIDTagId:
            // _productFaker.RuleFor(p => p.RFIDTagId, f => f.UniqueIndex);

            _productFaker.RuleFor(p => p.RFIDTag, () => _rfidTagFaker.Generate());
        }

        public List<Product> GenerateFakeProducts(int count)
        {
            return _productFaker.Generate(count);
        }

        public List<RFIDTag> GenerateFakeRFIDs(int count)
        {
            return _rfidTagFaker.Generate(count);
        }
    }
}
