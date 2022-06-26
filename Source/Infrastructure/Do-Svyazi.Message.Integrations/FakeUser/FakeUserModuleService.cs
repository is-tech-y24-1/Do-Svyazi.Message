using Do_Svyazi.Message.Application.Abstractions.DataAccess;
using Do_Svyazi.Message.Application.Abstractions.Integrations;
using Microsoft.EntityFrameworkCore;

namespace Do_Svyazi.Message.Integrations.FakeUser;

public class FakeUserModuleService : IUserModuleService
{
    private readonly IMessageDatabaseContext _context;

    public FakeUserModuleService(IMessageDatabaseContext context)
    {
        _context = context;
    }

    public Task<bool> IsUserChatMemberAsync(Guid userId, Guid chatId, CancellationToken cancellationToken)
        => Task.FromResult(true);

    public async Task<IReadOnlyCollection<Guid>> GetUserChatIdsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Chats
            .Select(c => c.Id)
            .ToListAsync(cancellationToken);
    }
}