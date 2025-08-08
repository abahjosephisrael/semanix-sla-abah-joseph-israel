namespace RenderingService.Domain.Dtos;

public record RenderedForm(Guid id, string name, string description, int version, string state, object sections, DateTime created_at, string? created_by, DateTime? updated_at, string? updated_by);