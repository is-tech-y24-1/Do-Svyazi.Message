using AutoMapper;
using Do_Svyazi.Message.Application.Abstractions.DataAccess;
using Do_Svyazi.Message.Application.Abstractions.Integrations;
using Do_Svyazi.Message.Application.Abstractions.Services;
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
        IReadOnlyCollection<ContentDto> Contents) : IRequest<Response>;

    public record Response(ForwardedMessageDto Message);

    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly IMessageDatabaseContext _context;
        private readonly IChatUserService _chatUserService;
        private readonly IMessageService _messageService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;

        public Handler(
            IMessageDatabaseContext context,
            IChatUserService chatUserService,
            IAuthorizationService authorizationService,
            IMapper mapper,
            IMessageService messageService,
            IDateTimeService dateTimeService)
        {
            _context = context;
            _chatUserService = chatUserService;
            _authorizationService = authorizationService;
            _mapper = mapper;
            _messageService = messageService;
            _dateTimeService = dateTimeService;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var (userId, chatId, forwardedMessageId, text, contentDtos) = request;
            var postDateTime = _dateTimeService.GetCurrent();

            var chatUser = await _chatUserService
                .GetChatUser(chatId, userId, cancellationToken)
                .ConfigureAwait(false);

            await _authorizationService
                .AuthorizeMessageSendAsync(chatUser.User, chatUser.Chat, cancellationToken)
                .ConfigureAwait(false);

            var forwardedMessage = await _messageService
                .GetMessageAsync(forwardedMessageId, cancellationToken)
                .ConfigureAwait(false);

            IEnumerable<Content> contents = contentDtos.Select(_mapper.Map<Content>);
            var message = new ForwardedMessage(chatUser, text, postDateTime, contents, forwardedMessage);

            _context.Messages.Add(message);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var messageDto = _mapper.Map<ForwardedMessageDto>(message);
            return new Response(messageDto);
        }
    }
}