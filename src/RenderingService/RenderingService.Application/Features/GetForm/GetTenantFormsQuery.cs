using FluentValidation;
using FormsEngine.Shared.Wrappers;
using MediatR;
using RenderingService.Domain.Dtos;

namespace RenderingService.Application.Features.GetForm;

public record GetTenantFormsQuery(string TenantId) : IRequest<Response<IReadOnlyCollection<Form>>>;

public class GetTenantFormsQueryValidator : AbstractValidator<GetTenantFormsQuery>
{
    public GetTenantFormsQueryValidator()
    {
        RuleFor(x => x.TenantId).NotEmpty().WithMessage("TenantId is required.");
    }
}
