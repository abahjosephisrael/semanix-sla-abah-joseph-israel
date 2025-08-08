using FormsService.Domain.Enums;

namespace FormsService.Domain.Models;
// Here, we are using the rich domain model approach, which encapsulates the business logic and state transitions within the model itself, giving us a more robust and maintainable design.
public class Form : BaseModel
{
    public string TenantId { get; private set; } = default!;
    public string? EntityId { get; private set; }
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public List<Section> Sections { get; private set; } = [];
    public int Version { get; private set; } = 1;
    public FormState State { get; private set; } = FormState.Draft;

    private Form() { } // EF

    public static Form CreateDraft(string tenantId, string? entityId, string name, string? desc, List<Section> sections)
    {
        ArgumentException.ThrowIfNullOrEmpty(tenantId);
        ArgumentException.ThrowIfNullOrEmpty(name);
        if (name.Length > 100) throw new ArgumentException("Name ≤ 100 chars");
        return new Form
        {
            TenantId = tenantId,
            EntityId = entityId,
            Name = name,
            Description = desc,
            Sections = sections
        };
    }
    
    public static Form Update(Form form, string name, string? desc, List<Section> sections)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        if (name.Length > 100) throw new ArgumentException("Name ≤ 100 chars");

        form.Name = name;
        form.Description = desc;
        form.Sections = sections;
        if (form.State == FormState.Archived)
            throw new InvalidOperationException("Cannot update an archived form");
        form.Version++;
        return form;
    }

    public void Publish()
    {
        if (State == FormState.Published || State == FormState.Archived)
            throw new InvalidOperationException("Illegal transition");
        State = FormState.Published;
        Version++;
    }

    public void Archive()
    {
        if (State == FormState.Archived)
            throw new InvalidOperationException("Illegal transition");
        State = FormState.Archived;
    }
}

public record Section(string Title, int Order, List<Field> Fields);
public record Field(string FieldId, string Label, FieldType Type, int Order, bool Required = false, int? MaxLength = null);
