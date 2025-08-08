using FormsEngine.Shared.Events;
using FormsEngine.Shared.Interfaces;
using FormsEngine.Shared.Wrappers;
using FormsService.Domain.Enums;
using FormsService.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FormsService.Application.Features.UpdateForm;

public class UpdateFormCommandHandler(
    ILogger<UpdateFormCommandHandler> logger,
    IPublisher publisher,
    IGenericRepository<Form> formsRepository) : IRequestHandler<UpdateFormCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(UpdateFormCommand req, CancellationToken ct)
    {
        var sections = req.Sections.Select(s => new Section(
            s.Title,
            s.Order,
            s.Fields.Select(f => new Field(
                f.FieldId, f.Label, f.Type, f.Order, f.Required, f.MaxLength)).ToList()
        )).ToList();

        var form = await formsRepository.GetByIdAsync(req.Id) ?? throw new KeyNotFoundException($"Form with ID {req.Id} not found!");
        if (form.State == FormState.Archived)
            throw new InvalidOperationException("Cannot update an archived form");

        if (form.TenantId != req.TenantId)
            throw new UnauthorizedAccessException($"Form with ID {req.Id} does not belong to tenant {req.TenantId}.");

        form = Form.Update(form, req.Name, req.Description, sections);

        formsRepository.Update(form);
        await formsRepository.SaveChangesAsync(ct);

        // Here publish the event to other components that might be interested in the form being updated.
        await publisher.Publish(new FormUpdatedEvent(form.Id, form.TenantId, form.EntityId, form.Version), ct);

        logger.LogInformation("Updated form {FormId} for tenant {TenantId}", form.Id, req.TenantId);

        return new Response<Guid>(form.Id, "Form Data Updated");
    }
}