using Microsoft.AspNetCore.Mvc;

namespace Warehouse_API.Extensions.Dtos
{
    public class DateRange 
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
