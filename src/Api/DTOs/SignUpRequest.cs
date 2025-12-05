namespace Theater_Management_BE.src.Api.DTOs;

public record SignUpRequest(
    string Username,
    string Email,
    string Password);
