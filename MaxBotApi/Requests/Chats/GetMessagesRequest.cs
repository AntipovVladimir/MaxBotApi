using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class GetMessagesRequest : RequestBase<MessagesResponse>
{
    private const string base1 = "messages?chat_id={0}";
    private const string base2 = "messages?messages_ids={0}";
    private const string base3 = "messages?chat_id={0}&messages_ids={1}";
    
    
    public GetMessagesRequest(long chat_id) : base(string.Format(base1, chat_id))
    {
        HttpMethod = HttpMethod.Get;
    }


    public GetMessagesRequest(IEnumerable<string> messages_ids)
        : base(string.Format(base2, string.Join(",", messages_ids)))
    {
        HttpMethod = HttpMethod.Get;
    }


    public GetMessagesRequest(long chat_id, IEnumerable<string> messages_ids)
        : base(string.Format(base3, chat_id, string.Join(",", messages_ids)))
    {
        HttpMethod = HttpMethod.Get;
    }

    // TODO Implement optional: long from, long to, int count = 50
}