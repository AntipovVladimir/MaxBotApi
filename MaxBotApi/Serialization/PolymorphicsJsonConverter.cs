using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MaxBotApi.Serialization;

/// <summary>
/// Supports deserializing JSON payloads that use polymorphism but don't specify $type as the first field.
/// Modified from https://github.com/dotnet/runtime/issues/72604#issuecomment-1440708052.
/// </summary>
internal sealed class PolymorphicJsonConverter<T> : JsonConverter<T>
{
    private readonly string _discriminatorPropName;
    private readonly Dictionary<string, Type> _discriminatorToSubtype = [];

    public PolymorphicJsonConverter()
    {
        var attr = typeof(T).GetCustomAttribute<CustomJsonPolymorphicAttribute>()!;
        _discriminatorPropName = JsonNamingPolicy.SnakeCaseLower.ConvertName(attr.TypeDiscriminatorPropertyName ?? "$type");

        foreach (var subtype in typeof(T).GetCustomAttributes<CustomJsonDerivedTypeAttribute>())
        {
            if (subtype.Discriminator is not null)
            {
                _discriminatorToSubtype.Add(subtype.Discriminator, subtype.Subtype);
            }
        }
    }

    public override bool CanConvert(Type typeToConvert) => typeof(T) == typeToConvert;

    public override T Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions options)
    {
        var reader2 = reader;
        using var doc = JsonDocument.ParseValue(ref reader2);

        var root = doc.RootElement;
        var typeField = root.GetProperty(_discriminatorPropName);

        if (typeField.GetString() is not { } typeName)
        {
            throw new JsonException($"Could not find string property {_discriminatorPropName} when trying to deserialize {typeof(T).Name}");
        }

        if (!_discriminatorToSubtype.TryGetValue(typeName, out var type))
            throw new JsonException($"Unknown type: {typeName}");

        return (T)JsonSerializer.Deserialize(ref reader, type, options)!;
    }

    public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
    {
        var type = value!.GetType();
        JsonSerializer.Serialize(writer, value, type, options);
    }
}