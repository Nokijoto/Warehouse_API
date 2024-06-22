namespace Warehouse_API.Dto
{
    public class LogsDto
    {
        public string User { get; set; }
        public string Message { get; set; }
        public string LogType { get; set; }
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
