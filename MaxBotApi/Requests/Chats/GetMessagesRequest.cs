using System.Text;
using System.Text.Json.Serialization;
using MaxBotApi.Models;
using MaxBotApi.Serialization;

namespace MaxBotApi.Requests;

public class GetMessagesRequest : ParameterlessRequest<MessagesResponse>
{
    private const string base1 = "messages?chat_id={0}";
    private const string base2 = "messages?messages_ids={0}";

    [JsonConverter(typeof(UnixDateTimeConverter))]
    [JsonPropertyName("from")]
    public DateTime? From { get; set; }

    [JsonConverter(typeof(UnixDateTimeConverter))]
    [JsonPropertyName("to")]
    public DateTime? To { get; set; }

    [JsonPropertyName("count")] public int? Count { get; set; }

    public GetMessagesRequest(long chat_id) : base(string.Empty)
    {
        HttpMethod = HttpMethod.Get;
        StringBuilder sb = new();
        sb.Append(string.Format(base1, chat_id));
        if (From != null)
        {
            long unixTime = ((DateTimeOffset)From.Value).ToUnixTimeSeconds();
            sb.Append(string.Format("&from={0}", unixTime));
        }

        if (To != null)
        {
            long unixTime = ((DateTimeOffset)To.Value).ToUnixTimeSeconds();
            sb.Append(string.Format("&to={0}", unixTime));
        }

        if (Count != null)
            sb.Append(string.Format("&count={0}", Count));
        MethodName = sb.ToString();
    }


    public GetMessagesRequest(IEnumerable<string> messages_ids) : base(string.Empty)
    {
        HttpMethod = HttpMethod.Get;
        StringBuilder sb = new();
        sb.Append(string.Format(base2, string.Join(",", messages_ids)));
        if (From != null)
        {
            long unixTime = ((DateTimeOffset)From.Value).ToUnixTimeSeconds();
            sb.Append(string.Format("&from={0}", unixTime));
        }

        if (To != null)
        {
            long unixTime = ((DateTimeOffset)To.Value).ToUnixTimeSeconds();
            sb.Append(string.Format("&to={0}", unixTime));
        }

        if (Count != null)
            sb.Append(string.Format("&count={0}", Count));
        MethodName = sb.ToString();
    }

}