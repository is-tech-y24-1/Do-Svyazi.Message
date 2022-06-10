using Do_Svyazi.Message.Application.Abstractions.DataAccess;
using Do_Svyazi.Message.Application.Abstractions.Exceptions.NotFound;
using Do_Svyazi.Message.Application.Abstractions.Services;
using Do_Svyazi.Message.Domain.Entities;

namespace Do_Svyazi.Message.Application.Services;

public class UserService : IUserService
{
    private readonly IMessageDatabaseContext _context;

    public UserService(IMessageDatabaseContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FindAsync(new object[] { id }, cancellationToken)
            .ConfigureAwait(false);

        if (user is null)
            throw new UserNotFoundException(id);

        return user;
    }
}