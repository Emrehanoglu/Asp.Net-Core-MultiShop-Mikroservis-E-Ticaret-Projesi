using MultiShop.Message.DAL.Context;
using MultiShop.Message.Dtos;

namespace MultiShop.Message.Services;

public class UserMessageService : IUserMessageService
{
    private readonly MessageContext _messageContext;

    public UserMessageService(MessageContext messageContext)
    {
        _messageContext = messageContext;
    }

    public async Task CreateMessageAsync(CreateMessageDto createMessageDto)
    {
        await _messageContext.UserMessages(createMessageDto);
    }

    public async Task DeleteMessageAsync(int id)
    {
        var values = await _messageContext.UserMessages.FindAsync(id);
        _messageContext.UserMessages.Remove(values);
    }

    public Task<List<ResultMessageDto>> GetAllMessageAsync()
    {
        throw new NotImplementedException();
    }

    public Task<GetByIdMessageDto> GetByIdMessageAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ResultInboxMessageDto>> GetInboxMessageAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ResultSendboxMessageDto>> GetSendboxMessageAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetTotalMessageCount()
    {
        throw new NotImplementedException();
    }

    public Task<int> GetTotalMessageCountByReceiverId(string id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateMessageAsync(UpdateMessageDto updateMessageDto)
    {
        throw new NotImplementedException();
    }
}
