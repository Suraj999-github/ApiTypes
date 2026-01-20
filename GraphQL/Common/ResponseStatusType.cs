using System.ComponentModel;

namespace GraphQL.Common
{
    public enum ResponseStatusType
    {
        [Description("Either username or password is invalid.")]
        INVALID_CREDENTIALS,

        [Description("Invalid token.")]
        INVALID_TOKEN,

        [Description("Operation completed successfully")]
        SUCCESS,

        [Description("Operation failed")]
        FAILED,

        [Description("Validation errors occurred")]
        VALIDATION_ERROR,

        [Description("Requested resource not found")]
        NOT_FOUND,

        [Description("Authentication required")]
        UNAUTHORIZED,

        [Description("Insufficient permissions")]
        FORBIDDEN,

        [Description("Resource conflict detected")]
        CONFLICT,

        [Description("Rate limit exceeded")]
        RATE_LIMITED,

        [Description("Operation is in progress")]
        PROCESSING,

        [Description("Operation partially completed")]
        PARTIAL_SUCCESS,

        [Description("Invalid api version")]
        UN_SUPPORTED_API_VERSION,

        [Description("Invalid request")]
        ERROR,

        [Description("Something went wrong")]
        UNKNOWN,
    }
}
