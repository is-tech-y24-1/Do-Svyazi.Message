using Do_Svyazi.Message.Application.Abstractions.Services;

namespace Do_Svyazi.Message.Application.Services;

public class UtcDateTimeService : IDateTimeService
{
    public DateTime GetCurrent()
        => DateTime.UtcNow;
}