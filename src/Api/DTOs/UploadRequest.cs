using Microsoft.AspNetCore.Http;
namespace Theater_Management_BE.src.Api.DTOs;

public record UploadRequest(IFormFile File, string Type, Guid Id);
