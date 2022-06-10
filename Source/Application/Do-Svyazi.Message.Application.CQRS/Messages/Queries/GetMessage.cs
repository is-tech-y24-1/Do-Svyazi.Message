using AutoMapper;
using Do_Svyazi.Message.Application.Abstractions.Integrations;
using Do_Svyazi.Message.Application.Abstractions.Services;
using Do_Svyazi.Message.Application.Dto.Messages;
using MediatR;

namespace Do_Svyazi.Message.Application.CQRS.Messages.Queries;

public static class GetMessage
{
    public record Query(Guid UserId, Guid MessageId) : IRequest<Response>;

    public record Response(MessageDto Message);

    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly IAuthorizationService _authorizationService;

        public Handler(
            IMapper mapper,
            IUserService userService,
            IMessageService messageService,
            IAuthorizationService authorizationService)
        {
            _userService = userService;
            _messageService = messageService;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var (userId, messageId) = request;

            var user = await _userService
                .GetUserAsync(userId, cancellationToken)
                .ConfigureAwait(false);
            
            var message = await _messageService
                .GetMessageAsync(messageId, cancellationToken)
                .ConfigureAwait(false);

            await _authorizationService
                .AuthorizeMessageReadAsync(user, message.Sender.Chat, cancellationToken)
                .ConfigureAwait(false);

            var messageDto = _mapper.Map<MessageDto>(message);
            return new Response(messageDto);
        }
    }
}