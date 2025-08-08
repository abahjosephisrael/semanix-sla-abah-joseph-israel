namespace FormsService.Application.Interfaces;

public interface IMetrics
{
    void Increment(string name, object? tags = null);
}