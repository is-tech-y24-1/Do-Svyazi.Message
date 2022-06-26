using AutoMapper;
using Do_Svyazi.Message.Application.Dto.Messages;
using Do_Svyazi.Message.Domain.Entities;

namespace Do_Svyazi.Message.Mapping.Profiles;

public class ContentDtosProfile : Profile
{
    public ContentDtosProfile()
    {
        CreateMap<Content, ContentDto>().ReverseMap();
    }
}