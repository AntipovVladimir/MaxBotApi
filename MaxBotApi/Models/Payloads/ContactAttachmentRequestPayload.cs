using System.Text.Json.Serialization;

namespace MaxBotApi.Models.Payloads;

public class ContactAttachmentRequestPayload
{
    /// <summary>
    /// Имя контакта
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// ID контакта, если он зарегистирован в MAX
    /// </summary>
    [JsonPropertyName("contact_id")]
    public long? ContactId { get; set; }

    /// <summary>
    /// Полная информация о контакте в формате VCF
    /// </summary>
    [JsonPropertyName("vcf_info")]
    public string? VCF_Info { get; set; }

    /// <summary>
    /// Телефон контакта в формате VCF
    /// </summary>
    [JsonPropertyName("vcf_phone")]
    public string? VCF_Phone { get; set; }
}