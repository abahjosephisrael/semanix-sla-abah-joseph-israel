namespace RenderingService.Domain.Dtos;

public record Form(
    Guid id,
    string name,
    string description,
    int version,
    string state,
    List<Section> sections,
    DateTime created_at,
    string? created_by,
    DateTime? updated_at,
    string? updated_by
    );

public class Section
{
    public int Order { get; set; }
    public string Title { get; set; }
    public List<FormField> Fields { get; set; }
}

public class FormField
{
    public int Type { get; set; }
    public string Label { get; set; }
    public int Order { get; set; }
    public string FieldId { get; set; }
    public bool Required { get; set; }
    public int MaxLength { get; set; }
}
