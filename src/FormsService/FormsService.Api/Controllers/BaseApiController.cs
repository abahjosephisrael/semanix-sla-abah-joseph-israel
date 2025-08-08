using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FormsService.Api.Controllers;

// This is the base controller for all API controllers in the FormsService application. All API controllers should inherit from this class to ensure consistent behavior and access to shared services like MediatR.
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}