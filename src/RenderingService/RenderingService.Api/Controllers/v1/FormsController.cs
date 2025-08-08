using Asp.Versioning;
using FormsEngine.Shared.Controllers;
using Microsoft.AspNetCore.Mvc;
using RenderingService.Application.Features.GetForm;

namespace RenderingService.Api.Controllers.v1;

[ApiVersion("1.0")]
public class FormsController : BaseApiController
{
    /// <summary>
    /// Returns a list of forms for a tenant.
    /// </summary>
    /// <returns>A list of forms.</returns>
    [HttpGet("rendered-forms")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTenantForms([FromQuery] string tenant)
    {
        var response = await Mediator.Send(new GetTenantFormsQuery(tenant));
        return Ok(response);
    }
}
