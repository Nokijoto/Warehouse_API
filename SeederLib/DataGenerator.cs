using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Warehouse_API.Dto.CreationsDto;

namespace SeederLib
{
    public class DataGenerator
    {
        Faker<CreateProductDto> FakeProducts;
        Faker<CreateRFIDDto> FakeRFID;

        public DataGenerator()
        {
                Randomizer.Seed = new Random(1234567890);

            FakeProducts = new Faker<CreateProductDto>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
                .RuleFor(p => p.RFIDTagTagNumber, f => f.Random.AlphaNumeric(10));

            FakeRFID = new Faker<CreateRFIDDto>()
                .RuleFor(r => r.TagNumber, f => f.Random.AlphaNumeric(10));

        }


        public CreateProductDto GetFakeProduct()
        {
            return FakeProducts.Generate();
        }


        public CreateRFIDDto GetFakeRFID()
        {
            return FakeRFID.Generate();
        }   


    }
}
