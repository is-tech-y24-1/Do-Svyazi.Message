using AutoMapper;
using Do_Svyazi.Message.Application.Abstractions.DataAccess;
using Do_Svyazi.Message.Application.Abstractions.Integrations;
using Do_Svyazi.Message.Application.CQRS.Exceptions;
using Do_Svyazi.Message.Application.Dto.Messages;
using Do_Svyazi.Message.Domain.Entities;
using MediatR;

namespace Do_Svyazi.Message.Application.CQRS.Messages.Commands;

public static class UpdateMessageContent
{
    public record Command(Guid UserId, Guid MessageId, IReadOnlyCollection<ContentDto> NewContent) : IRequest;

    public class Handler : IRequestHandler<Command>
    {
        private readonly IMessageDatabaseContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        public Handler(IMessageDatabaseContext context, IAuthorizationService authorizationService, IMapper mapper)
        {
            _context = context;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var (userId, messageId, contentDtos) = request;

            var user = await _context.Users
                .FindAsync(new object[] { userId }, cancellationToken)
                .ConfigureAwait(false);

            if (user is null)
                throw new UserNotFoundException(userId);

            var message = await _context.Messages
                .FindAsync(new object[] { messageId }, cancellationToken)
                .ConfigureAwait(false);

            if (message is null)
                throw new MessageNotFoundException(messageId);

            if (!message.Sender.User.Equals(user))
            {
                await _authorizationService
                    .AuthorizeMessageEditAsync(user, message.Sender.Chat, cancellationToken)
                    .ConfigureAwait(false);
            }

            IEnumerable<Content> contents = contentDtos.Select(_mapper.Map<Content>);
            message.UpdateContent(contents);

            _context.Messages.Update(message);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}