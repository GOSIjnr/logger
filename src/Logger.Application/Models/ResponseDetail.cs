using Logger.Application.Enums;

namespace Logger.Application.Models;

public record ResponseDetail(
    string Message,
    ResponseSeverity Severity
);
