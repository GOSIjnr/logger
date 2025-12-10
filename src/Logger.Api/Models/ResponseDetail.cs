using Logger.Api.Enums;

namespace Logger.Api.Models;

public record ResponseDetail(
    string Message,
    ResponseSeverity Severity
);
