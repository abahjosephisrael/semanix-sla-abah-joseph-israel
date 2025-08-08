using FluentValidation;
using FormsEngine.Shared.Wrappers;
using FormsService.Domain.Dtos;
using FormsService.Domain.Enums;
using MediatR;

namespace FormsService.Application.Features.CreateForm;

public record CreateFormCommand(
    string? TenantId,
    string? EntityId,
    string Name,
    string? Description,
    List<SectionDto> Sections) : IRequest<Response<Guid>>;

// Here we define the command validator for CreateFormCommand to ensure the command's properties are valid before processing it.
public class CreateFormCommandValidator : AbstractValidator<CreateFormCommand>
{
    public CreateFormCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleForEach(x => x.Sections).SetValidator(new SectionDtoValidator());
    }
}

public class SectionDtoValidator : AbstractValidator<SectionDto>
{
    public SectionDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Order).GreaterThanOrEqualTo(0);
        RuleForEach(x => x.Fields).SetValidator(new FieldDtoValidator());
    }
}

public class FieldDtoValidator : AbstractValidator<FieldDto>
{
    public FieldDtoValidator()
    {
        RuleFor(x => x.FieldId).NotEmpty();
        RuleFor(x => x.Label).NotEmpty();
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.Order).GreaterThanOrEqualTo(0);
        RuleFor(x => x.MaxLength).GreaterThan(0).When(x => x.Type == FieldType.Text || x.Type == FieldType.Number);
    }
}