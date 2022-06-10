using AutoMapper;
using Do_Svyazi.Message.Application.Dto.Messages;
using Do_Svyazi.Message.Domain.Entities;
using ContentType = Do_Svyazi.Message.Domain.Entities.ContentType;
using ContentTypeDto = Do_Svyazi.Message.Application.Dto.Messages.ContentType;

namespace Do_Svyazi.Message.Mapping.Profiles;

public class MessageDtosProfile : Profile
{
    public MessageDtosProfile()
    {
        CreateMap<Content, ContentDto>();
        CreateMap<ContentType, ContentTypeDto>();
        CreateMap<Domain.Entities.Message, MessageDto>();
        CreateMap<ForwardedMessage, ForwardedMessageDto>();
    }
}