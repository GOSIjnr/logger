using Logger.Api.Common.Responses;
using Logger.Shared.Enums;
using Logger.Shared.Models;

namespace Logger.Api.Constants.Responses;

public static partial class ResponseCatalog
{
    public static class Auth
    {
        public static readonly OperationOutcomeResponse RegisterSuccessful = new(
            Id: "AUTH_REGISTER_SUCCESS",
            Title: "Registration successful.",
            Details: [
                new ResponseDetail(
                    Message: "Your account has been created successfully. Please verify your email to complete registration.",
                    Severity: ResponseSeverity.Info
                )
            ]
        );
    }
}
