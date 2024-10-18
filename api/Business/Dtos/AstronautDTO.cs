using StargateAPI.Business.Data;

namespace StargateAPI.Business.Dtos
{
    public class AstronautDTO
    {
        public AstronautDTO() { }
        public AstronautDTO(Person person)
        {
            if (person == null)
                return;

            Id = person.Id;
            Name = person.Name;

            if (person.AstronautDetail != null){
                AstronautDetail = new AstronautDetailDTO
                {
                    CareerStartDate = person.AstronautDetail.CareerStartDate,
                    CareerEndDate = person.AstronautDetail.CareerEndDate,
                    CurrentRank = person.AstronautDetail.CurrentRank
                };
            }

            if (person.AstronautDuties != null){
                AstronautDuties = person.AstronautDuties
                    .OrderByDescending(d => d.DutyStartDate)
                    .Select(ad => new AstronautDutyDTO(ad))
                    .ToList();
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public AstronautDetailDTO AstronautDetail { get; set; } = new AstronautDetailDTO();

        public List<AstronautDutyDTO> AstronautDuties { get; set; } = new List<AstronautDutyDTO> ();
    }
}
