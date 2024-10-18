using StargateAPI.Business.Data;

namespace StargateAPI.Business.Dtos
{
    public class AstronautDutyDTO
    {
        public AstronautDutyDTO() { }
        public AstronautDutyDTO(AstronautDuty? duty)
        {
            if (duty == null)
                return;

            Id = duty.Id;
            PersonId = duty.PersonId;
            DutyName = duty.DutyName;
            DutyStartDate = duty.DutyStartDate;
            DutyEndDate = duty.DutyEndDate;
        }

        public int Id { get; set; }

        public int PersonId { get; set; }

        public string DutyName { get; set; }

        public DateTime DutyStartDate { get; set; }

        public DateTime? DutyEndDate { get; set; }
    }
}
