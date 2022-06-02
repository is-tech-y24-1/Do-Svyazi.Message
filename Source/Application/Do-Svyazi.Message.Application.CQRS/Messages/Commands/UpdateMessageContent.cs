using AutoMapper;
using Do_Svyazi.Message.Application.Abstractions.DataAccess;
using Do_Svyazi.Message.Application.Abstractions.Exceptions.NotFound;
using Do_Svyazi.Message.Application.Abstractions.Services;
using Do_Svyazi.Message.Application.Dto.Messages;
using Do_Svyazi.Message.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Do_Svyazi.Message.Application.CQRS.Messages.Commands;

public static class UpdateMessageContent
{
    public record Command(
        Guid UserId,
        Guid MessageId,
        IReadOnlyCollection<ContentDto> AddedContent,
        IReadOnlyCollection<MessageContentDto> RemovedContent) : IRequest;

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
            var (userId, messageId, addedContentDtos, removedContentDtos) = request;

            var message = await _messageService
                .AuthorizeMessageToEdit(userId, messageId, cancellationToken)
                .ConfigureAwait(false);

            if (addedContentDtos.Count is not 0)
            {
                IEnumerable<Content> addedContents = addedContentDtos.Select(_mapper.Map<Content>);

                foreach (var content in addedContents)
                {
                    message.AddContent(content);
                }
            }

            if (removedContentDtos.Count is not 0)
            {
                Guid[] removedContentIds = removedContentDtos.Select(c => c.Id).ToArray();

                Content[] removedContents = await _context.Contents
                    .Where(c => removedContentIds.Contains(c.Id))
                    .ToArrayAsync(cancellationToken)
                    .ConfigureAwait(false);

                if (removedContents.Length != removedContentIds.Length)
                {
                    IEnumerable<Guid> foundIds = removedContents.Select(c => c.Id);
                    IEnumerable<Guid> notFoundIds = removedContentIds.Except(foundIds);

                    throw new ContentNotFoundException(notFoundIds);
                }

                foreach (var content in removedContents)
                {
                    message.RemoveContent(content);
                }
            }

            _context.Messages.Update(message);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}