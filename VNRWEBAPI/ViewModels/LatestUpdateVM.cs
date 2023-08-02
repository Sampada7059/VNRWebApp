namespace VNRWEBAPI.ViewModels
{
    public class LatestUpdateVM
    {
        public int Id { get; set; }
        public string Information { get; set; }
        public DateTime Created_Date { get; set; }
        public int CreatedBy { get; set; }
        public string? Name { get; set; }
    }
}
