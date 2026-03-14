using System.Runtime.CompilerServices;
using System.Text.Json;

namespace MaxBotApi.Extensions;

internal static class ObjectExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static T ThrowIfNull<T>(this T? value, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        => value ?? throw new ArgumentNullException(parameterName);

    private static readonly JsonSerializerOptions jso = new()
    {
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true,
        PropertyNameCaseInsensitive = false
    };

    internal static string SerializeToString<T>(this T value)
    {
        return JsonSerializer.Serialize(value, jso);
    }
}