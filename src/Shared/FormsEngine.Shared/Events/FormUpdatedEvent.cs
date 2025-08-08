using MediatR;

namespace FormsEngine.Shared.Events;

public record FormUpdatedEvent(Guid FormId, string TenantId, string? EntityId, int Version) : INotification;