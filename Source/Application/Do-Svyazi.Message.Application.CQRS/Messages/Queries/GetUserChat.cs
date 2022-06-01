using AutoMapper;
using Do_Svyazi.Message.Application.Abstractions.Services;
using Do_Svyazi.Message.Application.Dto.Chats;
using MediatR;

namespace Do_Svyazi.Message.Application.CQRS.Messages.Queries;

public static class GetUserChat
{
    public record Query(Guid UserId, Guid ChatId) : IRequest<Response>;

    public record Response(ChatUserDto ChatUser);

    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IChatUserService _chatUserService;
        private readonly IMapper _mapper;

        public Handler(IMapper mapper, IChatUserService chatUserService)
        {
            _mapper = mapper;
            _chatUserService = chatUserService;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var (userId, chatId) = request;
            var chatUser = await _chatUserService
                .GetChatUser(chatId, userId, cancellationToken)
                .ConfigureAwait(false);

            var chatUserDto = _mapper.Map<ChatUserDto>(chatUser);

            return new Response(chatUserDto);
        }
    }
}