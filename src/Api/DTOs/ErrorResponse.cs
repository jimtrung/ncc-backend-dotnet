namespace Theater_Management_BE.src.Api.DTOs

public record ErrorResponse(
    DateTime Timestamp,
    int Status,
    string Error,
    string Message,
    string Path);