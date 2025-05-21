using AutoMapper;
using CodeVote.DbModels;
using CodeVote.Models.DTO;

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
            CreateMap<VoteDbM, ReadVoteDTO>();
        }
    }
}

