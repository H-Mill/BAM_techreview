namespace StargateAPI.Business.Dtos
{
    public class AstronautDetailDTO
    {
        public AstronautDetailDTO() { }

        public string CurrentRank { get; set; }

        public DateTime CareerStartDate { get; set; }

        public DateTime? CareerEndDate { get; set; }
    }
}
