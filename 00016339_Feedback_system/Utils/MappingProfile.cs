using _00016339_Feedback_system.DTO;
using _00016339_Feedback_system.Models;
using AutoMapper;

namespace _00016339_Feedback_system.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FeedbackDto, Feedback>().ReverseMap();
            CreateMap<Sender, SenderDto>().ReverseMap();
        }
    }
}
