using Common.Dto;

namespace Warehouse_API.Dto
{
    public class UserDTO:BaseDto
    {
        string Name { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string Role { get; set; }



    }
}
