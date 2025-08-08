using MediatR;

namespace FormsEngine.Shared.Events;

public record FormPublishedEvent(Guid FormId, string TenantId, string? EntityId, int Version) : INotification;