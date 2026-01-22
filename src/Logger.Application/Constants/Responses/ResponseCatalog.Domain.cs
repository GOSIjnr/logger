using Logger.Application.Common.Responses;

namespace Logger.Application.Constants.Responses;

internal static partial class ResponseCatalog
{
    public static class Organization
    {
        public static readonly OperationOutcomeResponse Created = new(
            "ORGANIZATION_CREATED",
            "Organization created successfully.",
            []
        );

        public static readonly OperationOutcomeResponse NotFound = new(
            "ORGANIZATION_NOT_FOUND",
            "Organization not found.",
            []
        );
    }

    public static class Sheet
    {
        public static readonly OperationOutcomeResponse Created = new(
            "SHEET_CREATED",
            "Sheet created successfully.",
            []
        );

        public static readonly OperationOutcomeResponse NotFound = new(
            "SHEET_NOT_FOUND",
            "Sheet not found.",
            []
        );
    }

    public static class Incident
    {
        public static readonly OperationOutcomeResponse Created = new(
            "INCIDENT_CREATED",
            "Incident created successfully.",
            []
        );

        public static readonly OperationOutcomeResponse Updated = new(
            "INCIDENT_UPDATED",
            "Incident updated successfully.",
            []
        );

        public static readonly OperationOutcomeResponse NotFound = new(
            "INCIDENT_NOT_FOUND",
            "Incident not found.",
            []
        );
    }
}
