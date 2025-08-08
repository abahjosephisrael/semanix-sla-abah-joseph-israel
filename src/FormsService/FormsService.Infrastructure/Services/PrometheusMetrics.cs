using FormsService.Application.Interfaces;
using Prometheus;


namespace FormsService.Infrastructure.Services;


public sealed class PrometheusMetrics : IMetrics
{
    private readonly Counter _publishedCounter = Metrics.CreateCounter("fe_forms_published_total", "Total number of forms that reached the Published state.", ["tenant"]);

    public void Increment(string name, object? tags = null)
    {
        // We only handle the counter we care about.
        if (name == "fe_forms_published_total")
        {
            var tenant = tags switch
            {
                { } t => t.GetType().GetProperty("Tenant")?.GetValue(t)?.ToString() ?? string.Empty,
                _ => string.Empty
            };

            _publishedCounter.WithLabels(tenant).Inc();
        }
    }
}