using AutoMapper;
using Do_Svyazi.Message.Application.Abstractions.DataAccess;
using Do_Svyazi.Message.Application.Abstractions.Services;
using Do_Svyazi.Message.Application.Dto.Chats;
using Do_Svyazi.Message.Application.Dto.Messages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Do_Svyazi.Message.Application.CQRS.Chats.Queries;

public static class GetChatUserState
{
    public record Query(Guid UserId, Guid ChatId) : IRequest<Response>;

    public record Response(ChatUserStateDto ChatUserState);

    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IChatUserService _chatUserService;
        private readonly IMessageDatabaseContext _context;
        private readonly IMapper _mapper;

        public Handler(IChatUserService chatUserService, IMessageDatabaseContext context, IMapper mapper)
        {
            _chatUserService = chatUserService;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var (userId, chatId) = request;

            var chatUser = await _chatUserService
                .GetChatUser(chatId, userId, cancellationToken)
                .ConfigureAwait(false);
            
            IQueryable<Domain.Entities.Message> chatMessages = _context.Messages
                .Where(m => m.Sender.Chat.Equals(chatUser.Chat));

            int count;

            if (chatUser.LastReadMessage is null)
            {
                count = await chatMessages.CountAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                count = await chatMessages
                    .Where(m => m.PostDateTime > chatUser.LastReadMessage.PostDateTime)
                    .CountAsync(cancellationToken)
                    .ConfigureAwait(false);
            }

            var messageDto = _mapper.Map<MessageDto?>(chatUser.LastReadMessage);
            var chatUserStateDto = new ChatUserStateDto(userId, chatId, count, messageDto);

            return new Response(chatUserStateDto);
        }
    }
}