using Logger.Shared.Enums;

namespace Logger.Shared.Models;

public record ResponseDetail(
    string Message,
    ResponseSeverity Severity
);
