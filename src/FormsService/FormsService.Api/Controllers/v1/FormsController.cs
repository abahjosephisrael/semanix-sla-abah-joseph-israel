using Asp.Versioning;
using FormsEngine.Shared.Settings;
using FormsService.Application.Features.ArchiveForm;
using FormsService.Application.Features.CreateForm;
using FormsService.Application.Features.PublishForm;
using FormsService.Application.Features.UpdateForm;
using Microsoft.AspNetCore.Mvc;

namespace FormsService.Api.Controllers.v1;


[ApiVersion("1.0")]
public class FormsController : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateFormCommand cmd, CancellationToken ct)
    {
        var actual = cmd with { TenantId = TenantSettings.TenantId!, EntityId = TenantSettings.EntityId };

        var response = await Mediator.Send(actual, ct);
        return Ok(response);
    }
    
    [HttpPut]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateFormCommand cmd, CancellationToken ct)
    {
        var actual = cmd with { TenantId = TenantSettings.TenantId!, EntityId = TenantSettings.EntityId };

        var response = await Mediator.Send(actual, ct);
        return Ok(response);
    }

    [HttpPatch("{id}/publish")]
    public async Task<IActionResult> Publish(Guid id, CancellationToken ct)
    {
        var response = await Mediator.Send(new PublishFormCommand(id, TenantSettings.TenantId!), ct);
        return Ok(response);
    }

    [HttpPatch("{id}/archive")]
    public async Task<IActionResult> Archive(Guid id, CancellationToken ct)
    {
        var response = await Mediator.Send(new ArchiveFormCommand(id, TenantSettings.TenantId!), ct);
        return Ok(response);
    }
}
