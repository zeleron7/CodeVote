using AutoMapper;
using CodeVote.DbModels;
using CodeVote.DTO;

namespace CodeVote.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //project idea
            CreateMap<CreateProjectIdeaDTO, ProjectIdeaDbM>();
            CreateMap<ProjectIdeaDbM, ReadProjectIdeaDTO>();
            CreateMap<UpdateProjectIdeaDTO, ProjectIdeaDbM>();

            //user
            CreateMap<CreateUserDTO, UserDbM>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src =>
                string.IsNullOrWhiteSpace(src.FirstName) ? null : src.FirstName)) // Map FirstName and LastName to null if empty/whitespace instead of empty string                                                                 
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src =>
                string.IsNullOrWhiteSpace(src.LastName) ? null : src.LastName));

            CreateMap<UserDbM, ReadUserDTO>();
            CreateMap<UpdateUserDTO, UserDbM>();

            //vote
            CreateMap<CreateVoteDTO, VoteDbM>();
            CreateMap<VoteDbM, ReadVoteDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.ProjectIdeaDbM.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ProjectIdeaDbM.Description)); 
        }
    }
}

