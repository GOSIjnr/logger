using Logger.Api.Common.Entities;

namespace Logger.Api.Models;

public class UserSensitive : ISensitiveData
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
}
