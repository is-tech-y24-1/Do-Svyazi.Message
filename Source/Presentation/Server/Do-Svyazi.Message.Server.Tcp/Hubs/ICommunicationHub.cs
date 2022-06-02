using Do_Svyazi.Message.Application.Dto.Messages;

namespace Do_Svyazi.Message.Server.Tcp.Hubs;

public interface ICommunicationHub
{
    Task SendAsync(string method, MessageDto messageDto);
    Task NotifyAsync(string method, string message);
}