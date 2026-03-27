using AutoMapper;
using CodeVote.src.DbModels;
using CodeVote.src.DTO;
using Elfie.Serialization;

namespace CodeVote.src.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // project idea
            #region ProjectIdea
            CreateMap<CreateProjectIdeaDTO, ProjectIdeaDbM>();
            CreateMap<ProjectIdeaDbM, ReadProjectIdeaDTO>();
            CreateMap<UpdateProjectIdeaDTO, ProjectIdeaDbM>()
                // Map Title and Description to null if empty/whitespace instead of empty string
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src =>
                string.IsNullOrWhiteSpace(src.Title) ? null : src.Title))

                .ForMember(dest => dest.Description, opt => opt.MapFrom(src =>
                string.IsNullOrWhiteSpace(src.Description) ? null : src.Description))

                // Ignore null values during mapping to prevent overwriting existing values with null
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            #endregion

            // user
            #region User
            CreateMap<CreateUserDTO, UserDbM>();
            CreateMap<UserDbM, ReadUserDTO>();
            CreateMap<UpdateUserDTO, UserDbM>();

                // Map all properties to null if empty/whitespace instead of empty string
                CreateMap<UpdateUserDTO, UserDbM>()
                    .AddTransform<string>(s => string.IsNullOrWhiteSpace(s) ? null : s)

                // Ignore null values during mapping to prevent overwriting existing values with null
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            #endregion

            // vote
            #region Vote
            CreateMap<CreateVoteDTO, VoteDbM>();
            CreateMap<VoteDbM, ReadVoteDTO>();
            #endregion
        }
    }
}

