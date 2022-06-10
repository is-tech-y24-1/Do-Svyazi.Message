using AutoMapper;
using Do_Svyazi.Message.Application.Dto.Chats;
using Do_Svyazi.Message.Domain.Entities;

namespace Do_Svyazi.Message.Mapping.Profiles;

public class ChatDtosProfile : Profile
{
    public ChatDtosProfile()
    {
        CreateMap<ChatUser, ChatUserDto>()
            .ForCtorParam(nameof(ChatUserDto.LastMessage),
                opt => opt.MapFrom(src => src.UserMessages.MaxBy(m => m.PostDateTime)));
    }
}