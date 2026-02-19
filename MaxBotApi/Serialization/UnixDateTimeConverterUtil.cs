using System.Text.Json;

namespace MaxBotApi.Serialization;

internal static class UnixDateTimeConverterUtil
{
    private static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    internal static DateTime Read(ref Utf8JsonReader reader, Type typeToConvert)
    {
        var seconds = reader.GetInt64();
        if (seconds > 0)
            return UnixEpoch.AddMilliseconds(seconds);
        if (seconds == 0)
            return default; // easier to test than 1/1/1970
        throw new JsonException($"Cannot convert value that is before Unix epoch of 00:00:00 UTC on 1 January 1970 to {typeToConvert}.");
    }

    internal static void Write(Utf8JsonWriter writer, DateTime value)
    {
        if (value == default)
            writer.WriteNumberValue(0L);
        else
        {
            long milliseconds = (long)(value.ToUniversalTime() - UnixEpoch).TotalMilliseconds;
            if (milliseconds >= 0)
                writer.WriteNumberValue(milliseconds);
            else
                throw new JsonException("Cannot convert date value that is before Unix epoch of 00:00:00 UTC on 1 January 1970.");
        }
    }
} 