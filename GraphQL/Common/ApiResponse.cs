namespace GraphQL.Common
{
    public class ApiResponse<T>
    {
        public string Status { get; set; }
        public string? Message { get; set; }
        public string? Id { get; set; }
        public string? ResponseCode { get; set; }
        public T? Data { get; set; }
        public DateTime Timestamp { get; set; }
        public List<ErrorDetail>? Errors { get; set; }
        public string? Description { get; set; }
        public string? ErrorCode { get; set; }

        public ApiResponse()
        {
            Timestamp = DateTime.UtcNow;
            Errors = new List<ErrorDetail>();
        }

        // Success factory methods
        public static ApiResponse<T> SUCCESS(T? data, string? entityId = null, string? description = null, string? responseCode = null)
        {
            return new ApiResponse<T>
            {
                Status = ResponseStatusType.SUCCESS.ToString(),
                Message = EnumHelper.GetDescription(ResponseStatusType.SUCCESS),
                Description = description,
                Data = data,
                Id = entityId,
                ResponseCode = responseCode
            };
        }

        public static ApiResponse<T> FAILED(T? data = default, string? entityId = null, string? description = null, string? responseCode = null)
        {
            return new ApiResponse<T>
            {
                Status = ResponseStatusType.FAILED.ToString(),
                Message = EnumHelper.GetDescription(ResponseStatusType.FAILED),
                Description = description,
                Data = data,
                Id = entityId,
                ResponseCode = responseCode
            };
        }

        public static ApiResponse<T> VALIDATION_ERROR(string? description = null, string? responseCode = null, List<ErrorDetail>? errors = null)
        {
            return new ApiResponse<T>
            {
                Status = ResponseStatusType.VALIDATION_ERROR.ToString(),
                Message = EnumHelper.GetDescription(ResponseStatusType.VALIDATION_ERROR),
                Description = description,
                ResponseCode = responseCode,
                Errors = errors ?? new List<ErrorDetail>()
            };
        }

        public static ApiResponse<T> NOT_FOUND(T? data = default, string? entityId = null, string? description = null, string? responseCode = null)
        {
            return new ApiResponse<T>
            {
                Status = ResponseStatusType.NOT_FOUND.ToString(),
                Message = EnumHelper.GetDescription(ResponseStatusType.NOT_FOUND),
                Description = description,
                Data = data,
                Id = entityId,
                ResponseCode = responseCode
            };
        }

        public static ApiResponse<T> UNAUTHORIZED(T? data = default, string? entityId = null, string? description = null, string? responseCode = null)
        {
            return new ApiResponse<T>
            {
                Status = ResponseStatusType.UNAUTHORIZED.ToString(),
                Message = EnumHelper.GetDescription(ResponseStatusType.UNAUTHORIZED),
                Description = description,
                Data = data,
                Id = entityId,
                ResponseCode = responseCode
            };
        }

        public static ApiResponse<T> FORBIDDEN(T? data = default, string? entityId = null, string? description = null, string? responseCode = null)
        {
            return new ApiResponse<T>
            {
                Status = ResponseStatusType.FORBIDDEN.ToString(),
                Message = EnumHelper.GetDescription(ResponseStatusType.FORBIDDEN),
                Description = description,
                Data = data,
                Id = entityId,
                ResponseCode = responseCode
            };
        }

        public static ApiResponse<T> CONFLICT(T? data = default, string? entityId = null, string? description = null, string? responseCode = null)
        {
            return new ApiResponse<T>
            {
                Status = ResponseStatusType.CONFLICT.ToString(),
                Message = EnumHelper.GetDescription(ResponseStatusType.CONFLICT),
                Description = description,
                Data = data,
                Id = entityId,
                ResponseCode = responseCode
            };
        }

        public static ApiResponse<T> RATE_LIMITED(T? data = default, string? entityId = null, string? description = null, string? responseCode = null)
        {
            return new ApiResponse<T>
            {
                Status = ResponseStatusType.RATE_LIMITED.ToString(),
                Message = EnumHelper.GetDescription(ResponseStatusType.RATE_LIMITED),
                Description = description,
                Data = data,
                Id = entityId,
                ResponseCode = responseCode
            };
        }

        public static ApiResponse<T> PROCESSING(T? data = default, string? entityId = null, string? description = null, string? responseCode = null)
        {
            return new ApiResponse<T>
            {
                Status = ResponseStatusType.PROCESSING.ToString(),
                Message = EnumHelper.GetDescription(ResponseStatusType.PROCESSING),
                Description = description,
                Data = data,
                Id = entityId,
                ResponseCode = responseCode
            };
        }

        public static ApiResponse<T> PARTIAL_SUCCESS(T? data = default, string? entityId = null, string? description = null, string? responseCode = null)
        {
            return new ApiResponse<T>
            {
                Status = ResponseStatusType.PARTIAL_SUCCESS.ToString(),
                Message = EnumHelper.GetDescription(ResponseStatusType.PARTIAL_SUCCESS),
                Description = description,
                Data = data,
                Id = entityId,
                ResponseCode = responseCode
            };
        }

        public static ApiResponse<T> ERROR(T? data = default, string? entityId = null, string? description = null, string? responseCode = null, List<ErrorDetail>? errors = null)
        {
            return new ApiResponse<T>
            {
                Status = ResponseStatusType.ERROR.ToString(),
                Message = EnumHelper.GetDescription(ResponseStatusType.ERROR),
                Description = description,
                Data = data,
                Id = entityId,
                ResponseCode = responseCode,
                Errors = errors ?? new List<ErrorDetail>()
            };
        }

        public static ApiResponse<T> UNKNOWN(string errorCode, string description)
        {
            return new ApiResponse<T>
            {
                Status = ResponseStatusType.UNKNOWN.ToString(),
                Message = EnumHelper.GetDescription(ResponseStatusType.UNKNOWN),
                ErrorCode = errorCode,
                Description = description
            };
        }

        public static ApiResponse<T> INVALID_CREDENTIALS(string? description = null)
        {
            return new ApiResponse<T>
            {
                Status = ResponseStatusType.INVALID_CREDENTIALS.ToString(),
                Message = EnumHelper.GetDescription(ResponseStatusType.INVALID_CREDENTIALS),
                Description = description
            };
        }

        public static ApiResponse<T> INVALID_TOKEN(string? description = null)
        {
            return new ApiResponse<T>
            {
                Status = ResponseStatusType.INVALID_TOKEN.ToString(),
                Message = EnumHelper.GetDescription(ResponseStatusType.INVALID_TOKEN),
                Description = description
            };
        }

        public static ApiResponse<T> UN_SUPPORTED_API_VERSION(string? description = null)
        {
            return new ApiResponse<T>
            {
                Status = ResponseStatusType.UN_SUPPORTED_API_VERSION.ToString(),
                Message = EnumHelper.GetDescription(ResponseStatusType.UN_SUPPORTED_API_VERSION),
                Description = description
            };
        }
    }

    public class ErrorDetail
    {
        public string? Message { get; set; }
        public string? Field { get; set; }
        public object? Details { get; set; }
    }
}
