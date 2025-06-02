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

            //user
            #region User
            CreateMap<CreateUserDTO, UserDbM>()
                // Map FirstName and LastName to null if empty/whitespace instead of empty string
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src =>
                string.IsNullOrWhiteSpace(src.FirstName) ? null : src.FirstName))
                                                                                  
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src =>         
                string.IsNullOrWhiteSpace(src.LastName) ? null : src.LastName));

            CreateMap<UserDbM, ReadUserDTO>();

            CreateMap<UpdateUserDTO, UserDbM>()
                // Map all properties to null if empty/whitespace instead of empty string
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src =>
                string.IsNullOrWhiteSpace(src.FirstName) ? null : src.FirstName)) 

                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src =>
                string.IsNullOrWhiteSpace(src.LastName) ? null : src.LastName))

                .ForMember(dest => dest.Email, opt => opt.MapFrom(src =>
                string.IsNullOrWhiteSpace(src.Email) ? null : src.Email))

                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src =>
                string.IsNullOrWhiteSpace(src.UserName) ? null : src.UserName))

                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src =>
                string.IsNullOrWhiteSpace(src.PasswordHash) ? null : src.PasswordHash))

                // Ignore null values during mapping to prevent overwriting existing values with null
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            #endregion

            //vote
            #region Vote
            CreateMap<CreateVoteDTO, VoteDbM>();

            CreateMap<VoteDbM, ReadVoteDTO>();
            #endregion

        }
    }
}

