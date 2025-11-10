namespace Theater_Management_BE.src.Api.DTOs;

public record MovieActorsRequest(Guid MovieId, List<Guid> ActorsId);
