using Do_Svyazi.Message.Application.Abstractions.Integrations;
using MediatR;

namespace Do_Svyazi.Message.Application.CQRS.Users.Queries;

public static class GetUserChatIds
{
    public record Query(Guid UserId) : IRequest<Response>;

    public record Response(IReadOnlyCollection<Guid> ChatIds);

    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IUserModuleService _userModuleService;

        public Handler(IUserModuleService userModuleService)
        {
            _userModuleService = userModuleService;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<Guid> chatIds = await _userModuleService
                .GetUserChatIdsAsync(request.UserId, cancellationToken)
                .ConfigureAwait(false);
            
            return new Response(chatIds);
        }
    }
}