using AutoMapper;
using AutoMapper.QueryableExtensions;
using Do_Svyazi.Message.Application.Abstractions.DataAccess;
using Do_Svyazi.Message.Application.Abstractions.EntityManagers;
using Do_Svyazi.Message.Application.Abstractions.Integrations;
using Do_Svyazi.Message.Application.Dto.Messages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Do_Svyazi.Message.Application.CQRS.Messages.Queries;

public static class GetChatMessages
{
    public record Query(Guid UserId, Guid ChatId, DateTime Cursor, int Count) : IRequest<Response>;

    public record Response(IReadOnlyCollection<MessageDto> Messages, DateTime Cursor);

    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IMessageDatabaseContext _context;
        private readonly IChatUserManager _chatUserManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        public Handler(
            IMessageDatabaseContext context,
            IAuthorizationService authorizationService,
            IMapper mapper,
            IChatUserManager chatUserManager)
        {
            _context = context;
            _authorizationService = authorizationService;
            _mapper = mapper;
            _chatUserManager = chatUserManager;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var (userId, chatId, cursor, count) = request;

            var chatUser = await _chatUserManager
                .GetChatUser(chatId, userId, cancellationToken)
                .ConfigureAwait(false);

            await _authorizationService
                .AuthorizeMessageReadAsync(chatUser.User, chatUser.Chat, cancellationToken)
                .ConfigureAwait(false);

            MessageDto[] messages = await _context.Messages
                .Where(m => m.Sender.Chat.Equals(chatUser.Chat))
                .OrderBy(m => m.PostDateTime)
                .Where(m => m.PostDateTime >= cursor)
                .Take(count)
                .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);

            var newCursor = messages.Length is 0
                ? cursor
                : messages[^1].PostDateTime;

            return new Response(messages, newCursor);
        }
    }
}