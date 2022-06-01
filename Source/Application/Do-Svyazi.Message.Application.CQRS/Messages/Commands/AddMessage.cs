using AutoMapper;
using Do_Svyazi.Message.Application.Abstractions.DataAccess;
using Do_Svyazi.Message.Application.Abstractions.EntityManagers;
using Do_Svyazi.Message.Application.Abstractions.Integrations;
using Do_Svyazi.Message.Application.Dto.Messages;
using Do_Svyazi.Message.Domain.Entities;
using MediatR;

namespace Do_Svyazi.Message.Application.CQRS.Messages.Commands;

public static class AddMessage
{
    public record Command(
        Guid UserId,
        Guid ChatId,
        string Text,
        DateTime PostDateTime,
        IReadOnlyCollection<ContentDto> Contents) : IRequest<Response>;

    public record Response(MessageDto Message);

    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly IMessageDatabaseContext _context;
        private readonly IChatUserManager _chatUserManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        public Handler(
            IMessageDatabaseContext context,
            IChatUserManager chatUserManager,
            IAuthorizationService authorizationService,
            IMapper mapper)
        {
            _context = context;
            _chatUserManager = chatUserManager;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var (userId, chatId, text, postDateTime, contentDtos) = request;

            var chatUser = await _chatUserManager
                .GetChatUser(chatId, userId, cancellationToken)
                .ConfigureAwait(false);

            await _authorizationService
                .AuthorizeMessageSendAsync(chatUser.User, chatUser.Chat, cancellationToken)
                .ConfigureAwait(false);

            IEnumerable<Content> contents = contentDtos.Select(_mapper.Map<Content>);
            var message = new Domain.Entities.Message(chatUser, text, postDateTime, contents);

            _context.Messages.Add(message);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var messageDto = _mapper.Map<MessageDto>(message);
            return new Response(messageDto);
        }
    }
}