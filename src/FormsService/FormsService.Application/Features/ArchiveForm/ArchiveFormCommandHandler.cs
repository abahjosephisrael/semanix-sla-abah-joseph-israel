using FormsEngine.Shared.Events;
using FormsEngine.Shared.Interfaces;
using FormsEngine.Shared.Wrappers;
using FormsService.Application.Interfaces;
using FormsService.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FormsService.Application.Features.ArchiveForm;

internal class ArchiveFormCommandHandler(
    IGenericRepository<Form> formsRepository,
    IPublisher publisher,
    ILogger<ArchiveFormCommandHandler> logger,
    IMetrics metrics) : IRequestHandler<ArchiveFormCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(ArchiveFormCommand cmd, CancellationToken ct)
    {
        var form = await formsRepository.GetByIdAsync(cmd.Id) ?? throw new KeyNotFoundException($"Form with ID {cmd.Id} not found!");

        form.Archive();
        formsRepository.Update(form);
        await formsRepository.SaveChangesAsync(ct);

        logger.LogInformation("Archived form {FormId} v{Version}", form.Id, form.Version);
        metrics.Increment("fe_forms_published_total", new { Tenant = cmd.TenantId });

        // Here publish the event to other components that might be interested in the form being archived.
        await publisher.Publish(new FormUpdatedEvent(form.Id, form.TenantId, form.EntityId, form.Version), ct);

        return new Response<Guid>(form.Id, $"Form {form.Id} has been archived successfully.");
    }
}