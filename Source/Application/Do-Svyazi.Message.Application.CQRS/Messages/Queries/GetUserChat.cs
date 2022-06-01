using AutoMapper;
using Do_Svyazi.Message.Application.Abstractions.EntityManagers;
using Do_Svyazi.Message.Application.Dto.Chats;
using MediatR;

namespace Do_Svyazi.Message.Application.CQRS.Messages.Queries;

public static class GetUserChat
{
    public record Query(Guid UserId, Guid ChatId) : IRequest<Response>;

    public record Response(ChatUserDto ChatUser);

    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IChatUserManager _chatUserManager;
        private readonly IMapper _mapper;

        public Handler(IMapper mapper, IChatUserManager chatUserManager)
        {
            _mapper = mapper;
            _chatUserManager = chatUserManager;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var (userId, chatId) = request;
            var chatUser = await _chatUserManager
                .GetChatUser(chatId, userId, cancellationToken)
                .ConfigureAwait(false);

            var chatUserDto = _mapper.Map<ChatUserDto>(chatUser);

            return new Response(chatUserDto);
        }
    }
}