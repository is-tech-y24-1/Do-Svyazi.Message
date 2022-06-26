using System;
using System.Linq;
using AutoMapper;
using Do_Svyazi.Message.Application.Dto.Chats;
using Do_Svyazi.Message.Domain.Entities;
using Do_Svyazi.Message.Mapping.Extensions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Do_Svyazi.Message.Tests.Mapping;

public class MappingTests
{
    private IMapper _mapper = null!;

    [SetUp]
    public void Setup()
    {
        var collection = new ServiceCollection();
        collection.AddMapping();

        var provider = collection.BuildServiceProvider();
        _mapper = provider.GetRequiredService<IMapper>();
    }

    [Test]
    public void ChatUserDtoMappingTest_ChatUserMappedToChatUserDto_NoExceptionThrown()
    {
        var user = new User(Guid.Parse("159A90AB-CBCD-4798-8D65-03B1DF11FF54"));
        var chat = new Chat(Guid.Parse("7BE0121C-99E8-4378-A8D5-8C44AF83A510"));
        var chatUser = new ChatUser(user, chat);
        
        var chatUserDto = _mapper.Map<ChatUserDto>(chatUser);

        Assert.AreEqual(chatUser.Chat.Id, chatUserDto.ChatId);
        Assert.AreEqual(chatUser.User.Id, chatUserDto.UserId);
        Assert.AreEqual(chatUser.LastReadMessage?.Id, chatUserDto.LastReadMessage?.Id);
        Assert.AreEqual(chatUser.UserMessages.MaxBy(m => m.PostDateTime)?.Id, chatUserDto.LastMessage?.Id);
    }
}