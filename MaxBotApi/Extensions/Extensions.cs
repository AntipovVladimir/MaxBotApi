using System.Runtime.CompilerServices;

namespace MaxBotApi.Extensions;

internal static class ObjectExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static T ThrowIfNull<T>(this T? value, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        => value ?? throw new ArgumentNullException(parameterName);
}