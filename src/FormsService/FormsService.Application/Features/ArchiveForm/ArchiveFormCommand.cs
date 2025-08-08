using FluentValidation;
using FormsEngine.Shared.Wrappers;
using MediatR;

namespace FormsService.Application.Features.ArchiveForm;

public record ArchiveFormCommand(Guid Id, string TenantId) : IRequest<Response<Guid>>;

public class ArchiveFormCommandValidator : AbstractValidator<ArchiveFormCommand>
{
    public ArchiveFormCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Form ID must not be empty.");
        RuleFor(x => x.TenantId).NotEmpty().WithMessage("Tenant ID must not be empty.");
    }
}