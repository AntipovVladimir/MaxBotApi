using System.Text;
using System.Text.Json.Serialization;
using MaxBotApi.Models;
using MaxBotApi.Serialization;

namespace MaxBotApi.Requests;

public class GetCommentsRequest : RequestBase<CommentsResponse>
{
    private const string base1 = "messages/{0}/comments";

    /// <summary>
    /// Список идентификаторов комментариев, которые вы хотите получить
    /// </summary>
    [JsonPropertyName("comment_ids")]
    public IEnumerable<string>? CommentIds { get; set; }


    [JsonConverter(typeof(UnixDateTimeConverter))]
    [JsonPropertyName("before")]
    public DateTime? Before { get; set; }

    [JsonConverter(typeof(UnixDateTimeConverter))]
    [JsonPropertyName("after")]
    public DateTime? After { get; set; }

    /// <summary>
    /// По умолчанию: 50. Количество комментариев, которое вы хотите получить в ответе: от 1 до 100
    /// </summary>
    [JsonPropertyName("count")]
    public int? Count { get; set; }


    public GetCommentsRequest(string message_id) : base(string.Empty)
    {
        StringBuilder sb = new();
        sb.Append(string.Format(base1, message_id));
        HttpMethod = HttpMethod.Get;

        if (Before != null)
        {
            long unixTime = ((DateTimeOffset)Before.Value).ToUnixTimeSeconds();
            sb.Append(string.Format("&from={0}", unixTime));
        }

        if (After != null)
        {
            long unixTime = ((DateTimeOffset)After.Value).ToUnixTimeSeconds();
            sb.Append(string.Format("&to={0}", unixTime));
        }

        if (Count != null)
            sb.Append(string.Format("&count={0}", Count));
        MethodName = sb.ToString();
    }
}