using FormsEngine.Shared.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RenderingService.Application.EventHandlers;

public class FormUpdatedEventHandler(
    ILogger<FormUpdatedEventHandler> logger
    ) :
    INotificationHandler<FormUpdatedEvent>
{
    public Task Handle(FormUpdatedEvent notification, CancellationToken cancellationToken)
    {
        // Logic to handle the form updated event
        logger.LogInformation("Form updated event received for FormId: {FormId}", notification.FormId);
        return Task.CompletedTask;
    }
}