using AutoMapper;

using DetectiveGame.Application.Features.Games.Commands;
using DetectiveGame.Application.Features.Evidences.Commands;
using DetectiveGame.Domain.Entities;
using DetectiveGame.Application.Features.Notes.Commands;

namespace DetectiveGame.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Game, GameDto>();
            CreateMap<Player, PlayerDto>();
            CreateMap<Evidence, EvidenceDto>();
            CreateMap<Note, NoteDto>()
                .ForMember(dest => dest.PlayerUsername, 
                          opt => opt.MapFrom(src => src.Player.Username));

            CreateMap<CreateGameCommand, Game>();
            CreateMap<AddEvidenceCommand, Evidence>();
            CreateMap<AddNoteCommand, Note>();
        }
    }
} 