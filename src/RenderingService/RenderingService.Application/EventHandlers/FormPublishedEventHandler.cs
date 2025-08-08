using FormsEngine.Shared.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RenderingService.Application.EventHandlers;

public class FormPublishedEventHandler(
    ILogger<FormPublishedEventHandler> logger
    ) :
    INotificationHandler<FormPublishedEvent>
{
    public Task Handle(FormPublishedEvent notification, CancellationToken cancellationToken)
    {
        // Logic to handle the form published event
        logger.LogInformation("Form published event received for FormId: {FormId}", notification.FormId);
        return Task.CompletedTask;
    }
}