namespace Logger.Api.Interfaces.Services;

public interface IDataEncryptionService
{
	byte[] EncryptData(byte[] data);
	byte[] DecryptData(byte[] encryptedData);
}
