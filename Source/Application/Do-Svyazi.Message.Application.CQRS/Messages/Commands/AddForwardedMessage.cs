using AutoMapper;
using Do_Svyazi.Message.Application.Abstractions.DataAccess;
using Do_Svyazi.Message.Application.Abstractions.EntityManagers;
using Do_Svyazi.Message.Application.Abstractions.Integrations;
using Do_Svyazi.Message.Application.CQRS.Exceptions;
using Do_Svyazi.Message.Application.Dto.Messages;
using Do_Svyazi.Message.Domain.Entities;
using MediatR;

namespace Do_Svyazi.Message.Application.CQRS.Messages.Commands;

public static class AddForwardedMessage
{
    public record Command(
        Guid UserId,
        Guid ChatId,
        Guid ForwardedMessageId,
        string Text,
        DateTime PostDateTime,
        IReadOnlyCollection<ContentDto> Contents) : IRequest<Response>;

    public record Response(ForwardedMessageDto Message);

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
            var (userId, chatId, forwardedMessageId, text, postDateTime, contentDtos) = request;

            var chatUser = await _chatUserManager
                .GetChatUser(chatId, userId, cancellationToken)
                .ConfigureAwait(false);

            await _authorizationService
                .AuthorizeMessageSendAsync(chatUser.User, chatUser.Chat, cancellationToken)
                .ConfigureAwait(false);

            var forwardedMessage = await _context.Messages
                .FindAsync(new object[] { forwardedMessageId }, cancellationToken)
                .ConfigureAwait(false);

            if (forwardedMessage is null)
                throw new MessageNotFoundException(forwardedMessageId);

            IEnumerable<Content> contents = contentDtos.Select(_mapper.Map<Content>);
            var message = new ForwardedMessage(chatUser, text, postDateTime, contents, forwardedMessage);

            _context.Messages.Add(message);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var messageDto = _mapper.Map<ForwardedMessageDto>(message);
            return new Response(messageDto);
        }
    }
}