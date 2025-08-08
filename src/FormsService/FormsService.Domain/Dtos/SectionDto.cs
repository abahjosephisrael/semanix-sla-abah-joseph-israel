namespace FormsService.Domain.Dtos;

public record SectionDto(string Title, int Order, List<FieldDto> Fields);