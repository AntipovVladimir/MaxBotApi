using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class GetCommentRequest : RequestBase<CommentMessage>
{
    public GetCommentRequest(string message_id, string comment_id) : base(string.Format("messages/{0}/comments/{1}", message_id, comment_id))
    {
        HttpMethod = HttpMethod.Get;
    }
}