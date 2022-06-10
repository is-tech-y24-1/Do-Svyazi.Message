using Do_Svyazi.Message.Application.Dto.Messages;

namespace Do_Svyazi.Message.Client.Tcp.Interfaces;

public interface IChatClient
{
    Task OnErrorOccured(string message);
    Task OnMessageReceived(MessageDto messageDto);
    Task OnMessageUpdated(Guid messageId);
    Task OnMessageDeleted(Guid messageId);
}