using Logger.Application.Enums;

namespace Logger.Application.Services;

public interface IDataEncryptionService
{
    byte[] Encrypt(byte[] data, CryptoPurpose purpose);
    byte[] Decrypt(byte[] encryptedData, CryptoPurpose purpose);
}
