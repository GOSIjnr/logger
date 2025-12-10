namespace Logger.Api.Configurations.Security;

public record DataEncryptionOptions
{
    public string Key { get; init; } = string.Empty;
}
