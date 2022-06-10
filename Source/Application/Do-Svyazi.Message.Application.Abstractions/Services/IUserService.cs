using Do_Svyazi.Message.Domain.Entities;

namespace Do_Svyazi.Message.Application.Abstractions.Services;

public interface IUserService
{
    Task<User> GetUserAsync(Guid id, CancellationToken cancellationToken);
}