using FormsEngine.Shared.Interfaces;
using FormsEngine.Shared.Wrappers;
using FormsService.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FormsService.Application.Features.CreateForm;

public class CreateFormCommandHandler(
    ILogger<CreateFormCommandHandler> logger,
    IGenericRepository<Form> formsRepository) : IRequestHandler<CreateFormCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(CreateFormCommand req, CancellationToken ct)
    {
        var sections = req.Sections.Select(s => new Section(
            s.Title,
            s.Order,
            s.Fields.Select(f => new Field(
                f.FieldId, f.Label, f.Type, f.Order, f.Required, f.MaxLength)).ToList()
        )).ToList();

        var form = Form.CreateDraft(req.TenantId, req.EntityId, req.Name, req.Description, sections);
        await formsRepository.AddAsync(form);
        await formsRepository.SaveChangesAsync(ct);

        logger.LogInformation("Created form {FormId} for tenant {TenantId}", form.Id, req.TenantId);

        return new Response<Guid>(form.Id, "Form Data Saved");
    }
}