using System.Text.Json.Serialization;

namespace MaxBotApi.Models.Payloads;

public class ContactAttachmentPayload
{
    /// <summary>
    /// Информация о пользователе в формате VCF.
    /// </summary>
    [JsonPropertyName("vcf_info")]
    public string? VCFInfo { get; set; }
    
    /// <summary>
    /// Информация о пользователе
    /// </summary>
    [JsonPropertyName("max_info")]
    public User? MaxInfo { get; set; }
}