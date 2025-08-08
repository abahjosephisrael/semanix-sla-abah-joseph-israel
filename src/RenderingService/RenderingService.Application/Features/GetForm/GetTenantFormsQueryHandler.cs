using FormsEngine.Shared.Interfaces;
using FormsEngine.Shared.Wrappers;
using MediatR;
using RenderingService.Domain.Dtos;

namespace RenderingService.Application.Features.GetForm;

public class GetTenantFormsQueryHandler (
    IGenericRepository<RenderedForm> repository
    ): IRequestHandler<GetTenantFormsQuery, Response<IReadOnlyCollection<RenderedForm>>>
{
    public async Task<Response<IReadOnlyCollection<RenderedForm>>> Handle(GetTenantFormsQuery request, CancellationToken cancellationToken)
    {
        var forms = await repository.GetAllAsync(request.TenantId);
        if (forms is null || !forms.Any())
        {
            return Response<IReadOnlyCollection<RenderedForm>>.Failure("No forms found for the specified tenant.");
        }
        return Response<IReadOnlyCollection<RenderedForm>>.Success(forms.ToList());
    }
}
