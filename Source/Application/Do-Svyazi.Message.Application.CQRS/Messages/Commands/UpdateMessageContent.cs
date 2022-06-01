using AutoMapper;
using Do_Svyazi.Message.Application.Abstractions.DataAccess;
using Do_Svyazi.Message.Application.Abstractions.Services;
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
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public Handler(
            IMessageDatabaseContext context,
            IMapper mapper,
            IMessageService messageService)
        {
            _context = context;
            _mapper = mapper;
            _messageService = messageService;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var (userId, messageId, contentDtos) = request;

            var message = await _messageService.AuthorizeMessageToEdit(userId, messageId, cancellationToken);

            IEnumerable<Content> contents = contentDtos.Select(_mapper.Map<Content>);
            message.UpdateContent(contents);

            _context.Messages.Update(message);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}