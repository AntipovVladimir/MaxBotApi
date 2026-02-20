using System.Text;
using System.Text.Json.Serialization;
using MaxBotApi.Enums;
using MaxBotApi.Serialization;

namespace MaxBotApi.Models;

[JsonConverter(typeof(PolymorphicJsonConverter<MarkupElement>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(StrongMarkupElement), "strong")]
[CustomJsonDerivedType(typeof(EmphasizedMarkupElement), "emphasized")]
[CustomJsonDerivedType(typeof(MonospacedMarkupElement), "monospaced")]
[CustomJsonDerivedType(typeof(LinkMarkupElement), "link")]
[CustomJsonDerivedType(typeof(StrikethroughMarkupElement), "strikethrough")]
[CustomJsonDerivedType(typeof(UnderlineMarkupElement), "underline")]
[CustomJsonDerivedType(typeof(UserMentionMarkupElement), "user_mention")]
public abstract class MarkupElement
{
    /// <summary>
    /// Тип элемента разметки. Может быть жирный, курсив, зачеркнутый, подчеркнутый, моноширинный, ссылка или упоминание пользователя
    /// strong , emphasized, monospaced, link, strikethrough, underline, user_mention
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("type")]
    public abstract MarkupElementType Type { get; set; }

    /// <summary>
    /// Индекс начала элемента разметки в тексте. Нумерация с нуля
    /// </summary>
    [JsonPropertyName("from")]
    public int From { get; set; }

    /// <summary>
    /// Длина элемента разметки
    /// </summary>
    [JsonPropertyName("length")]
    public int Length { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("Type: ");
        sb.Append(Type);
        sb.Append(", From: ");
        sb.Append(From);
        sb.Append(", Length: ");
        sb.Append(Length);
        return sb.ToString();
    }
}

public class StrongMarkupElement : MarkupElement
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override MarkupElementType Type { get; set; } = MarkupElementType.Strong;
}

public class EmphasizedMarkupElement : MarkupElement
{
    public override MarkupElementType Type { get; set; } = MarkupElementType.Emphasized;
}

public class MonospacedMarkupElement : MarkupElement
{
    public override MarkupElementType Type { get; set; } = MarkupElementType.Monospaced;
}

public class LinkMarkupElement : MarkupElement
{
    public override MarkupElementType Type { get; set; } = MarkupElementType.Link;

    /// <summary>
    /// URL ссылки, до 1024 символов
    /// </summary>
    [JsonPropertyName("url")]
    public required string Url { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append(base.ToString());
        sb.Append(", Url: ");
        sb.Append(Url);
        return sb.ToString();
    }
}

public class StrikethroughMarkupElement : MarkupElement
{
    public override MarkupElementType Type { get; set; } = MarkupElementType.Strikethrough;
}

public class UnderlineMarkupElement : MarkupElement
{
    public override MarkupElementType Type { get; set; } = MarkupElementType.Underline;
}

public class UserMentionMarkupElement : MarkupElement
{
    public override MarkupElementType Type { get; set; } = MarkupElementType.UserMention;

    /// <summary>
    /// @username упомянутого пользователя
    /// </summary>
    [JsonPropertyName("user_link")]
    public string? UserLink { get; set; }

    /// <summary>
    /// ID упомянутого пользователя без имени
    /// </summary>
    [JsonPropertyName("user_id")]
    public long? UserID { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append(base.ToString());
        if (UserLink != null)
        {
            sb.Append(", UserLink: ");
            sb.Append(UserLink);
        }

        if (UserID != null)
        {
            sb.Append(", UserID: ");
            sb.Append(UserID);
        }
        return sb.ToString();
    }
}