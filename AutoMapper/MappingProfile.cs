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
            CreateMap<CreateUserDTO, UserDbM>();
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

