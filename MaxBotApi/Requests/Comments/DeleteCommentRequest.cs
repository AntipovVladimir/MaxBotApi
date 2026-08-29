using MaxBotApi.Models;

namespace MaxBotApi.Requests;

public class DeleteCommentRequest : RequestBase<ApiResponse>
{
    public DeleteCommentRequest(string messageId, string commentId) : base(string.Format("messages/{0}/comments?comment_id={1}", messageId, commentId))
    {
        HttpMethod = HttpMethod.Delete;
    }
}