namespace MaxBotApi.Serialization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
internal sealed class CustomJsonDerivedTypeAttribute(Type subtype, string? discriminator = default) : Attribute
{
    public Type Subtype { get; } = subtype;
    public string? Discriminator { get; set; } = discriminator;
}