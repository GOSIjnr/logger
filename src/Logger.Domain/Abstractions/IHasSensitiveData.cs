using Logger.Domain.Components.Security;

namespace Logger.Domain.Abstractions;

internal interface IHasSensitiveData<TSensitive>
    where TSensitive : ISensitiveData
{
    byte[] EncryptedData { get; }
    TSensitive? SensitiveData { get; }

    void SetEncryptedData(byte[] data);

    void SetSensitiveData(TSensitive data);
    void ClearSensitiveData();
}
