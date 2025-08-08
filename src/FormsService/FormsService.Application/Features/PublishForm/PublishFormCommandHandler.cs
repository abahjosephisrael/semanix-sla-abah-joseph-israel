using FormsEngine.Shared.Events;
using FormsEngine.Shared.Interfaces;
using FormsEngine.Shared.Wrappers;
using FormsService.Application.Interfaces;
using FormsService.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FormsService.Application.Features.PublishForm;

internal class PublishFormCommandHandler(
    IGenericRepository<Form> formsRepository,
    IPublisher publisher,
    ILogger<PublishFormCommandHandler> logger,
    IMetrics metrics) : IRequestHandler<PublishFormCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(PublishFormCommand cmd, CancellationToken ct)
    {
        var form = await formsRepository.GetByIdAsync(cmd.Id) ?? throw new KeyNotFoundException($"Form with ID {cmd.Id} not found!");

        form.Publish();
        formsRepository.Update(form);
        await formsRepository.SaveChangesAsync(ct);

        logger.LogInformation("Published form {FormId} v{Version}", form.Id, form.Version);
        metrics.Increment("fe_forms_published_total", new { Tenant = cmd.TenantId });

        // Here publish the event to other components that might be interested in the form being published.
        await publisher.Publish(new FormPublishedEvent(form.Id, form.TenantId, form.EntityId, form.Version), ct);

        return new Response<Guid>(form.Id, $"Form {form.Id} has been published successfully.");
    }
}