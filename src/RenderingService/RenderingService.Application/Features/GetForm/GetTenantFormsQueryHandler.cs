using FormsEngine.Shared.Interfaces;
using FormsEngine.Shared.Wrappers;
using MediatR;
using RenderingService.Domain.Dtos;

namespace RenderingService.Application.Features.GetForm;

public class GetTenantFormsQueryHandler (
    IGenericRepository<Form> repository
    ): IRequestHandler<GetTenantFormsQuery, Response<IReadOnlyCollection<Form>>>
{
    public async Task<Response<IReadOnlyCollection<Form>>> Handle(GetTenantFormsQuery request, CancellationToken cancellationToken)
    {
        var forms = await repository.GetAllAsync(request.TenantId);
        if (forms is null || !forms.Any())
        {
            return Response<IReadOnlyCollection<Form>>.Failure("No forms found for the specified tenant.");
        }
        return Response<IReadOnlyCollection<Form>>.Success(forms.ToList());
    }
}
