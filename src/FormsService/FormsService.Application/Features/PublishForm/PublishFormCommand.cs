using FluentValidation;
using FormsEngine.Shared.Wrappers;
using MediatR;

namespace FormsService.Application.Features.PublishForm;

public record PublishFormCommand(Guid Id, string TenantId) : IRequest<Response<Guid>>;

public class PublishFormCommandValidator : AbstractValidator<PublishFormCommand>
{
    public PublishFormCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Form ID must not be empty.");
        RuleFor(x => x.TenantId).NotEmpty().WithMessage("Tenant ID must not be empty.");
    }
}